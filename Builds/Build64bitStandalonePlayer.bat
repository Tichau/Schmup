@echo off

:: Variables
set UnityEditorFolder=C:\Program Files (x86)\Unity\Editor
set BuildFolder=Builds\Build 64bit %date:~6,4%.%date:~3,2%.%date:~0,2%
set UnityProjectFolder=%~dp0..\UnityProject
set GameExecutableName=Schmup.exe

:: Build
echo Build game in folder "%BuildFolder%"
"%UnityEditorFolder%\Unity.exe" -batchmode -nographics -quit -projectPath "%UnityProjectFolder%" -buildWindows64Player "%~dp0%BuildFolder%\%GameExecutableName%" 

echo Success
pause