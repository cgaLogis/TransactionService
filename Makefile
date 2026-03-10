.PHONY: build test up down deploy run

# Build the .NET solution
build:
	dotnet build TransactionService.sln

# Run the tests in the solution
test:
	dotnet test TransactionService.sln

# Start the backing services required for local development
up:
	docker-compose up -d

# Stop the backing services
down:
	docker-compose down

# Build the WebAPI Docker image
deploy:
	docker build -t my-app -f src/TransactionService.Api/Dockerfile .
	@echo "Docker image 'my-app' built successfully."
	@echo "To deploy, ensure 'my-app' and 'mssql' services are uncommented in docker-compose.yml, then run 'make up'."

# Run the WebApi application locally
run:
	dotnet run --project src/TransactionService.Api/TransactionService.Api.csproj