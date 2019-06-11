@echo off

set solution=Mbs.sln
set msbuild=C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\bin\MSBuild.exe
:: set msbuild=C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\MSBuild\15.0\bin\MSBuild.exe

:: set msbuild=C:\Program Files (x86)\MSBuild\14.0\bin\MSBuild.exe
:: set msbuild=C:\Windows\Microsoft.NET\Framework64\v4.0.30319\msbuild.exe

rem /fileLoggerParameters:NoItemAndPropertyList;{Summary|NoSummary};Verbosity={quiet|minimal|normal|detailed|diagnostic}
set msbuildOptions=/t:Rebuild /noconlog /nologo /maxcpucount /fl /m /fileLoggerParameters:Summary;Verbosity=minimal;Append;Encoding=UTF-8;LogFile=

set logfile=01.build.AnyCPU_Release.log
if exist %logfile% (
 del %logfile%
)
echo ================================================================================================================ >"%logfile%"
echo AnyCPU Release build>>"%logfile%"
echo ================================================================================================================>>"%logfile%"
call "%msbuild%" %solution% %msbuildOptions%%logfile% /property:Configuration=Release /property:Platform="Any CPU"
