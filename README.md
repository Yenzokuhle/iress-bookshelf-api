# Iress Bookshelf Restful API

Powered .NET CORE framework

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

Before we get started, we're going to need to make sure we have a few things installed and available on our machine.

### Installing

Below is a series of step by step instructions that tell you how to get a development env running.

Create a local clone of the repository.

```bash
git clone git@github.com:Yenzokuhle/iress-bookshelf-api.git
```

Enter the new repo.

### Step 1

Point the ConnectionString to you local machine by changing the Server property in the program.cs file.

### Step 2

When Running Pre-Existing Migrations you can use one of two approaches. The first being via the "ef tool" which you can install globally using the:

```bash
dotnet tool update --global dotnet-ef --version 9.0.5
```

### Step 3

After it is successfully installed you can use this command

```bash
dotnet ef database update
```

This will create the database BookDB in the local server you pointed it too in step above. It will also run the migration in the Migrations folder in you root project directory.

if you do not have the ef tool installed on you machine you can use Package Manager Console in you visual studio

and run this command

```bash
Update-Database
```

Make sure the backend server is running to have the full app experience.

## extra:

Folder called images has some good book images one could test with.

## Built With

The details of the tech stack that has been used:

- [.NET Core](https://dotnet.microsoft.com/en-us/download) - Backend Framework
- [Entity Framework](https://learn.microsoft.com/en-us/ef/core/) - ORM tool
- [SQL Server](https://www.microsoft.com/en-za/sql-server/sql-server-downloads) - Relational database tool

## Thank you for the opportunity
