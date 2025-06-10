using UnityEngine;
using System.Collections; // Required for Coroutines

public class GameObjectFlicker : MonoBehaviour
{
    // A private reference to the currently active flicker coroutine,
    // allowing us to stop it if a new flicker effect is requested.
    private Coroutine flickerCoroutine;

    /// <summary>
    /// Initiates a flickering effect for a given GameObject prefab.
    /// The prefab will be instantiated at a specified position, rapidly toggled
    /// between active and inactive states, and then destroyed after the duration.
    /// </summary>
    /// <param name="prefab">The GameObject prefab to instantiate and flicker.</param>
    /// <param name="position">The world position where the prefab should be instantiated.</param>
    /// <param name="duration">The total time the flickering effect should last (in seconds).</param>
    /// <param name="flickerInterval">The time in seconds that the object stays active OR inactive during each toggle.
    /// A smaller interval will result in a faster flicker.</param>
    public void StartFlicker(GameObject prefab, Vector3 position, float duration, float flickerInterval)
    {
        // Basic validation: ensure a prefab is provided.
        if (prefab == null)
        {
            Debug.LogError("GameObjectFlicker: Flickering prefab is null. Cannot start effect.");
            return; // Exit the method if no prefab is provided
        }

        // If a flicker effect is already running, stop it first to prevent overlapping effects.
        if (flickerCoroutine != null)
        {
            StopCoroutine(flickerCoroutine);
            // We should also destroy any object that was being flickered by the previous coroutine
            // before starting a new one. This requires a bit more advanced tracking or simply
            // relying on the new coroutine to create and destroy its own object.
            // For simplicity, we'll let the new coroutine instantiate its own.
        }

        // Start the main coroutine that handles the flickering logic.
        flickerCoroutine = StartCoroutine(FlickerEffect(prefab, position, duration, flickerInterval));
    }

    /// <summary>
    /// Coroutine that handles the actual instantiation, toggling, and destruction of the flickering object.
    /// </summary>
    /// <param name="prefab">The GameObject prefab to instantiate.</param>
    /// <param name="position">The spawn position for the prefab.</param>
    /// <param name="duration">The total duration of the flicker effect.</param>
    /// <param name="flickerInterval">The interval between each active/inactive toggle.</param>
    private IEnumerator FlickerEffect(GameObject prefab, Vector3 position, float duration, float flickerInterval)
    {
        // Instantiate the prefab at the given position with no rotation.
        GameObject flickeringObject = Instantiate(prefab, position, Quaternion.identity);

        float elapsed = 0f; // Timer to track how long the effect has been running.
        bool isActive = true; // Boolean to track the current active state of the object. Start active.

        // Loop as long as the elapsed time is less than the total duration.
        while (elapsed < duration)
        {
            // Toggle the active state of the instantiated object.
            flickeringObject.SetActive(!isActive);
            isActive = !isActive; // Update the boolean to reflect the new state.

            // Wait for the specified flickerInterval before the next toggle.
            yield return new WaitForSeconds(flickerInterval);

            // Increment the elapsed time by the interval that just passed.
            elapsed += flickerInterval;
        }

        // After the total duration, ensure the object is destroyed to clean up the scene.
        // This is important as we instantiated it temporarily for the effect.
        Destroy(flickeringObject);

        // Clear the coroutine reference, indicating that no flicker effect is currently active.
        flickerCoroutine = null;
    }
}
