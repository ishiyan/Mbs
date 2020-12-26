dotnet publish --configuration Release --output bin\x64\Release\net5.0\published --self-contained False

nuget pack Mbs.Api.nuspec 
