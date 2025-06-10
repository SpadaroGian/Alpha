using UnityEngine;
using TMPro; // Required for TextMeshPro

public class Coin : MonoBehaviour
{
    public int coinValue = 1; // How many points this coin is worth
    public AudioClip collectSound; // Optional: Sound to play when collected
    public GameObject collectEffectPrefab; // Optional: Particle effect to play

    [Header("Camera Blink Effect")]
    public bool useCameraBlink = false; // Enable/disable blink effect for this coin
    public float blinkDuration = 5f;    // Total duration of the blink effect
    public float blinkInterval = 0.1f;  // Time for each on/off state during blink

    [Header("Camera Shake Effect")]
    public bool useCameraShake = false; // Enable/disable shake effect for this coin
    public float shakeDuration = 5f;    // Duration of the shake effect
    public float shakeMagnitude = 0.1f; // Magnitude of the shake effect

    [Header("Camera Zoom Burst Effect")]
    public bool useCameraZoomBurst = false; // Enable/disable zoom burst for this coin
    public float zoomInDuration = 0.2f;    // How fast it zooms in (e.g., 0.1 - 0.5s)
    public float zoomOutDuration = 1.0f;   // How fast it zooms out (e.g., 0.5 - 2.0s)
    public float targetZoomFOV = 30f;      // The FOV to zoom to (smaller = more zoomed in)
    [Min(1)] // Ensures the value is at least 1 in the inspector
    public int zoomBurstRepetitions = 1;   // How many times the zoom in/out cycle repeats

    [Header("GameObject Flicker Effect")]
    public bool useGameObjectFlicker = false; // Enable/disable this effect for this coin
    public GameObject flickerPrefab;          // Assign the prefab you want to appear/disappear here!
    public float flickerEffectDuration = 3f;  // Total time the flickering lasts
    public float flickerToggleInterval = 0.1f; // How fast the object toggles on/off
    public float flickerObjectDistance = 2f; // New: Distance in front of the camera to spawn the object

    private CoinManager coinManager; // Reference to our CoinManager
    private CameraBlink cameraBlink; // Reference to the CameraBlink script
    private CameraShake cameraShake; // Reference to the CameraShake script
    private CameraZoomBurst cameraZoomBurst; // Reference to the CameraZoomBurst script
    private GameObjectFlicker gameObjectFlicker; // Reference to the new GameObjectFlicker script
    private GameObject mainCameraGameObject; // Reference to the main camera GameObject

    void Start()
    {
        // Find the CoinManager in the scene
        coinManager = FindObjectOfType<CoinManager>();
        if (coinManager == null)
        {
            Debug.LogError("CoinManager not found in the scene! Please add a CoinManager script to an empty GameObject.");
        }

        // Find the Camera effects scripts on the main camera or a designated manager object
        // It's good practice to attach these to a central manager or the MainCamera.
        mainCameraGameObject = GameObject.FindGameObjectWithTag("MainCamera"); // Get reference to MainCamera GameObject
        if (mainCameraGameObject != null)
        {
            cameraBlink = mainCameraGameObject.GetComponent<CameraBlink>();
            cameraShake = mainCameraGameObject.GetComponent<CameraShake>();
            cameraZoomBurst = mainCameraGameObject.GetComponent<CameraZoomBurst>();
            gameObjectFlicker = mainCameraGameObject.GetComponent<GameObjectFlicker>(); // Get the new component
        }
        else
        {
            Debug.LogWarning("No GameObject with 'MainCamera' tag found to host camera/effect scripts. Ensure your Main Camera has the tag or you have a dedicated Game Effects Manager.");
        }


        // Log warnings if effects are enabled but their managing script is not found
        if (useCameraBlink && cameraBlink == null)
        {
            Debug.LogWarning("CameraBlink script not found! Camera blink effect for this coin will not play.");
        }
        if (useCameraShake && cameraShake == null)
        {
            Debug.LogWarning("CameraShake script not found! Camera shake effect for this coin will not play.");
        }
        if (useCameraZoomBurst && cameraZoomBurst == null)
        {
            Debug.LogWarning("CameraZoomBurst script not found! Camera zoom burst effect for this coin will not play.");
        }
        if (useGameObjectFlicker && gameObjectFlicker == null) // New warning for GameObject Flicker
        {
            Debug.LogWarning("GameObjectFlicker script not found! GameObject flicker effect for this coin will not play.");
        }
        if (useGameObjectFlicker && flickerPrefab == null) // Warning if prefab is not assigned
        {
             Debug.LogWarning("Flicker Prefab not assigned for Coin: " + name + ". GameObject flicker effect will not play.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the collider entering the trigger is the player
        if (other.CompareTag("Player")) // Make sure your player GameObject has the "Player" tag
        {
            // Add coin to the manager
            if (coinManager != null)
            {
                coinManager.AddCoin(coinValue);
            }

            // Play collect sound (if assigned)
            if (collectSound != null)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
            }

            // Instantiate collect effect (if assigned)
            if (collectEffectPrefab != null)
            {
                Instantiate(collectEffectPrefab, transform.position, Quaternion.identity);
            }

            // Trigger camera effects based on coin settings
            if (useCameraBlink && cameraBlink != null)
            {
                cameraBlink.StartBlink(blinkDuration, blinkInterval);
            }

            if (useCameraShake && cameraShake != null)
            {
                cameraShake.StartShake(shakeDuration, shakeMagnitude);
            }

            if (useCameraZoomBurst && cameraZoomBurst != null)
            {
                cameraZoomBurst.StartZoomBurst(zoomInDuration, zoomOutDuration, targetZoomFOV, zoomBurstRepetitions);
            }

            // Trigger the new GameObject Flicker effect if enabled and prefab is assigned
            if (useGameObjectFlicker && gameObjectFlicker != null && flickerPrefab != null)
            {
                // The flickering object will now appear in front of the main camera
                if (mainCameraGameObject != null)
                {
                    Vector3 spawnPosition = mainCameraGameObject.transform.position + mainCameraGameObject.transform.forward * flickerObjectDistance;
                    gameObjectFlicker.StartFlicker(flickerPrefab, spawnPosition, flickerEffectDuration, flickerToggleInterval);
                }
                else
                {
                    Debug.LogWarning("Main Camera GameObject not found, cannot start flicker effect in front of camera.");
                }
            }

            // Destroy the coin object
            Destroy(gameObject);
        }
    }
}
