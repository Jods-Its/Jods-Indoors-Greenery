using MelonLoader;
using System.Reflection;

//This is a C# comment. Comments have no impact on compilation.

[assembly: AssemblyTitle(BuildInfo.ModName)]
[assembly: AssemblyCopyright($"Created by ModAuthor")]

[assembly: AssemblyVersion(BuildInfo.ModVersion)]
[assembly: AssemblyFileVersion(BuildInfo.ModVersion)]
[assembly: MelonInfo(typeof(ModNamespace.Implementations), BuildInfo.ModName, BuildInfo.ModVersion, BuildInfo.ModAuthor)]

//This tells MelonLoader that the mod is only for The Long Dark.
[assembly: MelonGame("Hinterland", "TheLongDark")]
[assembly: MelonPriority(800)]
internal static class BuildInfo
{
	internal const string ModName = "Indoors Greenery";
	internal const string ModAuthor = "Jods-Its";
	/// <summary>
	/// Version numbers in C# are a set of 1 to 4 positive integers separated by periods.
	/// Mods typically use 3 numbers. For example: 1.2.1
	/// </summary>
	internal const string ModVersion = "3.0.0";
}