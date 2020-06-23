mkdir test-reports\unit-coverage
docker run --rm --volume %CD%:/tmp/app/ -w /tmp/app/ mcr.microsoft.com/dotnet/core/sdk:3.1-alpine ./scripts/test_unit_backend.sh