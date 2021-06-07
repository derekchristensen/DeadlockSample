@echo off
del /q /f .\bin\*.*
dotnet build .\DeadlockSample\DeadlockSample.csproj -c Release -o .\bin
dotnet build .\DeadlockSample.MyApp\DeadlockSample.MyApp.csproj -c Release -o .\bin
