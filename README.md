# Calculator
The backend part of Calculator.

# Installation
## Possible solution with docker:
`console
	CALCULATOR_BACKEND_PATH=/path/to/project bash run.sh
`
This command will install docker image and start `ServerAPI` inside docker container

## Possible solution without docker
`console
	CALCULATOR_BACKEND_PATH=/path/to/project bash init.sh
`
This command will create database and tables, needed for backend. Postgres must be installed with default configuration for `postgres` user. (Will change this later)
