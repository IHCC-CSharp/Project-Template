# Project Templete

In this repo I will be playing around with what the labs (LU02+) will look like.
This is my idea so far.

[//]: # (TODO Add NUnit tests)
[//]: # (TODO change front end to TypeScript)
[//]: # (TODO add Front End tests)

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

# Add SLN file
dotnet new sln
dotnet sln add API

# Add a .gitignore
dotnet new gitignore

# Run the project
dotnet run
```

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
