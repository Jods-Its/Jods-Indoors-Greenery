using Il2Cpp;
using Il2CppNodeCanvas.Tasks.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace IndoorsGreenery
{
    internal class Buttons
    {
        internal static string seedText;
        internal static string fillText;
        internal static string fertilizeText;
        internal static string cleanText;
        private static GameObject seedButton;
        internal static GameObject fillButton;
        private static GameObject cleanButton;
        private static GameObject fertilizeButton;

        private static string seedFromPlant;
        internal static GearItem plantGearItem;
        private static string plantGameObject;

        internal static GearItem recipientItem;
        internal static string recipientItemName;
        internal static GearItem soilItem;
        internal static string soilItemName;
        internal static GearItem planterItem;
        internal static void InitializeIG(ItemDescriptionPage itemDescriptionPage)
        {
            seedText = Localization.Get("GAMEPLAY_IG_SeedExtractLabel");
            fillText = Localization.Get("GAMEPLAY_IG_SoilLabel");
            fertilizeText = Localization.Get("GAMEPLAY_IG_FertilizeLabel");
            cleanText = Localization.Get("GAMEPLAY_IG_CleanLabel");

            GameObject equipButton = itemDescriptionPage.m_MouseButtonEquip;
            seedButton = UnityEngine.Object.Instantiate<GameObject>(equipButton, equipButton.transform.parent, true);
            seedButton.transform.Translate(0.345f, 0, 0);
            Utils.GetComponentInChildren<UILabel>(seedButton).text = seedText;

            fillButton = UnityEngine.Object.Instantiate<GameObject>(equipButton, equipButton.transform.parent, true);
            fillButton.transform.Translate(0.345f, 0, 0);
            Utils.GetComponentInChildren<UILabel>(fillButton).text = fillText;

            cleanButton = UnityEngine.Object.Instantiate<GameObject>(equipButton, equipButton.transform.parent, true);
            cleanButton.transform.Translate(0.345f, 0, 0);
            Utils.GetComponentInChildren<UILabel>(cleanButton).text = cleanText;

            fertilizeButton = UnityEngine.Object.Instantiate<GameObject>(equipButton, equipButton.transform.parent, true);
            fertilizeButton.transform.Translate(0.345f, 0, 0);
            Utils.GetComponentInChildren<UILabel>(fertilizeButton).text = fertilizeText;

            AddAction(seedButton, new System.Action(OnExtract));
            AddAction(fillButton, new System.Action(OnFill));
            AddAction(cleanButton, new System.Action(OnClean));
            AddAction(fertilizeButton, new System.Action(OnFertilize));

            SetExtractActive(false);
            SetFillActive(false);
            SetFertilizeActive(false);
            SetCleanActive(false);
        }
        private static void AddAction(GameObject button, System.Action action)
        {
            Il2CppSystem.Collections.Generic.List<EventDelegate> placeHolderList = new Il2CppSystem.Collections.Generic.List<EventDelegate>();
            placeHolderList.Add(new EventDelegate(action));
            Utils.GetComponentInChildren<UIButton>(button).onClick = placeHolderList;
        }
        internal static void SetExtractActive(bool active)
        {
            NGUITools.SetActive(seedButton, active);           
        }
        internal static void SetFillActive(bool active)
        {
            NGUITools.SetActive(fillButton, active);
        }
        internal static void SetFertilizeActive(bool active)
        {
            NGUITools.SetActive(fertilizeButton, active);
        }
        internal static void SetCleanActive (bool active)
        {
            NGUITools.SetActive(cleanButton, active);
        }

        private static void OnExtract()
        {
            var thisGearItem = Buttons.plantGearItem;
            plantGameObject = thisGearItem.gameObject.name;
            seedFromPlant = IGUtils.FindPlant(thisGearItem.name);

            if (thisGearItem == null) return;
            GameAudioManager.PlayGuiConfirm();
            InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(Localization.Get("GAMEPLAY_IG_ExtractProgressBar"), 5f, 0f, 0f,
                            "Play_HarvestingPlants", null, false, true, new System.Action<bool, bool, float>(OnExtractFinished));
            GameManager.GetInventoryComponent().RemoveGearFromInventory(plantGameObject, 1);
        }
        private static void OnExtractFinished(bool success, bool playerCancel, float progress)
        {
            GearItem seedsExtracted = Addressables.LoadAssetAsync<GameObject>("GEAR_Seeds" + seedFromPlant + "Extracted").WaitForCompletion().GetComponent<GearItem>();
            if (seedFromPlant == "Burdock" || seedFromPlant == "Reishi" || seedFromPlant == "Lichen")
            {
                GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(seedsExtracted, 2);
            }
            else
            {
                GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(seedsExtracted, 1);
            }
        }
        private static void OnFill()
        {
            var thisGearItem = Buttons.recipientItem;
            GearItem soilBag = GameManager.GetInventoryComponent().GetBestGearItemWithName("GEAR_SoilBag");

            if (thisGearItem == null) return;
            if (soilBag == null)
            {
                HUDMessage.AddMessage(Localization.Get("GAMEPLAY_IG_NoSoil"));
                GameAudioManager.PlayGUIError();
                return;
            }
            if (thisGearItem.name == "GEAR_RecycledCan")
            {
                recipientItemName = thisGearItem.name;
                if (soilBag.m_StackableItem.m_Units < 1)
                {
                    HUDMessage.AddMessage(Localization.Get("GAMEPLAY_IG_NoSoilSmall"));
                    GameAudioManager.PlayGUIError();
                    return;
                }
                GameAudioManager.PlayGuiConfirm();
                InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(Localization.Get("GAMEPLAY_IG_SoilProgressBar"), 5f, 0f, 0f,
                                "Play_HarvestingAcorns", null, false, true, new System.Action<bool, bool, float>(OnFillFinished));
                GameManager.GetInventoryComponent().RemoveGearFromInventory(soilBag.name, 1);
                GearItem.Destroy(thisGearItem);
            }
            else if (thisGearItem.name == "GEAR_CookingPot")
            {
                recipientItemName = thisGearItem.name;
                if (soilBag.m_StackableItem.m_Units < 2)
                {
                    HUDMessage.AddMessage(Localization.Get("GAMEPLAY_IG_NoSoilMedium"));
                    GameAudioManager.PlayGUIError();
                    return;
                }
                GameAudioManager.PlayGuiConfirm();
                InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(Localization.Get("GAMEPLAY_IG_SoilProgressBar"), 5f, 0f, 0f,
                                "Play_HarvestingAcorns", null, false, true, new System.Action<bool, bool, float>(OnFillFinished));
                GameManager.GetInventoryComponent().RemoveGearFromInventory(soilBag.name, 2);
                GearItem.Destroy(thisGearItem);
            }
            else
            {
                recipientItemName = thisGearItem.name;
                if (soilBag.m_StackableItem.m_Units < 3)
                {
                    HUDMessage.AddMessage(Localization.Get("GAMEPLAY_IG_NoSoilLarge"));
                    GameAudioManager.PlayGUIError();
                    return;
                }
                GameAudioManager.PlayGuiConfirm();
                InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(Localization.Get("GAMEPLAY_IG_SoilProgressBar"), 5f, 0f, 0f,
                                "Play_HarvestingAcorns", null, false, true, new System.Action<bool, bool, float>(OnFillFinished));
                GameManager.GetInventoryComponent().RemoveGearFromInventory(soilBag.name, 3);
                GearItem.Destroy(thisGearItem);
            }
        }
        private static void OnFillFinished(bool success, bool playerCancel, float progress)
        {
            if (recipientItemName == "GEAR_RecycledCan")
            {
                GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(IGUtils.dirtItem1, 1);
            }
            else if (recipientItemName == "GEAR_CookingPot")
            {
                GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(IGUtils.dirtItem2, 1);
            }
            else if (recipientItemName == "GEAR_HumidKit")
            {
                GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(IGUtils.dirtItem3, 1);
            }
            else
            {
                GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(IGUtils.dirtItem4, 1);
            }
        }
        private static void OnFertilize()
        {
            var thisGearItem = soilItem;
            soilItemName = soilItem.name;
            if (thisGearItem == null) return;
            if (GameManager.GetInventoryComponent().GearInInventory(IGUtils.fertilizeItem2, 1))
            {
                GameAudioManager.PlayGuiConfirm();
                InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(Localization.Get("GAMEPLAY_IG_FertilizeProgressBar"), 5f, 0f, 0f,
                                "Play_HarvestingAcorns", null, false, true, new System.Action<bool, bool, float>(OnFertilizeFinished));
                GameManager.GetInventoryComponent().RemoveGearFromInventory(IGUtils.fertilizeItem2, 1);
                GearItem.Destroy(thisGearItem);
            }
            else if (GameManager.GetInventoryComponent().GearInInventory(IGUtils.fertilizeItem1, 1))
            {
                GameAudioManager.PlayGuiConfirm();
                InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(Localization.Get("GAMEPLAY_IG_FertilizeProgressBar"), 5f, 0f, 0f,
                                "Play_HarvestingAcorns", null, false, true, new System.Action<bool, bool, float>(OnFertilizeFinished));
                GameManager.GetInventoryComponent().RemoveGearFromInventory(IGUtils.fertilizeItem1, 1);
                GearItem.Destroy(thisGearItem);
            }
            else
            {
                HUDMessage.AddMessage(Localization.Get("GAMEPLAY_IG_NoFertilizer"));
                GameAudioManager.PlayGUIError();
            }
        }
        private static void OnFertilizeFinished(bool success, bool playerCancel, float progress)
        {
            if (soilItemName == "GEAR_DirtCan")
            {
                GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(IGUtils.soilItem1, 1);
            }
            else if (soilItemName == "GEAR_DirtPot")
            {
                GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(IGUtils.soilItem2, 1);
            }
            else if (soilItemName == "GEAR_DirtHumid")
            {
                GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(IGUtils.soilItem3, 1);
            }
            else
            {
                GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(IGUtils.soilItem4, 1);
            }
        }
        private static void OnClean()
        {

        }
    }
}
