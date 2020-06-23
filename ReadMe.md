# PokeShakespeare

A search engine to show the Shakespearean description for pokemons

## Code structure

### Backend

The `Munisso.PokeShakespeare.Web` contains the application code, divided in few namespaces
- Models: Data models used in the app
- Repositories: Classes to communicate with external services
- Services: Contain the business logic, consume repositories to achieve functionality
- Controllers: define API access to services

Unit tests are included in the `Munisso.PokeShakespeare.Web.Tests` project and End to End tests are in the `Munisso.PokeShakespeare.Web.E2ETests`


### Frontend

The frontend code lives in `Munisso.PokeShakespeare.Web/ClientApp/src`. Each component has matching tests in a sidecard `.tests.js` file. End to end tests are located in the `./cypress/integration` folder.

## Running the App


**Data usage Warning**: running the frontend E2E image needs a large (~500MB) docker image. Other command try to use the smallest image possible.

The solution file provided has not been tested using Visual Studio, it should work but is not guaranteed.
The application and tests have been dockerized to require minimal setup to run, and maximum platform compatibility.

### Linux and MacOS

Requires `make` and `docker` running on the client machine. The port 5000 needs to be available on the host machine.

A Makefile is provided, with the following commands:

- `build-docker`: builds the docker image for the application
- `run-docker`: runs the app in docker, will be accessible at http://localhost:5000/
- `test-unit-backend`: runs the unit tests for the backend application. A code coverage report will be available at `./test-reports/unit-coverage/index.html` (from the filesystem, not the running app)
- `test-unit-frontend`: runs the unit tests for the frontend application. Report is displayed on screen.
- `test-e2e-frontend`: runs (headless) end to end tests for the applications. Recordings will be made available at `Munisso.PokeShakespeare.Web/ClientApp/cypress/videos`. Should test fail, screenshot of the state will be placed at `Munisso.PokeShakespeare.Web/ClientApp/cypress/screenshots`.
- `test-e2e-backend`: runs end to end tests for the web api. Report is displayed on screen.

### Windows

Requires `docker` (with standard containers, not Windows contianers) and the LTS version of `nodejs` available for running.

Scripts are available to build, run and test the application:
- `win-build-docker.cmd`: builds the docker image for the application
- ``win-run-docker.cmd`: runs the app in docker, will be accessible at http://localhost:5000/
- `win-test-unit-backend.cmd`: runs the unit tests for the backend application. A code coverage report will be available at `./test-reports/unit-coverage/index.html` (from the filesystem, not the running app)
- `win-test-unit-frontend.cmd`: runs the unit tests for the frontend application. Report is displayed on screen.
- `win-test-e2e-frontend.cmd`: runs (headless) end to end tests for the applications. Recordings will be made available at `Munisso.PokeShakespeare.Web/ClientApp/cypress/videos`. Should test fail, screenshot of the state will be placed at `Munisso.PokeShakespeare.Web/ClientApp/cypress/screenshots`.
- `win-test-e2e-backend.cmd`: runs end to end tests for the web api. Report is displayed on screen.

### Using the dotnet / npm cli

Standard dotnet cli command (e.g.: dotnet run, dotnet test, ...) can be run from the solution and project folders.

The unit tests can be executed from the `Munisso.PokeShakespeare.Web/ClientApp/` folder running `npm run test`. 
The cypress GUI (and associated E2E tests) can be executed by running `npm run cypress`. The application needs to be reachable at http://localhost:5000 for the tests to run correctly.

Mixing cli commands and the docker commands above may create file ownerhip clashes on build artifacts. Just delete the affected files to solve the issue.

## API Limitations

The Shakespeare translation API is limited to 5 free calls per hour. The service caches the response for 5 minutes in order to improve performance and reduce API calls. 

In case the translation API rate limits your IP there are two possible solutions:
1) provide a valid API key by making a `FUNTRANSLATIONS_API_KEY=<key>` environnment variable available to the backend web server.
2) use a dummy translation repository by making a `USE_DUMMY_TRANSLATIONS=true` environnment variable available to the backend web server (there are commented versions for this in the Makefile and windows commands). Using this second option the E2E tests for the backend will fail because the dummy provider just returns "translated" for each request. The E2E test for the frontend should still work because the content of the translation is not verified.
