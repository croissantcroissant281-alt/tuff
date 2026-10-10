using BepInEx;
using UnityEngine;

namespace GTagModMenu
{
    [BepInPlugin("com.yourname.gtagmenu", "Test Mod Menu", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        private bool isInMenuOpen = false;
        private bool longArmsEnabled = false;
        private bool platformsEnabled = false;
        private bool flyEnabled = false;

        private GameObject leftPlatform = null;
        private GameObject rightPlatform = null;

        void Update()
        {
            // Toggle menu open/closed using 'F1' key on keyboard (or change as needed)
            if (UnityInput.GetKeyDown(KeyCode.F1))
            {
                isInMenuOpen = !isInMenuOpen;
                Debug.Log("[ModMenu] Menu Open: " + isInMenuOpen);
            }

            // Handle features if enabled
            RunLongArms();
            RunPlatforms();
            RunFly();
        }

        void OnGUI()
        {
            // Simple on-screen visual menu when toggled on
            if (!isInMenuOpen) return;

            GUI.Box(new Rect(50, 50, 250, 220), "Gorilla Tag Mod Menu (Test)");

            // Long Arms Toggle
            if (GUI.Button(new Rect(60, 90, 230, 30), "Long Arms: " + (longArmsEnabled ? "ON" : "OFF")))
            {
                longArmsEnabled = !longArmsEnabled;
            }

            // Platforms Toggle
            if (GUI.Button(new Rect(60, 130, 230, 30), "Platforms: " + (platformsEnabled ? "ON" : "OFF")))
            {
                platformsEnabled = !platformsEnabled;
                if (!platformsEnabled) DestroyPlatforms();
            }

            // Fly Toggle
            if (GUI.Button(new Rect(60, 170, 230, 30), "Fly: " + (flyEnabled ? "ON" : "OFF")))
            {
                flyEnabled = !flyEnabled;
            }
        }

        void RunLongArms()
        {
            try
            {
                if (longArmsEnabled)
                {
                    // Scales the local player's scale/arms if Gorilla Tag classes allow, 
                    // or adjusts the camera rig scale
                    GameObject playerRig = GameObject.Find("GorillaPlayer");
                    if (playerRig != null)
                    {
                        playerRig.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                    }
                }
                else
                {
                    GameObject playerRig = GameObject.Find("GorillaPlayer");
                    if (playerRig != null)
                    {
                        playerRig.transform.localScale = Vector3.one;
                    }
                }
            }
            catch { }
        }

        void RunPlatforms()
        {
            if (platformsEnabled)
            {
                // Basic platform logic checking for trigger inputs or hand positions
                // (Placeholder check using keyboard space or controller buttons)
                if (UnityInput.GetKey(KeyCode.Space))
                {
                    if (leftPlatform == null)
                    {
                        leftPlatform = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        leftPlatform.transform.localScale = new Vector3(0.2f, 0.02f, 0.2f);
                    }
                }
                else
                {
                    DestroyPlatforms();
                }
            }
        }

        void DestroyPlatforms()
        {
            if (leftPlatform != null) { Destroy(leftPlatform); leftPlatform = null; }
            if (rightPlatform != null) { Destroy(rightPlatform); rightPlatform = null; }
        }

        void RunFly()
        {
            if (flyEnabled)
            {
                // Hold 'F' or use triggers to move forward/fly up
                Rigidbody rb = GetComponent<Rigidbody>();
                if (rb == null && Camera.main != null)
                {
                    // Simple camera-based movement simulation
                    if (UnityInput.GetKey(KeyCode.W))
                    {
                        Camera.main.transform.position += Camera.main.transform.forward * Time.deltaTime * 10f;
                    }
                }
            }
        }
    }
}
