### Setup

```bash
dotnet new sln -n Bulletin
dotnet new console -n Presentation
dotnet new classlib -n BusinessLayer
dotnet new classlib -n DataAccessLayer

dotnet sln add Presentation/Presentation.csproj
dotnet sln add BusinessLayer/BusinessLayer.csproj
dotnet sln add DataAccessLayer/DataAccessLayer.csproj

dotnet add Presentation/Presentation.csproj reference BusinessLayer/BusinessLayer.csproj
dotnet add BusinessLayer/BusinessLayer.csproj reference DataAccessLayer/DataAccessLayer.csproj
```
