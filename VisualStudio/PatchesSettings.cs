using Il2Cpp;
using Il2CppTLD.Gear;
using HarmonyLib;
using MelonLoader;

namespace IndoorsGreenery
{
    internal class PatchesSettings
    {
        [HarmonyPatch(typeof(StartGear), "AddAllToInventory")]
        internal class StartGear_AddAllToInventory
        {
            private static void Postfix()
            {
                if (Settings.instance.gardener)
                {
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(IGUtils.startItem1, 4);
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(IGUtils.startItem2, 4);
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(IGUtils.startItem3, 4);
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(IGUtils.startItem4, 1);
                }
            }
        }
        [HarmonyPatch] // ModComponent patch of gear spawns
        class ManageSpawnsGreenery
        {
            public static System.Reflection.MethodBase TargetMethod()
            {
                var type = AccessTools.TypeByName("ModComponent.Mapper.ZipFileLoader");
                return AccessTools.FirstMethod(type, method => method.Name.Contains("TryHandleTxt"));
            }
            public static bool Prefix(string zipFilePath, string internalPath, ref string text, ref bool __result)
            {
                if (zipFilePath.EndsWith("Indoors_Greenery.modcomponent"))
                {
                    string fileName = internalPath.Replace("gear-spawns/", "").Replace(".txt", "");

                    if (Settings.instance.noGardeningTools && fileName == "GardeningTools")
                    {
                        MelonLogger.Msg(ConsoleColor.DarkYellow, "Skipping based on settings: " + fileName);
                        text = "";
                    }

                    if (Settings.instance.noPlantNutrients && fileName == "PlantNutrients")
                    {
                        MelonLogger.Msg(ConsoleColor.DarkYellow, "Skipping based on settings: " + fileName);
                        text = "";
                    }

                    if (Settings.instance.noRainDrops && fileName == "PlantWater")
                    {
                        MelonLogger.Msg(ConsoleColor.DarkYellow, "Skipping based on settings: " + fileName);
                        text = "";
                    }

                    if (Settings.instance.noGardeningMesh && fileName == "GardeningMesh")
                    {
                        MelonLogger.Msg(ConsoleColor.DarkYellow, "Skipping based on settings: " + fileName);
                        text = "";
                    }

                    if (Settings.instance.noSeeds && fileName == "Seeds")
                    {
                        MelonLogger.Msg(ConsoleColor.DarkYellow, "Skipping based on settings: " + fileName);
                        text = "";
                    }
                }

                return true;
            }
        }
    }
}
