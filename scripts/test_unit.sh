#!/bin/sh
DOTNET_CLI_HOME="/tmp/dotnet"
DOTNET_CLI_TELEMETRY_OPTOUT="true"

dotnet tool install -g dotnet-reportgenerator-globaltool

export PATH="$PATH:/root/.dotnet/tools"

dotnet build

cd Munisso.PokeShakespeare.Web.Tests

dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=lcov /p:CoverletOutput=./lcov.info
reportgenerator -reports:"**/lcov.info" -reporttypes:"Html" -targetdir:../test-reports/unit-coverage
