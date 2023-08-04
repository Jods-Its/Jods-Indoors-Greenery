using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine;

namespace IndoorsGreenery
{
    internal class IGUtils
    {
        public static string[] plantGearList = { "GEAR_Burdock", "GEAR_ReishiMushroom", "GEAR_Blueberries", "GEAR_Berrysoap", "GEAR_OldMansBeardHarvested", "GEAR_HerbLeavesHarvested", "GEAR_RoseHip", "GEAR_Carrot" };
        public static string fertilizeItem1 = "GEAR_PlantNutrients";
        public static string fertilizeItem2 = "GEAR_PlantNutrientsCrafted";
        public static Panel_Inventory inventory;

        public static GearItem recipientItem1 = Addressables.LoadAssetAsync<GameObject>("GEAR_RecycledCan").WaitForCompletion().GetComponent<GearItem>();
        public static GearItem recipientItem2 = Addressables.LoadAssetAsync<GameObject>("GEAR_CookingPot").WaitForCompletion().GetComponent<GearItem>();
        public static GearItem recipientItem3 = Addressables.LoadAssetAsync<GameObject>("GEAR_HumidKit").WaitForCompletion().GetComponent<GearItem>();
        public static GearItem recipientItem4 = Addressables.LoadAssetAsync<GameObject>("GEAR_MetalBoxForge").WaitForCompletion().GetComponent<GearItem>();
        public static GearItem dirtItem1 = Addressables.LoadAssetAsync<GameObject>("GEAR_DirtCan").WaitForCompletion().GetComponent<GearItem>();
        public static GearItem dirtItem2 = Addressables.LoadAssetAsync<GameObject>("GEAR_DirtPot").WaitForCompletion().GetComponent<GearItem>();
        public static GearItem dirtItem3 = Addressables.LoadAssetAsync<GameObject>("GEAR_DirtHumid").WaitForCompletion().GetComponent<GearItem>();
        public static GearItem dirtItem4 = Addressables.LoadAssetAsync<GameObject>("GEAR_DirtBox").WaitForCompletion().GetComponent<GearItem>();
        public static GearItem soilItem1 = Addressables.LoadAssetAsync<GameObject>("GEAR_PlanterSmall").WaitForCompletion().GetComponent<GearItem>();
        public static GearItem soilItem2 = Addressables.LoadAssetAsync<GameObject>("GEAR_PlanterBig").WaitForCompletion().GetComponent<GearItem>();
        public static GearItem soilItem3 = Addressables.LoadAssetAsync<GameObject>("GEAR_PlanterHumid").WaitForCompletion().GetComponent<GearItem>();
        public static GearItem soilItem4 = Addressables.LoadAssetAsync<GameObject>("GEAR_PlanterBox").WaitForCompletion().GetComponent<GearItem>();
        public static GearItem organicWaste = Addressables.LoadAssetAsync<GameObject>("GEAR_DeadMatter").WaitForCompletion().GetComponent<GearItem>();

        public static GearItem startItem1 = Addressables.LoadAssetAsync<GameObject>("GEAR_PlantNutrientsCrafted").WaitForCompletion().GetComponent<GearItem>();
        public static GearItem startItem2 = Addressables.LoadAssetAsync<GameObject>("GEAR_DistilledWater").WaitForCompletion().GetComponent<GearItem>();
        public static GearItem startItem3 = Addressables.LoadAssetAsync<GameObject>("GEAR_SoilBag").WaitForCompletion().GetComponent<GearItem>();
        public static GearItem startItem4 = Addressables.LoadAssetAsync<GameObject>("GEAR_GardeningToolsImprovised").WaitForCompletion().GetComponent<GearItem>();


        public static string FindPlant(string gearItemName)
        {
            string[] seed = { "Burdock", "Reishi", "Blue", "Soap", "Lichen", "Herb", "Rose", "Carrot"};
            int index = -1;
            for (int i = 0;  i < plantGearList.Length; i++)
            {
                if (gearItemName == plantGearList[i])
                {
                    index = i; 
                    break;
                }
            }
            return seed[index];
        }
        public static bool IsPlant(string gearItemName)
        {
            for (int i = 0; i < plantGearList.Length; i++)
            {
                if (gearItemName == plantGearList[i]) return true;
            }
            return false;
        }
        public static bool IsRecipient (string gearItemName)
        {
            string[] recipient = { recipientItem1.name, recipientItem2.name, recipientItem3.name, recipientItem4.name };
            for (int i = 0; i < recipient.Length; i++)
            {
                if (gearItemName == recipient[i]) return true;
            }
            return false;
        }
        public static bool IsSoiled(string gearItemName)
        {
            string[] soil = { dirtItem1.name, dirtItem2.name, dirtItem3.name, dirtItem4.name };
            for (int i = 0; i < soil.Length; i++)
            {
                if (gearItemName == soil[i]) return true;
            }
            return false;
        }
        public static bool IsDead(string gearItemName)
        {
            string[] waste = { "GEAR_DeadPlanterSmall", "GEAR_DeadPlanterBig", "GEAR_DeadPlanterHumid", "GEAR_DeadPlanterBox" };
            for (int i = 0; i < waste.Length; i++)
            {
                if (gearItemName == waste[i]) return true;
            }
            return false;
        }
    }
}
