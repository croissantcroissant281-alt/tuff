using BepInEx;
using UnityEngine;

namespace MyUniqueModMenu
{
    [BepInPlugin("org.yourname.gorillatag.uniquemenu", "My Unique Mod Menu", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        private bool isMenuVisible = false;

        void Update()
        {
            // Toggle menu with Q key or secondary input
            if (UnityInput.GetKeyDown(KeyCode.Q))
            {
                isMenuVisible = !isMenuVisible;
            }
        }

        void OnGUI()
        {
            if (!isMenuVisible) return;

            // Draw a custom box for your unique menu
            GUI.Box(new Rect(30, 30, 250, 200), "My Custom Gorilla Tag Menu");
            
            GUI.Label(new Rect(40, 60, 200, 25), "Status: Active");

            if (GUI.Button(new Rect(40, 100, 180, 30), "Custom Button"))
            {
                Debug.Log("Custom menu button pressed!");
            }
        }
    }
}
