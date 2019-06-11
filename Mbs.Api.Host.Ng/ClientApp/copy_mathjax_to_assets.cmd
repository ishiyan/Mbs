@echo off
set source_folder=node_modules\mathjax
set target_folder=src\assets\mathjax
rmdir /S /Q "%target_folder%"
mkdir "%target_folder%"
copy "%source_folder%\MathJax.js" "%target_folder%\MathJax.js"
xcopy /S /Q /G /R /Y /I "%source_folder%\config" "%target_folder%\config"
xcopy /S /Q /G /R /Y /I "%source_folder%\extensions" "%target_folder%\extensions"
xcopy /S /Q /G /R /Y /I "%source_folder%\jax" "%target_folder%\jax"
xcopy /S /Q /G /R /Y /I "%source_folder%\localization" "%target_folder%\localization"
xcopy /S /Q /G /R /Y /I "%source_folder%\fonts" "%target_folder%\fonts"
