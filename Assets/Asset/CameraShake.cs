using UnityEngine;
using System.Collections; // Required for Coroutines

public class CameraShake : MonoBehaviour
{
    // Original position of the camera
    private Vector3 originalPosition;
    // Coroutine reference to stop the shake if a new one starts
    private Coroutine shakeCoroutine;

    void Awake()
    {
        // Store the camera's initial position when the script wakes up
        originalPosition = transform.localPosition;
    }

    /// <summary>
    /// Initiates a camera shake effect.
    /// </summary>
    /// <param name="duration">How long the camera should shake in seconds.</param>
    /// <param name="magnitude">The intensity of the shake (how far the camera moves).</param>
    public void StartShake(float duration, float magnitude)
    {
        // If a shake is already active, stop it before starting a new one
        if (shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine);
            // Reset camera to original position before starting new shake to prevent drifting
            transform.localPosition = originalPosition;
        }

        // Start the shake coroutine
        shakeCoroutine = StartCoroutine(Shake(duration, magnitude));
    }

    /// <summary>
    /// Coroutine that handles the actual camera shaking.
    /// </summary>
    /// <param name="duration">The total time the shake should last.</param>
    /// <param name="magnitude">The maximum displacement for the camera during the shake.</param>
    private IEnumerator Shake(float duration, float magnitude)
    {
        float elapsed = 0f; // Time elapsed since the shake started

        // Loop for the duration of the shake
        while (elapsed < duration)
        {
            // Generate random offsets for X and Y based on magnitude
            // Using a circle for shake direction provides a more natural feel
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            // Apply the offset to the camera's local position
            // We use localPosition because the camera might be a child of another object
            transform.localPosition = originalPosition + new Vector3(x, y, 0);

            // Increment elapsed time by the time since the last frame
            elapsed += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // After the shake duration, reset the camera to its original position
        transform.localPosition = originalPosition;
        shakeCoroutine = null; // Clear the coroutine reference
    }
}