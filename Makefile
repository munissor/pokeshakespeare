E2E_NETWORK=pokeshakespeare_e2e_net
E2E_HOST=pokeshakespeare_e2e
DNS=1.1.1.1
USER=`id -u`:`id -g`

test-unit-backend:
	mkdir -p test-reports/unit-coverage
	docker run --rm -u $(USER) --volume `pwd`:/tmp/app/ -w /tmp/app/ mcr.microsoft.com/dotnet/core/sdk:3.1-alpine ./scripts/test_unit_backend.sh

test-unit-frontend:
	docker run --rm -u $(USER) --volume `pwd`:/tmp/app/ -w /tmp/app/ node:12.18.1-buster ./scripts/test_unit_frontend.sh

test-unit: test-unit-backend test-unit-frontend
	
build-docker:
	docker build . -t pokeshakespeare:latest

run-docker: build-docker
	docker run --rm --dns $(DNS) -p 5000:80 pokeshakespeare:latest

test-e2e-frontend: build-docker
	mkdir -p tmp/cypress
	docker stop $(E2E_HOST) 2>/dev/null || true
	docker network rm $(E2E_NETWORK) 2>/dev/null || true
	docker network create --driver bridge $(E2E_NETWORK)
	docker run --rm -d --dns $(DNS) -p 5000:80 --name $(E2E_HOST) --env USE_DUMMY_TRANSLATIONS=true --network=$(E2E_NETWORK) pokeshakespeare:latest
	docker run --rm -u $(USER) --volume `pwd`:/tmp/app/ --volume `pwd`/tmp/cypress:/home/node/.cache/Cypress/ -w /tmp/app/ --network=$(E2E_NETWORK) cypress/base:12.18.0 ./scripts/test_e2e_frontend.sh
	docker stop $(E2E_HOST)
	docker network rm $(E2E_NETWORK)

test-e2e-backend: build-docker
	docker stop $(E2E_HOST) 2>/dev/null || true
	docker network rm $(E2E_NETWORK) 2>/dev/null || true
	docker network create --driver bridge $(E2E_NETWORK)
	docker run --rm -d --dns $(DNS) -p 5000:80 --name $(E2E_HOST) --network=$(E2E_NETWORK) pokeshakespeare:latest
	docker run --rm -u $(USER) --volume `pwd`:/tmp/app/ -w /tmp/app/ --env TEST_HOST=$(E2E_HOST) --network=$(E2E_NETWORK) mcr.microsoft.com/dotnet/core/sdk:3.1-alpine ./scripts/test_e2e_backend.sh
	docker stop $(E2E_HOST)
	docker network rm $(E2E_NETWORK)