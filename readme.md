```
dotnet tool install --global dotnet-ef
```

```
dotnet ef migrations add 'initial' --project ./RestAPI/RestAPI.csproj --context RestAPI.Models.MovieContext
```

```

dotnet ef database update --project ./RestAPI/RestAPI.csproj --context RestAPI.Models.MovieContext --configuration Debugg
20240621031843_initial
```
