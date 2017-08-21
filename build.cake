//////////////////////////////////////////////////////////////////////
// TOOLS
//////////////////////////////////////////////////////////////////////
#tool "nuget:?package=GitVersion.CommandLine&prerelease"
#addin "MagicChunks"

using Path = System.IO.Path;
using IO = System.IO;

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////
var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

///////////////////////////////////////////////////////////////////////////////
// GLOBAL VARIABLES
///////////////////////////////////////////////////////////////////////////////
var artifactsDir = "./artifacts";
var localPackagesDir = "../LocalPackages";
var packageName = "Octopus.Configuration";
var globalAssemblyFile = "./source/" + packageName + "/Properties/AssemblyInfo.cs";
var projectToPackage = "./source/" + packageName;
var cleanups = new List<IDisposable>(); 

var isContinuousIntegrationBuild = !BuildSystem.IsLocalBuild;

var gitVersionInfo = GitVersion(new GitVersionSettings {
    OutputType = GitVersionOutput.Json
});

var nugetVersion = gitVersionInfo.NuGetVersion;

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////
Setup(context =>
{
    Information("Building " + packageName + " v{0}", nugetVersion);
});

Teardown(context =>
{
	Information("Finished running tasks.");
});

//////////////////////////////////////////////////////////////////////
//  PRIVATE TASKS
//////////////////////////////////////////////////////////////////////

Task("__Default")
    .IsDependentOn("__Clean")
    .IsDependentOn("__Restore")
    .IsDependentOn("__Build")
    .IsDependentOn("__Pack")
	.IsDependentOn("__Publish")
	.IsDependentOn("__CopyToLocalPackages");

Task("__Clean")
    .Does(() =>
{
    CleanDirectory(artifactsDir);
    CleanDirectories("./source/**/bin");
    CleanDirectories("./source/**/obj");
});

Task("__Restore")
    .Does(() => DotNetCoreRestore("source"));
	
Task("__Build")
    .Does(() =>
{
    DotNetCoreBuild("source", new DotNetCoreBuildSettings
    {
        Configuration = configuration,
        ArgumentCustomization = args => args.Append($"/p:Version={nugetVersion}")
    });
});

Task("__Pack")
    .Does(() => 
{
	DotNetCorePack(projectToPackage, new DotNetCorePackSettings
	{
		Configuration = configuration,
		OutputDirectory = artifactsDir,
		NoBuild = true,
        ArgumentCustomization = args => args.Append($"/p:Version={nugetVersion}")
	});
});

Task("__Publish")
    .WithCriteria(BuildSystem.IsRunningOnTeamCity)
    .IsDependentOn("__Pack")
    .Does(() =>
{
    NuGetPush($"{artifactsDir}/{packageName}.{nugetVersion}.nupkg", new NuGetPushSettings {
		Source = "https://octopus.myget.org/F/octopus-dependencies/api/v3/index.json",
		ApiKey = EnvironmentVariable("MyGetApiKey")
	});
	
    if (string.IsNullOrWhiteSpace(gitVersionInfo.PreReleaseLabel))
    {
        NuGetPush($"{artifactsDir}/{packageName}.{nugetVersion}.nupkg", new NuGetPushSettings {
            Source = "https://www.nuget.org/api/v2/package",
            ApiKey = EnvironmentVariable("NuGetApiKey")
        });
    }
});

Task("__CopyToLocalPackages")
    .WithCriteria(BuildSystem.IsLocalBuild)
    .IsDependentOn("__Pack")
    .Does(() =>
{
    CreateDirectory(localPackagesDir);
    CopyFileToDirectory(Path.Combine(artifactsDir, $"{packageName}.{nugetVersion}.nupkg"), localPackagesDir);
});

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////
Task("Default")
    .IsDependentOn("__Default");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////
RunTarget(target);
