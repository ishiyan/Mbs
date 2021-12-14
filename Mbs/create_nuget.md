# How to create a NuGet package

Please follow instructions below.

See the [nuspec documentation](https://docs.microsoft.com/en-us/nuget/reference/nuspec) to verify if specifications have been changed.

## Download NuGet client tools

Download the latest version of the `nuget.exe` from the [Microsoft website](https://docs.microsoft.com/en-us/nuget/install-nuget-client-tools) and put it somewhere in the path so that when you type `nuget` in a command prompt you see its usage output.

## Download NuGetPackageExplorer

Download a `zip` the latest version of the `NuGetPackageExplorer` from the [GitHub repo](https://github.com/NuGetPackageExplorer/NuGetPackageExplorer/releases/) and unzip it in some folder. You may not copy all numerious folders with locales and resources, they are not needed.
It is quite large, more than 200 mb.

Alternatively, on Wondows 10 you can install the `NuGetPackageExplorer` from the [Microsoft Store](https://www.microsoft.com/store/apps/9wzdncrdmdm3?ocid=badge). This is the recommended way.

## Update the `nuspec` and the `csproj` files

Edit the `Mbs.nuspec` and the `Mbs.csproj` files and update the version numbers.
Make sure the version number in the `Mbs.nuspec` is the same as the assembly version in the `Mbs.csproj`.

Note the `Mbs.nuspec`is referenced from the `Mbs.csproj`.

## Buld the package in the terminal

Run the `create_nuget.cmd` which will issue the following commands.

```bash
dotnet publish --configuration Release --output bin\x64\Release\net6.0\published --self-contained False

nuget pack Mbs.nuspec 
```

The package will be created in the current directory.

## Buld the package in the Visual Studio

Ensure you have a `Properties\PublishProfiles\FolderProfile.pubxml` file with the following content.
Make sure that the `PublishDir` matches with the path specified in the `Mbs.nuspec` file.

```xml
<?xml version="1.0" encoding="utf-8"?>
<!--
https://go.microsoft.com/fwlink/?LinkID=208121. 
-->
<Project ToolsVersion="4.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <PropertyGroup>
    <Configuration>Release</Configuration>
    <Platform>x64</Platform>
    <PublishDir>bin\x64\Release\net6.0\published</PublishDir>
    <PublishProtocol>FileSystem</PublishProtocol>
    <TargetFramework>net6.0</TargetFramework>
    <SelfContained>false</SelfContained>
  </PropertyGroup>
</Project>
```

Right-click on `Mbs` project in the `Solution Explorer` tab and select `Publish...`.
You should see the information from the file above in the summary section.

Click on `Publish` button. The package will be created in the `bin\x64\Release\net6.0\published` directory.

## Inspect the package

Start the `NuGetPackageExplorer.exe` and open the package you've just built,
e.g. `Mbs.1.0.1.nupkg`

You should not see any warnings or errors, everything should have green checkmarks.

Open the `tools\Analyze Package` menu. You should see `0 issue(s) found`.

Now you are ready to deploy the package to a feed.
