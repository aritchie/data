@echo off
del *.nupkg
nuget pack Acr.Data.nuspec
nuget pack Acr.Data.Ef.nuspec
pause