Add Migration
dotnet ef migrations add Providers --context ScvContext --output-dir Domain/Migrations

Update Database
dotnet ef database update --context ScvContext