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
	
generate-key-pair-base64:
	openssl genrsa -out private.pem 2048
	openssl rsa -in private.pem -pubout -out public.pem
	openssl rsa -in private.pem -outform PEM | base64 -w 0 > private.pem.base64
	openssl rsa -in private.pem -pubout -outform PEM | base64 -w 0 > public.pem.base64