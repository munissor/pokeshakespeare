SET E2E_NETWORK=pokeshakespeare_e2e_net
SET E2E_HOST=pokeshakespeare_e2e
SET DNS=1.1.1.1
SET ENV_DUMMY=
:: SET ENV_DUMMY=--env USE_DUMMY_TRANSLATIONS=true
call .\win-build-docker.cmd
docker stop %E2E_HOST%
docker network rm %E2E_NETWORK%
docker network create --driver bridge %E2E_NETWORK%
docker run --rm -d --dns %DNS% -p 5000:80 --name %E2E_HOST% %ENV_DUMMY% --network=%E2E_NETWORK% pokeshakespeare:latest
docker run --rm --volume %CD%:/tmp/app/ -w /tmp/app/ --env TEST_HOST=%E2E_HOST% --network=%E2E_NETWORK% mcr.microsoft.com/dotnet/core/sdk:3.1-alpine ./scripts/test_e2e_backend.sh
docker stop %E2E_HOST%
docker network rm %E2E_NETWORK%