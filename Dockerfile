# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /source


COPY . .
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish "./vtable.pmipam/vtable.pmipam.csproj" \
    -c Release \
    -r linux-x64 \
    -o /app


FROM mcr.microsoft.com/dotnet/aspnet:10.0
ARG CACHEBUST=1
ENV DEBIAN_FRONTEND=noninteractive

RUN apt-get update \
    && apt-get -y --no-install-recommends \
       -o Dpkg::Options::="--force-confdef" \
       -o Dpkg::Options::="--force-confold" \
       upgrade \
    && apt-get install -y --no-install-recommends \
       ca-certificates \
       git \
    && apt-get autoremove -y --purge \
    && rm -rf /var/lib/apt/lists/* /var/cache/apt/archives/* /tmp/* /var/tmp/*

RUN set -eux; \
    groupadd --gid 10001 appuser; \
    useradd --uid 10001 --gid 10001 --create-home --shell /bin/bash appuser; \
    passwd -l root; \
    find / -xdev -perm /6000 -type f -exec chmod a-s {} + 2>/dev/null || true; \
    echo 'umask 027' >> /etc/profile; \
    echo 'umask 027' >> /etc/bash.bashrc; \
    awk -F: '($3 < 1000 && $1 != "root" && $7 !~ /(nologin|false)$/) {print $1}' /etc/passwd \
      | xargs -r -I{} chsh -s /usr/sbin/nologin {}

WORKDIR /app
COPY --from=build /app .

USER 10001:10001
EXPOSE 8080

ENTRYPOINT ["dotnet", "vtable.pmipam.dll"]
