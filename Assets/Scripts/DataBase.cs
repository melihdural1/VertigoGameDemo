using UnityEngine;

namespace DefaultNamespace
{
    public static class DataBase
    {
        

        public static int GoldAmount
        {
            get => PlayerPrefs.GetInt("goldAmount", 0);
            set => PlayerPrefs.SetInt("goldAmount", value);
        }
        
        public static int CashAmount
        {
            get => PlayerPrefs.GetInt("cashAmount", 0);
            set => PlayerPrefs.SetInt("cashAmount", value);
        }
        
        public static int HealthShot
        {
            get => PlayerPrefs.GetInt("healthShotAmount", 0);
            set => PlayerPrefs.SetInt("healthShotAmount", value);
        }
        
        public static int AdrenalinShot
        {
            get => PlayerPrefs.GetInt("adrenalinShotAmount", 0);
            set => PlayerPrefs.SetInt("adrenalinShotAmount", value);
        }
        
        public static int MedicalKit
        {
            get => PlayerPrefs.GetInt("medicalKitAmount", 0);
            set => PlayerPrefs.SetInt("medicalKitAmount", value);
        }

        public static void ResetData()
        {
            GoldAmount = 0;
            CashAmount = 0;
            HealthShot = 0;
            AdrenalinShot = 0;
            MedicalKit = 0;
        }

        public static int GetData(InventoryType type)
        {
            var data = type switch
            {
                InventoryType.Gold => GoldAmount,
                InventoryType.Cash => CashAmount,
                InventoryType.Adrenalin => AdrenalinShot,
                InventoryType.Health => HealthShot,
                InventoryType.Medical => MedicalKit,
                _ => 0
            };

            return data;
        }

    }
}