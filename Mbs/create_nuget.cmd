dotnet publish --configuration Release --output bin\x64\Release\net6.0\published --self-contained False

nuget pack Mbs.nuspec 
