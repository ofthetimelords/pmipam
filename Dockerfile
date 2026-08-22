# syntax=docker/dockerfile:1.4
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG CACHEBUST=1
WORKDIR /source
COPY . .


RUN dotnet publish "./vtable.pmipam/vtable.pmipam.csproj" -c Release -r linux-x64 -o /app
RUN ls -R /root/.nuget
RUN dotnet nuget locals all -l


FROM mcr.microsoft.com/dotnet/aspnet:10.0

RUN apt-get update && apt-get install -y \
    git

COPY harden.sh /harden.sh
RUN /bin/bash /harden.sh
WORKDIR /app
COPY --from=build /app .


ENTRYPOINT ["dotnet", "vtable.pmipam.dll"]
