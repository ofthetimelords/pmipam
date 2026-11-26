set -x
PL_VERSION=$(date '+%Y.%m.%d.%H%M%S')

docker buildx build  --progress=plain --platform linux/amd64 -t vtable/pmipam:${PL_VERSION} \
	-t vtable/pmipam:latest --push --build-arg CACHEBUST=$(date +%s) .
