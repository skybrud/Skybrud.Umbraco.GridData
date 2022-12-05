@echo off
dotnet build src/Skybrud.Umbraco.GridData --configuration Debug /t:rebuild /t:pack -p:PackageOutputPath=c:/nuget