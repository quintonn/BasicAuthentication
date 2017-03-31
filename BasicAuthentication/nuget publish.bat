rem nuget pack -OutputDirectory "..\..\..\..\NugetSource"  "..\..\BasicAuthentication.csproj"
rem if not exist ..\NugetPackages mkdir ..\NugetPackages
rem nuget pack -OutputDirectory "..\NugetPackages"  "..\..\BasicAuthentication.csproj"
rem -Exclude "config\*.*"
rem nuget pack -OutputDirectory "..\..\..\NugetSource"  "..\WebsiteTemplate.csproj"
rem nuget pack -OutputDirectory "..\..\..\..\NugetSource"  "..\..\BasicAuthentication.csproj"

ECHO Packing Nuget Package
nuget pack -OutputDirectory "..\..\..\..\NugetSource"  "..\..\BasicAuthentication.csproj"
ECHO Publishing Nuget Package
nuget push "..\..\..\..\NugetSource\BasicAuthentication*.nupkg" -s http://nugetrepo.q10hub.com/7c8fb305-b447-4e6f-86dd-88a39d99b47c v4J[TT?}eG_}+7rSg;r-AcGbhjH+F
ECHO Deleting Nuget Package
del ..\..\..\..\NugetSource\BasicAuthentication*.nupkg
ECHO Done with nuget code
