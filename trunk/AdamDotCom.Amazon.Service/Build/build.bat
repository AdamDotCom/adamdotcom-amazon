@echo off
cls
..\Dependencies\nant\bin\NAnt.exe -buildfile:AdamDotCom.Amazon.Service.build %*
pause