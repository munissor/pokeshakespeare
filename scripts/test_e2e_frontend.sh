#!/bin/sh
cd Munisso.PokeShakespeare.Web/ClientApp
npm install
npm install cypress
export CYPRESS_BASE_URL=http://pokeshakespeare_e2e/
npm run e2e-tests