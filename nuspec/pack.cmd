@echo off
del *.nupkg
rem nuget pack Acr.Data.nuspec
nuget pack Acr.Data.Ef.nuspec
pause