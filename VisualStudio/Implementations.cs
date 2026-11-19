using Il2Cpp;
using IndoorsGreenery;
using MelonLoader;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ModNamespace;
internal sealed class Implementations : MelonMod
{
    private static List<string> cookableGear = new List<string>();

    private static bool loadedCookingTex;

    private static Material vanillaLiquidMaterial;
    public override void OnInitializeMelon()
	{
        MelonLoader.MelonLogger.Msg(System.ConsoleColor.Yellow, "Fertilizing soil...");
        MelonLoader.MelonLogger.Msg(System.ConsoleColor.Yellow, "Extracting seeds...");
        MelonLoader.MelonLogger.Msg(System.ConsoleColor.Yellow, "Setting planters...");
        MelonLoader.MelonLogger.Msg(System.ConsoleColor.Green, "Indoors Greenery Loaded!");
        Settings.instance.AddToModSettings("Indoors Greenery");
    }
    public override void OnSceneWasInitialized(int level, string name)
    {
        // Wait until an actual gameplay scene loads so ModComponent bundles are fully registered
        if (string.IsNullOrEmpty(name) || name == "Boot" || name == "Empty" || name.Contains("MainMenu"))
        {
            return;
        }

        if (!loadedCookingTex) // adding pot cooking textures
        {
            cookableGear.Clear(); // prevents duplicate entries if a scene reload happens before loadedCookingTex = true
            cookableGear.Add("PreparedSoapberries");
            cookableGear.Add("PreparedBlueberries");
            cookableGear.Add("HerbLeavesDried");
            Material potMat;
            GameObject potGear;

            for (int i = 0; i < cookableGear.Count; i++)
            {
                potGear = Addressables.LoadAssetAsync<GameObject>("GEAR_" + cookableGear[i]).WaitForCompletion();

                if (potGear == null) continue;
                Texture? tex = potGear.transform.Find("POTtexture")?.GetComponent<MeshRenderer>()?.material?.mainTexture;

                if (tex == null)
                {
                    MelonLogger.Msg(System.ConsoleColor.Red, "Jods, you forgor 💀");
                    return;
                }

                potMat = InstantiateLiquidMaterial();
                potMat.name = ("CKN_" + cookableGear[i] + "_MAT");

                potMat.mainTexture = tex;
                potMat.SetTexture("_Main_texture2", tex);

                potGear.GetComponent<Cookable>().m_CookingPotMaterialsList = new Material[1] { potMat };
            }

            loadedCookingTex = true;
        }
    }

    public static Material InstantiateLiquidMaterial()
    {
        if (!vanillaLiquidMaterial)
        {
            GameObject coffeeGear = Addressables.LoadAssetAsync<GameObject>("GEAR_CoffeeCup").WaitForCompletion();
            if (coffeeGear != null)
            {
                vanillaLiquidMaterial = new Material(coffeeGear.GetComponent<Cookable>().m_CookingPotMaterialsList[0]);
                vanillaLiquidMaterial.name = "Liquid";
            }
        }
        return new Material(vanillaLiquidMaterial);
    }
}