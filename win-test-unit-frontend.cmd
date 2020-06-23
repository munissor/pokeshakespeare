md tmp\cypress
pushd Munisso.PokeShakespeare.Web/ClientApp
npm install
popd
docker run --rm --volume %CD%\:/tmp/app/ -w /tmp/app/ node:12.18.1-buster ./scripts/test_unit_frontend.sh