COMPOSE_FILE=docker-compose.yml

build:
	@echo "Building the Docker images..."
	docker compose -f $(COMPOSE_FILE) build

up:
	@echo "Starting up the containers for dima.api, dima.web and SQL Server..."
	docker compose -f $(COMPOSE_FILE) up -d

# Comando para parar e remover os containers
down:
	@echo "Stopping and removing the containers..."
	docker compose -f $(COMPOSE_FILE) down

# Comando para construir e subir os containers
run: build up
