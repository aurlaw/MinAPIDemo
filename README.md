# Minimal API Demo

.NET 6 and Minimal API demo


## Running

```
dotnet restore
dotnet run --project MinAPIDemo/MinAPIDemo.csproj
```


## Entity Migrations

```
dotnet ef migrations add MIGRATION_NAME -c EfContext -o Data/Migrations --project MinAPIDemo/MinAPIDemo.csproj
dotnet ef database update --project MinAPIDemo/MinAPIDemo.csproj
```

