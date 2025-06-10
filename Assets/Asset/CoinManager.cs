using UnityEngine;
using TMPro; // Required for TextMeshPro

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance { get; private set; } // Singleton pattern for easy access

    public TextMeshProUGUI coinCountText; // Assign your UI TextMeshPro object here
    public int currentCoins = 0;

    [Header("Objects to Disable at specific coin counts")]
    public GameObject objectToDisableAt5Coins;   // Assign in Inspector
    public GameObject objectToDisableAt10Coins;  // Assign in Inspector
    public GameObject objectToDisableAt15Coins;  // Assign in Inspector

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
        CheckAndDisableObjects(); // Check initial state in case coins are loaded from save
    }

    public void AddCoin(int amount)
    {
        currentCoins += amount;
        UpdateCoinCountText();
        Debug.Log("Medicine: " + currentCoins);
        CheckAndDisableObjects(); // Check objects every time coins are added
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

    private void CheckAndDisableObjects()
    {
        if (currentCoins >= 1 && objectToDisableAt5Coins != null)
        {
            objectToDisableAt5Coins.SetActive(false); // Deactivate the GameObject
        }

        if (currentCoins >= 5 && objectToDisableAt10Coins != null)
        {
            objectToDisableAt10Coins.SetActive(false); // Deactivate the GameObject
        }

        if (currentCoins >= 10 && objectToDisableAt15Coins != null)
        {
            objectToDisableAt15Coins.SetActive(false); // Deactivate the GameObject
        }
    }
}