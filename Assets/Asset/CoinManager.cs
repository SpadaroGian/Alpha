using UnityEngine;
using TMPro; // Required for TextMeshPro

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance { get; private set; } // Singleton pattern for easy access

    public TextMeshProUGUI coinCountText; // Assign your UI TextMeshPro object here
    private int currentCoins = 0;

    void Awake()
    {
        // Implement singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: if you want the coin count to persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateCoinCountText(); // Initialize the UI text
    }

    public void AddCoin(int amount)
    {
        currentCoins += amount;
        UpdateCoinCountText();
        Debug.Log("Medicine: " + currentCoins);
    }

    public int GetCurrentCoins()
    {
        return currentCoins;
    }

    private void UpdateCoinCountText()
    {
        if (coinCountText != null)
        {
            coinCountText.text = "Medicine: " + currentCoins.ToString();
        }
    }
}