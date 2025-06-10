using UnityEngine;
using System.Collections; // Required for Coroutines

public class CameraZoomBurst : MonoBehaviour
{
    // Reference to the main camera component
    private Camera mainCamera;

    // Store the original field of view to return to after the effect
    private float originalFOV;

    // Coroutine reference to stop the burst if a new one starts
    private Coroutine zoomCoroutine;

    void Awake()
    {
        // Get the Camera component attached to this GameObject (assumes this script is on the main camera)
        mainCamera = GetComponent<Camera>();
        if (mainCamera == null)
        {
            Debug.LogError("CameraZoomBurst: No Camera component found on this GameObject. This script must be attached to a Camera.");
            enabled = false; // Disable this script if no camera is found
        }
    }

    void Start()
    {
        // Store the original FOV when the script starts
        if (mainCamera != null)
        {
            originalFOV = mainCamera.fieldOfView;
        }
    }

    /// <summary>
    /// Initiates a camera zoom burst effect. The camera's FOV will rapidly change
    /// and then smoothly return to its original value, repeating for a specified number of times.
    /// </summary>
    /// <param name="zoomDuration">How long the rapid zoom-in portion lasts for each repetition.</param>
    /// <param name="returnDuration">How long the smooth zoom-out portion lasts for each repetition.</param>
    /// <param name="targetFOV">The target FOV to zoom to. A smaller value means more zoom-in.</param>
    /// <param name="repetitions">The number of times the zoom-in/zoom-out cycle should repeat.</param>
    public void StartZoomBurst(float zoomDuration, float returnDuration, float targetFOV, int repetitions)
    {
        // Ensure we have a camera to work with
        if (mainCamera == null)
        {
            Debug.LogError("CameraZoomBurst: Cannot start zoom burst, Camera component is null.");
            return;
        }

        // If a zoom burst is already active, stop the previous one before starting a new one
        if (zoomCoroutine != null)
        {
            StopCoroutine(zoomCoroutine);
            // Immediately reset to original FOV to avoid glitches if a new burst starts mid-effect
            mainCamera.fieldOfView = originalFOV;
        }

        // Start the new zoom burst coroutine
        zoomCoroutine = StartCoroutine(ZoomBurstEffect(zoomDuration, returnDuration, targetFOV, repetitions));
    }

    /// <summary>
    /// Coroutine that handles the actual camera zoom burst effect, repeating for a given number of times.
    /// </summary>
    private IEnumerator ZoomBurstEffect(float zoomDuration, float returnDuration, float targetFOV, int repetitions)
    {
        // Loop for the specified number of repetitions
        for (int i = 0; i < repetitions; i++)
        {
            float currentFOV = mainCamera.fieldOfView;

            // --- Phase 1: Rapid Zoom In ---
            float elapsed = 0f;
            while (elapsed < zoomDuration)
            {
                // Lerp the FOV towards the target FOV
                mainCamera.fieldOfView = Mathf.Lerp(currentFOV, targetFOV, elapsed / zoomDuration);
                elapsed += Time.deltaTime;
                yield return null; // Wait for the next frame
            }
            // Ensure it hits the target FOV precisely
            mainCamera.fieldOfView = targetFOV;

            // --- Phase 2: Smooth Zoom Out ---
            elapsed = 0f;
            currentFOV = mainCamera.fieldOfView; // Start lerping from the target FOV
            while (elapsed < returnDuration)
            {
                // Lerp the FOV back to the original FOV
                mainCamera.fieldOfView = Mathf.Lerp(currentFOV, originalFOV, elapsed / returnDuration);
                elapsed += Time.deltaTime;
                yield return null; // Wait for the next frame
            }
            // Ensure it hits the original FOV precisely at the end of each cycle
            mainCamera.fieldOfView = originalFOV;
        }

        zoomCoroutine = null; // Clear the coroutine reference after all repetitions are done
    }
}
