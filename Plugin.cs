using BepInEx;
using UnityEngine;

namespace GTagModMenu
{
    [BepInPlugin("com.yourname.gtagmenu", "Test Mod Menu", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        private bool menuOpen = false;
        private bool longArms = false;
        private bool fly = false;

        void Update()
        {
            // Press F1 to toggle the visual menu
            if (UnityInput.GetKeyDown(KeyCode.F1))
            {
                menuOpen = !menuOpen;
            }

            // Long arms logic
            if (longArms)
            {
                GameObject player = GameObject.Find("GorillaPlayer");
                if (player != null) player.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
            }

            // Fly logic
            if (fly && UnityInput.GetKey(KeyCode.Space))
            {
                if (Camera.main != null)
                {
                    Camera.main.transform.position += Camera.main.transform.forward * Time.deltaTime * 12f;
                }
            }
        }

        void OnGUI()
        {
            if (!menuOpen) return;

            GUI.Box(new Rect(40, 40, 220, 180), "My Custom Mod Menu");

            if (GUI.Button(new Rect(50, 80, 200, 30), "Long Arms: " + (longArms ? "ON" : "OFF")))
            {
                longArms = !longArms;
            }

            if (GUI.Button(new Rect(50, 120, 200, 30), "Fly (Space): " + (fly ? "ON" : "OFF")))
            {
                fly = !fly;
            }
        }
    }
}
