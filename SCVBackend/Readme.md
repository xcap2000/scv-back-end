Add Migration
dotnet ef migrations add Providers --context ScvContext --output-dir Domain/Migrations

Update Database
dotnet ef database update --context ScvContext

Revert migration
dotnet ef database update Providers

Remove migration
dotnet ef migrations remove