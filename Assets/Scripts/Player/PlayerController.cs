using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int Coins;
    [SerializeField] private AudioSource audioSource;
    public Action<int> OnCoinAmountChanged;
    [SerializeField] private GameObject collectEffectPrefab;
    [SerializeField] private int initialLives = 1;
    public static event Action OnPlayerDied;

    private int lives;
    private GameHUD coinsCounter;

    [System.Serializable]
    public class PlayerData
    {
        public string Name;
        public int Coins;
    }

    private void Start()
    {
        LoadPlayerData();
        lives = initialLives;
        OnCoinAmountChanged?.Invoke(0);
        coinsCounter = FindObjectOfType<GameHUD>();
    }

    private void LoadPlayerData()
    {
        if (PlayerPrefs.HasKey("Coins"))
        {
            Coins = PlayerPrefs.GetInt("Coins");
        }
        else
        {
            Coins = 0;
        }
    }

    public void SavePlayerData()
    {
        PlayerPrefs.SetInt("Coins", Coins);
        PlayerPrefs.Save();
        Debug.Log("Player data saved: Coins - " + Coins);
    }

    public void ClearPlayerData()
    {
        PlayerPrefs.DeleteKey("Coins");
        PlayerPrefs.Save();
        Debug.Log("Player data cleared");
    }

    public void TakeDamage(int damageAmount)
    {
        lives -= damageAmount;

        if (lives <= 0)
        {
            Coins = 0;
            OnCoinAmountChanged?.Invoke(Coins);
            SavePlayerData();
            ClearPlayerData(); // ќчист≥ть дан≥ при смерт≥ гравц€

            OnPlayerDied?.Invoke();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Catch>(out var cat))
        {
            var coinValue = cat.Collect();
            Coins += coinValue;

            if (audioSource != null)
            {
                audioSource.Play();
            }

            OnCoinAmountChanged?.Invoke(Coins);

            if (coinsCounter != null)
            {
                coinsCounter.UpdateCoinsCount(Coins);
            }
        }

        if (collectEffectPrefab != null)
        {
            Instantiate(collectEffectPrefab, collision.transform.position, Quaternion.identity);
        }
    }
}
