test-unit:
	mkdir -p test-reports/unit-coverage
	docker run --rm --volume `pwd`:/tmp/app/ -w /tmp/app/ mcr.microsoft.com/dotnet/core/sdk:3.1-alpine ./scripts/test_unit.sh

build-docker:
	docker build . -t pokeshakespeare:latest

run-docker: build-docker
	docker run --dns 1.1.1.1 -p 5000:80 pokeshakespeare:latest