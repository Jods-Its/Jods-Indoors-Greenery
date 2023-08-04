using Il2Cpp;
using HarmonyLib;

namespace IndoorsGreenery
{
    internal class Patches
    {
        [HarmonyPatch(typeof(Panel_Inventory), nameof(Panel_Inventory.Initialize))]
        internal class IndoorsGreeneryInitialization
        {
            private static void Postfix(Panel_Inventory __instance)
            {
                IGUtils.inventory = __instance;
                IGButtons.InitializeIG(__instance.m_ItemDescriptionPage);
            }
        }
        [HarmonyPatch(typeof(ItemDescriptionPage), nameof(ItemDescriptionPage.UpdateGearItemDescription))]
        internal class UpdateExtractSeedsButton
        {
            private static void Postfix(ItemDescriptionPage __instance, GearItem gi)
            {
                if (__instance != InterfaceManager.GetPanel<Panel_Inventory>()?.m_ItemDescriptionPage) return;
                IGButtons.plantGearItem = gi?.GetComponent<GearItem>();
                if (gi != null && IGUtils.IsPlant(gi.name) == true)
                {
                    IGButtons.SetExtractSeedsActive(true);
                }
                else
                {
                    IGButtons.SetExtractSeedsActive(false);
                }
            }
        }
        [HarmonyPatch(typeof(ItemDescriptionPage), nameof(ItemDescriptionPage.UpdateGearItemDescription))]
        internal class UpdateFillRecipientButton
        {
            private static void Postfix(ItemDescriptionPage __instance, GearItem gi)
            {
                if (__instance != InterfaceManager.GetPanel<Panel_Inventory>()?.m_ItemDescriptionPage) return;
                IGButtons.recipientItem = gi?.GetComponent<GearItem>();
                if (gi != null && IGUtils.IsRecipient(gi.name) == true)
                {
                    if (gi.name == "GEAR_CookingPot")
                    {
                        IGButtons.SetFillRecipientFixActive(true);
                        IGButtons.SetFillRecipientActive(false);
                    }
                    else
                    {
                        IGButtons.SetFillRecipientActive(true);
                        IGButtons.SetFillRecipientFixActive(false);
                    }                                         
                }
                else
                {
                    IGButtons.SetFillRecipientFixActive(false);
                    IGButtons.SetFillRecipientActive(false);
                }
            }
        }
        [HarmonyPatch(typeof(ItemDescriptionPage), nameof(ItemDescriptionPage.UpdateGearItemDescription))]
        internal class UpdateFertilizePlanterButton
        {
            private static void Postfix(ItemDescriptionPage __instance, GearItem gi)
            {
                if (__instance != InterfaceManager.GetPanel<Panel_Inventory>()?.m_ItemDescriptionPage) return;
                IGButtons.soilItem = gi?.GetComponent<GearItem>();
                if (gi != null && IGUtils.IsSoiled(gi.name) == true)
                {
                    IGButtons.SetFertilizePlanterActive(true);
                }
                else
                {
                    IGButtons.SetFertilizePlanterActive(false);
                }
            }
        }
        [HarmonyPatch(typeof(ItemDescriptionPage), nameof(ItemDescriptionPage.UpdateGearItemDescription))]
        internal class UpdateEmptyRecipientButton
        {
            private static void Postfix(ItemDescriptionPage __instance, GearItem gi)
            {
                if (__instance != InterfaceManager.GetPanel<Panel_Inventory>()?.m_ItemDescriptionPage) return;
                IGButtons.soilItem = gi?.GetComponent<GearItem>();
                if (gi != null && IGUtils.IsSoiled(gi.name) == true)
                {
                    IGButtons.SetEmptyRecipientActive(true);
                }
                else
                {
                    IGButtons.SetEmptyRecipientActive(false);
                }
            }
        }
        [HarmonyPatch(typeof(ItemDescriptionPage), nameof(ItemDescriptionPage.UpdateGearItemDescription))]
        internal class UpdateCleanPlanterButton
        {
            private static void Postfix(ItemDescriptionPage __instance, GearItem gi)
            {
                if (__instance != InterfaceManager.GetPanel<Panel_Inventory>()?.m_ItemDescriptionPage) return;
                IGButtons.planterItem = gi?.GetComponent<GearItem>();
                if (gi != null && IGUtils.IsDead(gi.name) == true)
                {
                    IGButtons.SetCleanPlanterActive(true);
                }
                else
                {
                    IGButtons.SetCleanPlanterActive(false);
                }
            }
        }
        [HarmonyPatch(typeof(ItemDescriptionPage), nameof(ItemDescriptionPage.UpdateGearItemDescription))]
        internal class UpdatePlacePlanterButton
        {
            private static void Postfix(ItemDescriptionPage __instance, GearItem gi)
            {
                if (__instance != InterfaceManager.GetPanel<Panel_Inventory>()?.m_ItemDescriptionPage) return;
                IGButtons.placeItem = gi?.GetComponent<GearItem>();
                if (gi != null && gi.name.ToLowerInvariant().Contains("planter"))
                {
                    IGButtons.SetPlacePlanterActive(true);
                }
                else
                {
                    IGButtons.SetPlacePlanterActive(false);
                }
            }
        }
        [HarmonyPatch(typeof(GearItem), nameof(GearItem.Awake))]
        internal class SetStackables
        {
            private static void Postfix (GearItem __instance)
            {
                if (__instance != null && (__instance.name == "GEAR_SoilBag" || __instance.name == "GEAR_PlantNutrients"))
                {
                    __instance.m_StackableItem.m_StackByCondition = true;
                }
            }
        }
    }
}
