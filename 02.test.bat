@echo off

set logfile=02.test.x64_Release.log
if exist %logfile% (
 del %logfile%
)
echo ================================================================================================================ >"%logfile%"
echo Unit tests (x64 Release)>>"%logfile%"
echo ================================================================================================================>>"%logfile%"
call dotnet vstest "Mbs.UnitTests\bin\Release\netcoreapp2.1\Mbs.UnitTests.dll" >>"%logfile%" 2>>&1
