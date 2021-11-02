@echo off
set source_folder=node_modules\mathjax
set target_folder=src\assets\mathjax
rmdir /S /Q "%target_folder%"
mkdir "%target_folder%"
xcopy /S /Q /G /R /Y /I "%source_folder%\es5" "%target_folder%\es5"
