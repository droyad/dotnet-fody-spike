del build\*.* /Q
rmdir %USERPROFILE%\.nuget\packages\dotnet-fody /S /Q
"C:\Program Files (x86)\MSBuild\14.0\bin\MSBuild" dotnet-fody.sln
c:\temp\nuget pack src/dotnet-fody/dotnet-fody.csproj -OutputDirectory build
dotnet restore src/TestApp --configfile src/TestApp/nuget.config --no-cache
dotnet run -p src/TestApp