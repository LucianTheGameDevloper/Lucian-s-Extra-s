using System;
using BoneLib;
using FlatPlayer;
using MelonLoader;
using SLZ.Interaction;
using SLZ.Marrow.Input;
using SLZ.Props.Weapons;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Lucian
{
	public class Main : MelonMod
	{
        public override void OnInitializeMelon()
        {
            Hooking.OnLevelInitialized += delegate
            {
                OnSceneAwake();
            };
            Preferences.BonemenuCreator();
        }

        public void OnSceneAwake()
        {
            SceneLoaded = true;
            UIDefault = Player.rigManager.uiRig.popUpMenu.dis_uiSpawn_Far;
            ChangeUIDistance();
            ChangeFOV();
        }
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
		{
			GameObject[] array = UnityEngine.Object.FindObjectsOfType<GameObject>();
			MelonLogger.Msg("Starting search for player hands...");
			foreach (GameObject gameObject in array)
			{
				bool flag = gameObject.name == "Hand (left)" && gameObject.layer == LayerMask.NameToLayer("Player");
				if (flag)
				{
					this.lHand = gameObject;
					this.leftInventorySlot = this.lHand.GetComponent<InventorySlot>();
					MelonLogger.Msg("Defined Left Hand.");
				}
				bool flag2 = gameObject.name == "Hand (right)" && gameObject.layer == LayerMask.NameToLayer("Player");
				if (flag2)
				{
					this.rHand = gameObject;
					this.rightInventorySlot = this.rHand.GetComponent<InventorySlot>();
					MelonLogger.Msg("Defined Right Hand.");
				}
			}
		}
        public static void ChangeUIDistance()
        {
            Player.rigManager.uiRig.popUpMenu.dis_uiSpawn_Far = Mathf.Clamp(UIDefault + UIDistance, 0.9f, UIDefault);
        }

        public static void ChangeFOV()
        {
            FlatBooter.mainCamera.fieldOfView = FOV;
        }

        public override void OnUpdate()
		{
            if (Preferences.IsEnabled && SceneLoaded && FlatBooter.isReady)
            {
                if (Input.GetKeyDown((KeyCode)DismountKey))
                {
                    ((XRController)FlatBooter.RightController).JoystickButton = true;
                    ((XRController)FlatBooter.RightController).JoystickButtonDown = true;
                }
                if (Input.GetKeyUp((KeyCode)DismountKey))
                {
                    ((XRController)FlatBooter.RightController).JoystickButton = false;
                    ((XRController)FlatBooter.RightController).JoystickButtonUp = true;
                }
                if (Input.GetKeyDown((KeyCode)SlomoKey))
                {
                    ((XRController)FlatBooter.LeftController).AButton = true;
                    ((XRController)FlatBooter.LeftController).AButtonDown = true;
                }
                if (Input.GetKeyUp((KeyCode)SlomoKey))
                {
                    ((XRController)FlatBooter.LeftController).AButton = true;
                    ((XRController)FlatBooter.LeftController).AButtonUp = true;
                }
                if (Input.GetKeyDown((KeyCode)EjectKey))
                {
                    ((XRController)FlatBooter.LeftController).BButton = true;
                    ((XRController)FlatBooter.LeftController).BButtonDown = true;
                    ((XRController)FlatBooter.RightController).BButton = true;
                    ((XRController)FlatBooter.RightController).BButtonDown = true;
                }
                if (Input.GetKeyUp((KeyCode)EjectKey))
                {
                    ((XRController)FlatBooter.LeftController).BButton = false;
                    ((XRController)FlatBooter.LeftController).BButtonUp = true;
                    ((XRController)FlatBooter.RightController).BButton = false;
                    ((XRController)FlatBooter.RightController).BButtonUp = true;
                }
            }
            Cursor.visible = true;
			bool keyDown = Input.GetKeyDown((KeyCode)120);
			if (keyDown)
			{
                try
                {
                    if ((Object)(object)leftInventorySlot.itemGameObject != (Object)null && (Object)(object)leftInventorySlot.itemGameObject.GetComponent<Gun>() != (Object)null)
                    {
                        Gun component = leftInventorySlot.itemGameObject.GetComponent<Gun>();
                        component.InstantLoad();
                        component.CompleteSlidePull();
                        component.CompleteSlideReturn();
                        component.CompleteSlidePull();
                        component.CompleteSlideReturn();
                    }
                    if ((Object)(object)rightInventorySlot.itemGameObject != (Object)null && (Object)(object)rightInventorySlot.itemGameObject.GetComponent<Gun>() != (Object)null)
                    {
                        Gun component2 = rightInventorySlot.itemGameObject.GetComponent<Gun>();
                        component2.InstantLoad();
                        component2.CompleteSlidePull();
                        component2.CompleteSlideReturn();
                        component2.CompleteSlidePull();
                        component2.CompleteSlideReturn();
                    }
                }
                catch
				{
					MelonLogger.Msg("Could not reload gun.");
				}
			}
		}

		private GameObject lHand;

		private GameObject rHand;

		private InventorySlot leftInventorySlot;

		private InventorySlot rightInventorySlot;

		private string leftShoulderSlot = "slot_target 05";

		private string rightShoulderSlot = "slot_target 06";

		private string leftHipSlot = "slot_target 01";

		private string rightHipSlot = "slot_target 08";

		private string backSlot = "slot_target 07";

        private bool SceneLoaded = false;

        public static float UIDefault;

        public static float UIDistance = 0f;

        public static int DismountKey = 120;

        public static int SlomoKey = 118;

        public static int EjectKey = 99;

        public static int FOV = 90;
    }
}
