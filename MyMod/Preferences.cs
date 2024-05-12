using System;
using BoneLib.BoneMenu;
using BoneLib.BoneMenu.Elements;
using MelonLoader;

namespace Lucian
{
    public class Preferences : MelonMod
    {

        public static MelonPreferences_Category MelonPrefCategory { get; private set; }

        public static MelonPreferences_Entry<bool> MelonPrefEnabled { get; private set; }

        public static bool IsEnabled { get; private set; }

        
        public static MelonPreferences_Entry<bool> MelonPrefVehicleDismount { get; private set; }
    
        public static bool VehicleDismount { get; private set; }

        public static MelonPreferences_Entry<bool> MelonPrefGunEject { get; private set; }

        public static MelonPreferences_Entry<bool> MelonPrefSlomo { get; private set; }
     
        public static bool Slomo { get; private set; }

        public static MelonPreferences_Entry<float> MelonPrefUI { get; private set; }
     
        public static MelonPreferences_Entry<int> MelonPrefFOV { get; private set; }

        public static MelonPreferences_Entry<int> MelonPrefDismountKey { get; private set; }

        public static MelonPreferences_Entry<int> MelonPrefEjectKey { get; private set; }

        public static MelonPreferences_Entry<int> MelonPrefSlomoKey { get; private set; }
        public static void BonemenuCreator()
        {
            Preferences.MelonPrefCategory = MelonPreferences.CreateCategory("Fling");

            Preferences.MelonPrefEnabled = Preferences.MelonPrefCategory.CreateEntry<bool>("Mod Enabled", false, null, null, false, false, null, null);

            Preferences.IsEnabled = Preferences.MelonPrefEnabled.Value;

            Preferences.MelonPrefVehicleDismount = Preferences.MelonPrefCategory.CreateEntry<bool>("Dismount Enabled", true, null, null, false, false, null, null);

            Preferences.VehicleDismount = Preferences.MelonPrefVehicleDismount.Value;

            Preferences.MelonPrefGunEject = Preferences.MelonPrefCategory.CreateEntry<bool>("Eject Enabled", true, null, null, false, false, null, null);

            Preferences.MelonPrefSlomo = Preferences.MelonPrefCategory.CreateEntry<bool>("Slomo Enabled", true, null, null, false, false, null, null);

            Preferences.Slomo = Preferences.MelonPrefSlomo.Value;

            Preferences.MelonPrefUI = Preferences.MelonPrefCategory.CreateEntry<float>("UI Distance", 0f, null, null, false, false, null, null);

            Preferences.MelonPrefFOV = Preferences.MelonPrefCategory.CreateEntry<int>("FOV", 90, null, null, false, false, null, null);

            Preferences.MelonPrefDismountKey = Preferences.MelonPrefCategory.CreateEntry<int>("Dismount Keycode", 120, null, null, false, false, null, null);

            Preferences.MelonPrefEjectKey = Preferences.MelonPrefCategory.CreateEntry<int>("Eject Keycode", 118, null, null, false, false, null, null);

            Preferences.MelonPrefSlomoKey = Preferences.MelonPrefCategory.CreateEntry<int>("Slomo Keycode", 99, null, null, false, false, null, null);

            MenuCategory menuCategory = MenuManager.CreateCategory("Lucian's PC Extra's", "#ff8000");

            menuCategory.CreateBoolElement("Mod Toggle", "#ff8000", Preferences.IsEnabled, new Action<bool>(Preferences.OnSetEnabled));

            menuCategory.CreateBoolElement("Vehicle Dismount", "#ff8000", Preferences.VehicleDismount, new Action<bool>(Preferences.OnSetDismount));

            menuCategory.CreateBoolElement("Slowmo", "#ff8000", Preferences.Slomo, new Action<bool>(Preferences.OnSetSlomo));

            menuCategory.CreateFloatElement("UI Distance", "#ff8000", Main.UIDistance, 0.2f, -3f, 0f, delegate (float ud)
            {
                Main.UIDistance = ud;
                Preferences.MelonPrefUI.Value = ud;
                Preferences.MelonPrefCategory.SaveToFile(true);
                Main.ChangeUIDistance();
            });

            menuCategory.CreateIntElement("FOV", "#ff8000", Main.FOV, 5, 30, 120, delegate (int f)
            {
                Main.FOV = f;
                Preferences.MelonPrefFOV.Value = f;
                Preferences.MelonPrefCategory.SaveToFile(true);
                Main.ChangeFOV();
            });

            MenuCategory menuCategory2 = menuCategory.CreateCategory("Controls", "#ff8000");
            menuCategory2.CreateIntElement("Dismount Key", "#ff8000", Main.DismountKey, 1, 0, 509, delegate (int dk)
            {
                Main.DismountKey = dk;
                Preferences.MelonPrefDismountKey.Value = dk;
                Preferences.MelonPrefCategory.SaveToFile(true);
            }); 
            
            menuCategory2.CreateIntElement("Slomo Key", "#ff8000", Main.SlomoKey, 1, 0, 509, delegate (int sk)
            {
                Main.SlomoKey = sk;
                Preferences.MelonPrefSlomoKey.Value = sk;
                Preferences.MelonPrefCategory.SaveToFile(true);
            });

        }

        public static void OnSetEnabled(bool value)
        {
            Preferences.IsEnabled = value;
            Preferences.MelonPrefEnabled.Value = value;
            Preferences.MelonPrefCategory.SaveToFile(true);
        }

        public static void OnSetDismount(bool value)
        {
            Preferences.VehicleDismount = value;
            Preferences.MelonPrefVehicleDismount.Value = value;
            Preferences.MelonPrefCategory.SaveToFile(true);
        }

        public static void OnSetSlomo(bool value)
        {
            Preferences.Slomo = value;
            Preferences.MelonPrefSlomo.Value = value;
            Preferences.MelonPrefCategory.SaveToFile(true);
        }
    }
}
