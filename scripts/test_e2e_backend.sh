#!/bin/sh
export HOME=/tmp
DOTNET_CLI_TELEMETRY_OPTOUT=true

cd Munisso.PokeShakespeare.Web.E2ETests

dotnet test
