@echo off
"C:\Program Files (x86)\Microsoft Visual Studio\2019\Professional\MSBuild\Current\Bin\msbuild.exe" "src/Skybrud.Umbraco.GridData" /t:rebuild /p:BuildTools=1 /p:Configuration=Release