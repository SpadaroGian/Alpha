using UnityEngine;
using System.Collections; // Required for Coroutines

public class CameraBlink : MonoBehaviour
{
    // Reference to the main camera component
    private Camera mainCamera;

    // Store original camera settings to restore after the blink effect
    private CameraClearFlags originalClearFlags;
    private Color originalBackgroundColor;

    // Coroutine reference to stop the blink if a new one starts
    private Coroutine blinkCoroutine;

    void Awake()
    {
        // Get the Camera component attached to this GameObject (assumes this script is on the main camera)
        mainCamera = GetComponent<Camera>();
        if (mainCamera == null)
        {
            Debug.LogError("CameraBlink: No Camera component found on this GameObject. This script must be attached to a Camera.");
            enabled = false; // Disable this script if no camera is found
        }
    }

    void Start()
    {
        // Store the original camera settings when the script starts
        if (mainCamera != null)
        {
            originalClearFlags = mainCamera.clearFlags;
            originalBackgroundColor = mainCamera.backgroundColor;
        }
    }

    /// <summary>
    /// Initiates a camera blinking effect by rapidly toggling the camera's background to black.
    /// The 'blinkInterval' parameter now controls the duration of each black screen or normal view.
    /// </summary>
    /// <param name="duration">How long the total effect should last in seconds.</param>
    /// <param name="blinkInterval">The time in seconds that the screen stays black OR normal during each toggle.</param>
    public void StartBlink(float duration, float blinkInterval)
    {
        // Ensure we have a camera to work with
        if (mainCamera == null)
        {
            Debug.LogError("CameraBlink: Cannot start blink, Camera component is null.");
            return;
        }

        // If a blink effect is already active, stop the previous one before starting a new one
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
            // Ensure camera is restored to original state immediately when stopping a previous blink
            mainCamera.clearFlags = originalClearFlags;
            mainCamera.backgroundColor = originalBackgroundColor;
        }

        // Start the new blink coroutine
        blinkCoroutine = StartCoroutine(Blink(duration, blinkInterval));
    }

    /// <summary>
    /// Coroutine that handles the actual black screen blinking by manipulating camera properties.
    /// </summary>
    /// <param name="duration">The total time the effect should last.</param>
    /// <param name="blinkInterval">The time to wait between turning the screen black and back to normal.</param>
    private IEnumerator Blink(float duration, float blinkInterval)
    {
        float elapsed = 0f; // Initialize a timer to track how long the effect has been running
        bool isBlack = false; // Track if the camera is currently showing black

        // Loop continues as long as the elapsed time is less than the total duration
        while (elapsed < duration)
        {
            if (!isBlack)
            {
                // Set camera to solid black
                mainCamera.clearFlags = CameraClearFlags.SolidColor;
                mainCamera.backgroundColor = Color.black;
                isBlack = true;
            }
            else
            {
                // Restore original camera settings
                mainCamera.clearFlags = originalClearFlags;
                mainCamera.backgroundColor = originalBackgroundColor;
                isBlack = false;
            }

            // Wait for the specified blinkInterval before the next toggle
            yield return new WaitForSeconds(blinkInterval);

            // Increment the elapsed time by the interval that just passed
            elapsed += blinkInterval;
        }

        // After the total duration, ensure the camera is restored to its original settings
        mainCamera.clearFlags = originalClearFlags;
        mainCamera.backgroundColor = originalBackgroundColor;
        // Clear the coroutine reference, indicating that no blink is currently active
        blinkCoroutine = null;
    }
}