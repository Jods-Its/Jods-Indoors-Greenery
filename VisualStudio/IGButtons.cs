using Il2Cpp;
using Il2CppNodeCanvas.Tasks.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace IndoorsGreenery
{
    internal class IGButtons
    {
        internal static string seedText;
        internal static string fillText;
        internal static string fertilizeText;
        internal static string cleanText;
        internal static string placeText;
        internal static string emptyText;
        private static GameObject seedButton;
        internal static GameObject fillButton;
        internal static GameObject fillButtonFix;
        private static GameObject cleanButton;
        private static GameObject fertilizeButton;
        private static GameObject placeButton;
        private static GameObject emptyButton;

        private static string seedFromPlant;
        internal static GearItem plantGearItem;
        private static string plantGameObject;

        internal static GearItem recipientItem;
        internal static string recipientItemName;
        internal static GearItem soilItem;
        internal static string soilItemName;
        internal static GearItem planterItem;
        internal static string planterItemName;
        internal static GearItem placeItem;

        internal static void InitializeIG(ItemDescriptionPage itemDescriptionPage)
        {
            seedText = Localization.Get("GAMEPLAY_IG_SeedExtractLabel");
            fillText = Localization.Get("GAMEPLAY_IG_SoilLabel");
            emptyText = Localization.Get("GAMEPLAY_IG_EmptyLabel");
            fertilizeText = Localization.Get("GAMEPLAY_IG_FertilizeLabel");
            cleanText = Localization.Get("GAMEPLAY_IG_CleanLabel");
            placeText = Localization.Get("GAMEPLAY_IG_PlaceLabel");
            

            GameObject equipButton = itemDescriptionPage.m_MouseButtonEquip;
            seedButton = UnityEngine.Object.Instantiate<GameObject>(equipButton, equipButton.transform.parent, true);
            seedButton.transform.Translate(0.345f, 0, 0);
            Utils.GetComponentInChildren<UILabel>(seedButton).text = seedText;

            fillButton = UnityEngine.Object.Instantiate<GameObject>(equipButton, equipButton.transform.parent, true);
            fillButton.transform.Translate(0.345f, 0, 0);
            Utils.GetComponentInChildren<UILabel>(fillButton).text = fillText;

            fillButtonFix = UnityEngine.Object.Instantiate<GameObject>(equipButton, equipButton.transform.parent, true);
            fillButtonFix.transform.Translate(0.345f, -0.1f, 0);
            Utils.GetComponentInChildren<UILabel>(fillButtonFix).text = fillText;

            emptyButton = UnityEngine.Object.Instantiate<GameObject>(equipButton, equipButton.transform.parent, true);
            emptyButton.transform.Translate(0.345f, 0, 0);
            Utils.GetComponentInChildren<UILabel>(emptyButton).text = emptyText;

            cleanButton = UnityEngine.Object.Instantiate<GameObject>(equipButton, equipButton.transform.parent, true);
            cleanButton.transform.Translate(0.345f, 0, 0);
            Utils.GetComponentInChildren<UILabel>(cleanButton).text = cleanText;

            fertilizeButton = UnityEngine.Object.Instantiate<GameObject>(equipButton, equipButton.transform.parent, true);
            Utils.GetComponentInChildren<UILabel>(fertilizeButton).text = fertilizeText;

            placeButton = UnityEngine.Object.Instantiate<GameObject>(equipButton, equipButton.transform.parent, true);
            Utils.GetComponentInChildren<UILabel>(placeButton).text = placeText;

            AddAction(seedButton, new System.Action(OnExtractSeeds));
            AddAction(fillButton, new System.Action(OnFillRecipient));
            AddAction(fillButtonFix, new System.Action(OnFillRecipient));
            AddAction(emptyButton, new System.Action(OnEmptyRecipient));
            AddAction(cleanButton, new System.Action(OnCleanPlanter));
            AddAction(fertilizeButton, new System.Action(OnFertilizePlanter));
            AddAction(placeButton, new System.Action(OnPlacePlanter));

            SetExtractSeedsActive(false);
            SetFillRecipientActive(false);
            SetFillRecipientFixActive(false);
            SetEmptyRecipientActive(false);
            SetFertilizePlanterActive(false);
            SetCleanPlanterActive(false);
            SetPlacePlanterActive(false);
        }
        private static void AddAction(GameObject button, System.Action action)
        {
            Il2CppSystem.Collections.Generic.List<EventDelegate> placeHolderList = new Il2CppSystem.Collections.Generic.List<EventDelegate>();
            placeHolderList.Add(new EventDelegate(action));
            Utils.GetComponentInChildren<UIButton>(button).onClick = placeHolderList;
        }
        internal static void SetExtractSeedsActive(bool active)
        {
            NGUITools.SetActive(seedButton, active);           
        }
        internal static void SetFillRecipientActive(bool active)
        {
            NGUITools.SetActive(fillButton, active);
        }
        internal static void SetFillRecipientFixActive(bool active)
        {
            NGUITools.SetActive(fillButtonFix, active);
        }
        internal static void SetEmptyRecipientActive(bool active)
        {
            NGUITools.SetActive(emptyButton, active);
        }
        internal static void SetFertilizePlanterActive(bool active)
        {
            NGUITools.SetActive(fertilizeButton, active);
        }
        internal static void SetCleanPlanterActive (bool active)
        {
            NGUITools.SetActive(cleanButton, active);
        }
        internal static void SetPlacePlanterActive(bool active)
        {
            NGUITools.SetActive(placeButton, active);
        }
        private static void OnExtractSeeds()
        {
            var thisGearItem = IGButtons.plantGearItem;
            plantGameObject = thisGearItem.gameObject.name;
            seedFromPlant = IGUtils.FindPlant(thisGearItem.name);

            if (thisGearItem == null) return;
            GameAudioManager.PlayGuiConfirm();
            InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(Localization.Get("GAMEPLAY_IG_ExtractProgressBar"), 5f, 0f, 0f,
                            "Play_HarvestingPlants", null, false, true, new System.Action<bool, bool, float>(OnExtractSeedsFinished));
            GameManager.GetInventoryComponent().RemoveGearFromInventory(plantGameObject, 1);
        }
        private static void OnExtractSeedsFinished(bool success, bool playerCancel, float progress)
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
        private static void OnFillRecipient()
        {
            var thisGearItem = IGButtons.recipientItem;
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
                                "Play_HarvestingAcorns", null, false, true, new System.Action<bool, bool, float>(OnFillRecipientFinished));
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
                                "Play_HarvestingAcorns", null, false, true, new System.Action<bool, bool, float>(OnFillRecipientFinished));
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
                                "Play_HarvestingAcorns", null, false, true, new System.Action<bool, bool, float>(OnFillRecipientFinished));
                GameManager.GetInventoryComponent().RemoveGearFromInventory(soilBag.name, 3);
                GearItem.Destroy(thisGearItem);
            }
        }
        private static void OnFillRecipientFinished(bool success, bool playerCancel, float progress)
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
        private static void OnFertilizePlanter()
        {
            var thisGearItem = IGButtons.soilItem;
            soilItemName = thisGearItem.name;
            if (thisGearItem == null) return;
            if (GameManager.GetInventoryComponent().GearInInventory(IGUtils.fertilizeItem2, 1))
            {
                GameAudioManager.PlayGuiConfirm();
                InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(Localization.Get("GAMEPLAY_IG_FertilizeProgressBar"), 5f, 0f, 0f,
                                "Play_HarvestingAcorns", null, false, true, new System.Action<bool, bool, float>(OnFertilizePlanterFinished));
                GameManager.GetInventoryComponent().RemoveGearFromInventory(IGUtils.fertilizeItem2, 1);
                GearItem.Destroy(thisGearItem);
            }
            else if (GameManager.GetInventoryComponent().GearInInventory(IGUtils.fertilizeItem1, 1))
            {
                GameAudioManager.PlayGuiConfirm();
                InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(Localization.Get("GAMEPLAY_IG_FertilizeProgressBar"), 5f, 0f, 0f,
                                "Play_HarvestingAcorns", null, false, true, new System.Action<bool, bool, float>(OnFertilizePlanterFinished));
                GameManager.GetInventoryComponent().RemoveGearFromInventory(IGUtils.fertilizeItem1, 1);
                GearItem.Destroy(thisGearItem);
            }
            else
            {
                HUDMessage.AddMessage(Localization.Get("GAMEPLAY_IG_NoFertilizer"));
                GameAudioManager.PlayGUIError();
            }
        }
        private static void OnFertilizePlanterFinished(bool success, bool playerCancel, float progress)
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
        private static void OnCleanPlanter()
        {
            var thisGearItem = IGButtons.planterItem;
            planterItemName = thisGearItem.name;
            

            if (thisGearItem == null) return;
            GameAudioManager.PlayGuiConfirm();
            InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(Localization.Get("GAMEPLAY_IG_CleanProgressBar"), 5f, 0f, 0f,
                            "Play_HarvestingPlants", null, false, true, new System.Action<bool, bool, float>(OnCleanPlanterFinished));
            GearItem.Destroy(thisGearItem);
        }
        private static void OnCleanPlanterFinished(bool success, bool playerCancel, float progress)
        {
            if (planterItemName == "GEAR_DeadPlanterSmall")
            {
                GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(IGUtils.dirtItem1, 1);
                GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(IGUtils.organicWaste, 1);
            }
            else if (planterItemName == "GEAR_DeadPlanterBig")
            {
                GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(IGUtils.dirtItem2, 1);
                GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(IGUtils.organicWaste, 2);
            }
            else if (planterItemName == "GEAR_DeadPlanterHumid")
            {
                GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(IGUtils.dirtItem3, 1);
                GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(IGUtils.organicWaste, 3);
            }
            else
            {
                GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(IGUtils.dirtItem4, 1);
                GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(IGUtils.organicWaste, 3);
            }
        }
        private static void OnPlacePlanter()
        {
            var toDrop = placeItem?.m_StackableItem?.m_UnitsPerItem ?? 1;
            toDrop = Mathf.Clamp(toDrop, 0, placeItem?.m_StackableItem?.m_Units ?? 1);
            var dropped = placeItem.Drop(toDrop);
            IGUtils.inventory.OnBack(); 
            dropped.PerformAlternativeInteraction();
        }
        private static void OnEmptyRecipient()
        {
            var thisGearItem = IGButtons.soilItem;
            soilItemName = thisGearItem.name;

            if (thisGearItem == null) return;
            GameAudioManager.PlayGuiConfirm();
            InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(Localization.Get("GAMEPLAY_IG_EmptyProgressBar"), 5f, 0f, 0f,
                            "Play_HarvestingAcorns", null, false, true, new System.Action<bool, bool, float>(OnEmptyRecipientFinished));
            GearItem.Destroy(thisGearItem);
        }
        private static void OnEmptyRecipientFinished(bool success, bool playerCancel, float progress)
        {
            //HUDMessage.AddMessage(Localization.Get("GAMEPLAY_IG_SoilLost"));
            if (soilItemName == "GEAR_DirtCan")
            {
                GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(IGUtils.recipientItem1, 1);
                GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(IGUtils.soilBag, 1);
            }
            else if (soilItemName == "GEAR_DirtPot")
            {
                GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(IGUtils.recipientItem2, 1);
                GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(IGUtils.soilBag, 2);
            }
            else if (soilItemName == "GEAR_DirtHumid")
            {
                GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(IGUtils.recipientItem3, 1);
                GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(IGUtils.soilBag, 3);
            }
            else
            {
                GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(IGUtils.recipientItem4, 1);
                GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(IGUtils.soilBag, 3);
            }

        }
    }
}

