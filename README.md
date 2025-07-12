# GScore - A score checking system

## How to install

### Locally installed into machine

Make sure that you have installed [.NET 9](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)

- Clone the repository

```bash
# Clone the repository
git clone https://github.com/thanhKasper/gscores-backend.git

# At the directory
cd gscores-backend
```

- Add these lines into **src/GScores.UI/appsettings.json** or **src/GScores/UI/appsettings.Development.json**

```json
{
  "ScoresFilePath": "Your path to the csv file",
  "ConnectionStrings": {
    "DefaultConnection": "SQL Server Connection String"
  }
}
```

- Build and run the project

```bash
# Setup project
dotnet build

# Run the project
dotnet run --project src/GScores.UI
```

### Using Docker

```bash
# Run the command
docker compose up

# To stop the server
docker compose down

# The url is now http://localhost:8080
```
