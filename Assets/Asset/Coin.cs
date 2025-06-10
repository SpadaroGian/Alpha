using UnityEngine;
using TMPro; // Required for TextMeshPro

public class Coin : MonoBehaviour
{
    public int coinValue = 1; // How many points this coin is worth
    public AudioClip collectSound; // Optional: Sound to play when collected
    public GameObject collectEffectPrefab; // Optional: Particle effect to play

    private CoinManager coinManager; // Reference to our CoinManager

    void Start()
    {
        // Find the CoinManager in the scene
        coinManager = FindObjectOfType<CoinManager>();
        if (coinManager == null)
        {
            Debug.LogError("CoinManager not found in the scene! Please add a CoinManager script to an empty GameObject.");
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

            // Destroy the coin object
            Destroy(gameObject);
        }
    }
}