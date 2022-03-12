# Minimal API Demo

.NET 6 and Minimal API demo

## Installing Open API Tooling

```
dotnet new tool-manifest
dotnet tool install SwashBuckle.AspNetCore.Cli
```

Building will generate `swagger.json` and `swagger.yaml` under `min-client/references/`

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



## React Client

- Vite
- Typescript
- React Query

```
  cd min-client
  npm install
  npm run dev
```

yarn add --dev openapi-typescript-codegen
{
  "scripts": {
    "codegen": "cd ../MinAPIDemo && dotnet build && cd ../min-client && yarn openapi --input references/swagger.json --output references/codegen --client axios --postfix Service --useOptions --useUnionTypes"
  }
}
