@echo Off
set config=%1
if "%config%" == "" (
   set config=Release
)
 
set version=1.0.1
if not "%PackageVersion%" == "" (
   set version=%PackageVersion%
)

set nuget=
if "%nuget%" == "" (
	set nuget=nuget
)

REM Package restore
call %NuGet% restore BasicAuthentication\packages.config -OutputDirectory packages -NonInteractive

%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild .\BasicAuthentication.sln /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=diag /nr:false
REM %WINDIR%\Microsoft.NET\Framework64\v4.0.30319\msbuild BasicAuthentication.sln /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=diag /nr:false

mkdir Build
mkdir Build\lib
mkdir Build\lib\net40

%nuget% pack "BasicAuthentication\BasicAuthentication.csproj" -NoPackageAnalysis -verbosity detailed -o Build -Version %version% -p Configuration="%config%"