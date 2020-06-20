FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine as buildbackend

WORKDIR /opt/pokeshakespeare
ADD . /opt/pokeshakespeare
RUN dotnet build --configuration Release

FROM node:12.18.1-buster as buildfrontend

WORKDIR /opt/pokeshakespeare
ADD ./Munisso.PokeShakespeare.Web/ClientApp /opt/pokeshakespeare
RUN npm i --arch=x64 --platform=linuxmusl
RUN npm run build

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine
WORKDIR /opt/pokeshakespeare
COPY --from=buildbackend  /opt/pokeshakespeare/Munisso.PokeShakespeare.Web/bin/Release/netcoreapp3.1/ .
COPY --from=buildfrontend  /opt/pokeshakespeare/build ./ClientApp/build

ENTRYPOINT [ "dotnet", "Munisso.PokeShakespeare.Web.dll" ]
