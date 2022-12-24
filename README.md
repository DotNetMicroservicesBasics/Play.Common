# Play.Common
Common libraries used by play economy services.

## Create and publish package
'''cmd
$version="1.0.8"
$owner="DotNetMicroservicesBasics"
dotnet pack src\Play.Common --configuration Release -p:PackageVersion=$version -p:RepositoryUrl=https://github.com/$owner/Play.Common -o D:\Dev\NugetPackages
'''