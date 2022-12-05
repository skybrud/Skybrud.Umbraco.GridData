@echo off
dotnet build src/Skybrud.Umbraco.GridData --configuration Release /t:rebuild /t:pack -p:PackageOutputPath=../../releases/nuget