using BepInEx;
using GorillaNetworking;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;


/*
 * --CREDITS--
 * pixi (@1xpixi) - base UI
 * gold (@goldzxcv) - mods 
 * --END OF CREDITS--
 */




namespace Zotac
{
    [BepInPlugin("gold.zotac.ui", "ZotacUI", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        private bool showMenu = true;
        private int toolbarIndex = 0;
        string text = "";
        private string[] toolbarLabels = { "Mods", "Settings", "Computer", "Credits" };
        private Rect windowRect = new Rect(20, 20, 390, 360);
        private Vector2 scrollPosition = Vector2.zero;
        string[] mods = {
            "WASD Movement"
        };


        //button vars
        bool wasd = false;

        void Update()
        {
            if (wasd)
            {
                mods[0] = "<color=magenta>WASD Movement</color>";

            }
            else
            {
                mods[0] = "WASD Movement";
            }




            if (Keyboard.current.tabKey.wasPressedThisFrame)
            {
                showMenu = !showMenu;
            }
        }

        void OnGUI()
        {
            GUI.color = Color.Lerp(Color.magenta, Color.white, Mathf.PingPong(Time.time, .7f));
            if (showMenu)
            {
                windowRect = GUILayout.Window(0, windowRect, ModMenuWindow, "Zotac | v1.0 | [TAB]");
            }
        }

        void ModMenuWindow(int windowID)
        {
            toolbarIndex = GUILayout.Toolbar(toolbarIndex, toolbarLabels);

            switch (toolbarIndex)
            {
                case 0: //mods section
                    DisplayMods();
                    break;
                case 1: //settings section
                    DisplaySettings();
                    break;
                case 2: //computer settings
                    DisplayComputer();
                    break;
                case 3: //credits section
                    DisplayCredits();
                    break;
            }

            GUI.DragWindow(new Rect(0, 0, 5000, 5000));
        }

        void DisplayMods()
        {
            GUILayout.Label("This is where you can toggle mods.");

            scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, true);

            if (GUILayout.Button(mods[0]))
            {
                wasd = !wasd;
            }

            GUILayout.EndScrollView();
        }

        void DisplaySettings()
        {
            GUILayout.Label("Adjust settings for mods here.");

            scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, true);


            GUILayout.EndScrollView();
        }

        void DisplayComputer()
        {
            GUILayout.Label("You can use computer mods here.");

            scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, true);

            text = GUILayout.TextArea(text);
            if(GUILayout.Button("Join Room"))
            {
                PhotonNetworkController.Instance.AttemptToJoinSpecificRoom(text);
            }

            if (GUILayout.Button("Leave Room"))
            {
                PhotonNetworkController.Instance.AttemptDisconnect();
            }

            GUILayout.EndScrollView();
        }

        void DisplayCredits()
        {
            GUILayout.Label("Having issues? Contact @goldzxcv on Discord.");

            scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, true);

            GUILayout.Label("\npixi (@1xpixi) - base UI\ngold (@goldzxcv) - mods\n\n\nSource code at: https://github.com/goldzxz/Zotac-GTag");
            if(GUILayout.Button("Join Discord"))
            {
                Process.Start("https://discord.gg/zCGA7TvSMW");
            }

            GUILayout.EndScrollView();
        }
    }
}
