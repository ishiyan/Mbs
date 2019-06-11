@echo off
set source_folder=node_modules\mathjax
set target_folder=src\assets\mathjax
rmdir /S /Q "%target_folder%"
mkdir "%target_folder%"
copy "%source_folder%\MathJax.js" "%target_folder%\MathJax.js"
mklink /J "%target_folder%\config" "%source_folder%\config"
mklink /J "%target_folder%\extensions" "%source_folder%\extensions"
mklink /J "%target_folder%\jax" "%source_folder%\jax"
mklink /J "%target_folder%\localization" "%source_folder%\localization"
mklink /J "%target_folder%\fonts" "%source_folder%\fonts"
