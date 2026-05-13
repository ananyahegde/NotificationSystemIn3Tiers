# 3 Tier Architecture with EFCore

### Setup

Run the following commands in the terminal:
```bash
mkdir NotificationSystem
cd NotificationSystem
```

```bash
dotnet new sln -n Bulletin
dotnet new console -n Presentation
dotnet new classlib -n BusinessLayer
dotnet new classlib -n DataAccessLayer
```

```bash
dotnet sln add Presentation/Presentation.csproj
dotnet sln add BusinessLayer/BusinessLayer.csproj
dotnet sln add DataAccessLayer/DataAccessLayer.csproj
```

```bash
dotnet add Presentation/Presentation.csproj reference BusinessLayer/BusinessLayer.csproj
dotnet add BusinessLayer/BusinessLayer.csproj reference DataAccessLayer/DataAccessLayer.csproj
```

```bash
createdb notificationsystem -U postgres
```

## Packages
Run the following commands in `DataAccessLayer/`:
```bash
dotnet add package Microsoft.EntityFrameworkCore --version 8.0.8
dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.8
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL --version 8.0.8
dotnet add package DotNetEnv --version 3.2.0
```

Also add to `Presentation/`:
```bash
dotnet add Presentation/Presentation.csproj package Microsoft.EntityFrameworkCore.Design --version 8.0.8
```

## Migrations
Run from `DataAccessLayer/`:
```bash
cd DataAccessLayer
dotnet ef migrations add InitialCreate
dotnet ef database update
```

replace the password with yours in `DataAccessLayer/Database/Context.cs`.

To run the project: 
```bash
cd Presentation
dotnet run
```

## Results

![1](Screenshots/1.png)
![2](Screenshots/2.png)
