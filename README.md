# Project Templete

In this repo I will be playing around with what the labs (LU02+) will look like.
This is my idea so far.

## Back End

ASP.NET Core

```bash
# Create the project
dotnet new webapi -n MyRawApi
cd MyRawApi

# Add SQLite and Dapper
dotnet add package Microsoft.Data.Sqlite
dotnet add package Dapper
```

## Front End

[Svelte](https://svelte.dev/) + TailwindCSS

```bash
npx sv create my-app
```