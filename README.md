# GScore - A score checking system

## How to install

Make sure that you have installed [.NET 9](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)

```bash
# Clone the repository
git clone https://github.com/thanhKasper/gscores-backend.git

# At the directory
cd gscores-backend

# Setup project
dotnet build

# Run the project
dotnet run --project src/GScores.UI
```

To seed the csv data into the database. Please follow these steps

```json
// Add these lines into appSettings.json or appSettings.Development.json
{
    //...
    "ScoresFilePath": "Your path to the csv file",
    "ConnectionStrings": {
        "DefaultConnection": "SQL Server Connection String"
    }
    //...
}
```

After that, go to the terminal, at the project root directory, follow these commands
```bash
# In case you haven't install the dotnet-ef tools
dotnet tool install --global dotnet-ef

# Next start updating the database
dotnet database update -p src/GScores.Infrastructure -s src/GScores.UI
```

