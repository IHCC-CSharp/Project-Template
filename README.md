# Project Templete

In this repo I will be playing around with what the labs (LU02+) will look like.
This is my idea so far.

[//]: # 'TODO Add NUnit tests'
[//]: # 'TODO change front end to TypeScript'
[//]: # 'TODO add Front End tests'

## Creating the project

### Back End

ASP.NET Core

```bash
# Create the project
dotnet new webapi -n API
cd API
# Packages via NuGet
dotnet add package Microsoft.Data.Sqlite
dotnet add package Dapper
dotnet add package Microsoft.AspNetCore.OpenApi
dotnet add package Scalar.AspNetCore

# Add SLN file might need to add `--format slnx`
dotnet new sln
dotnet sln add API

# Add a .gitignore
dotnet new gitignore

# Run the project
dotnet run
```

- Look at the Scalar UI: http://localhost:[port]/scalar
- Your OpenAPI documentation: http://localhost:[port]/openapi/v1.json

### Front End

[Svelte](https://svelte.dev/) + TailwindCSS

```bash
npx sv create front-end
cd front-end
npm install
npm run dev
```

## Database

## Model

## API

## Front End

```bash
npx sv create campus-locker-ui
# Choose: SvelteKit, TypeScript, and Demo App (or Skeleton)
cd campus-locker-ui
npm install

# Install Packages
npm install @hey-api/openapi-ts -D
npm install @hey-api/client-fetch

# Generate the API client
npx openapi-ts -i http://localhost:[port]/openapi/v1.json -o src/lib/api
```

Create a file at `src/lib/api-client.ts` to set the base URL so Svelte knows where to find your .NET app.

```typescript
import { client } from './api/client.gen';

client.setConfig({
    baseUrl: 'http://localhost:5090',
});
```
