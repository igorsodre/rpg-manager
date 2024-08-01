all:
	echo "choose a command from the list bellow"
	
add-migration:
	dotnet ef migrations add $(name) -p rpg-manager.database/rpg-manager.database.csproj -o Migrations
	
run-migrations:
	dotnet ef database update -p rpg-manager.database/rpg-manager.database.csproj
	
spin-database:
	docker-compose -f docker-compose.utils.yaml up -d postgresql 
	
spin-cache:
	docker-compose  -f docker-compose.utils.yaml up -d redis