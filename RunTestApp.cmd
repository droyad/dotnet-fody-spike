del build\*.* /Q
rmdir %USERPROFILE%\.nuget\packages\dotnet-fody /S /Q
dotnet pack src/dotnet-fody -o build
dotnet restore src/TestApp --configfile src/TestApp/nuget.config --no-cache
dotnet run -p src/TestApp