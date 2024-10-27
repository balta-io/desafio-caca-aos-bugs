build:
	@echo "Building the Docker image for dima.api..."	
	docker compose build

up:
	@echo "Starting up the containers for dima.api and SQL Server..."
	docker compose up -d

down:
	@echo "Stopping and removing the containers..."
	docker compose down

run: build up
