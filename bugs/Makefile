COMPOSE_FILE=docker-compose.yml
COMPOSE_FILE_MIGRATION=docker-compose-migration.yml

migrations:
	@echo "Executing migrations..."
	docker compose -f $(COMPOSE_FILE_MIGRATION) up --build --abort-on-container-exit

up:
	@echo "Starting up the containers for dima.api, dima.web and SQL Server..."
	docker compose -f $(COMPOSE_FILE) up -d 

down:
	@echo "Stopping and removing the containers..."
	docker compose -f $(COMPOSE_FILE) down