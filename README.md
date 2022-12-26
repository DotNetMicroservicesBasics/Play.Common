# Play.Common
Common libraries used by play economy services.

## Create and publish package
```powershell
$version="1.0.13"
$owner="DotNetMicroservicesBasics"
$local_packages_path="D:\Dev\NugetPackages"
$gh_pat="PAT HERE"

dotnet pack src\Play.Common --configuration Release -p:PackageVersion=$version -p:RepositoryUrl=https://github.com/$owner/Play.Common -o $local_packages_path

dotnet nuget push $local_packages_path\Play.Common.$version.nupkg --api-key $gh_pat --source github
```