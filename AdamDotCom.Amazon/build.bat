@echo off
cls
3rdParty\nant\bin\NAnt.exe -buildfile:AdamDotCom.Amazon.build %*
pause