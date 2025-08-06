using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private TextMeshProUGUI coinText;
    private TextMeshProUGUI timerText;
    private TextMeshProUGUI highScoreText;
    private int initialHighScore;
    private bool notifiedNewHigh = false;

    [HideInInspector] public int coinsCollected;
    [HideInInspector] public float elapsedTime;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else Destroy(gameObject);
    }

    void OnDestroy()
    {
        if (Instance == this)
            SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // <-- called every time a new scene is loaded (including after death-reload) -->
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 1) grab the new UI elements
        coinText = GameObject.Find("Score point")?.GetComponent<TextMeshProUGUI>();
        timerText = GameObject.Find("time")?.GetComponent<TextMeshProUGUI>();
        highScoreText = GameObject.Find("HighScoreText")?.GetComponent<TextMeshProUGUI>();

        // Grab the starting “best ever” before any coins are collected
        if (LeaderboardManager.Instance != null)
            initialHighScore = LeaderboardManager.Instance.GetHighestCoins();
        else
            initialHighScore = 0;

        // 2) reset counters
        coinsCollected = 0;
        elapsedTime = 0f;

        // 3) push that to screen
        UpdateCoinUI();
        UpdateTimerUI();
        UpdateHighScoreUI();
        notifiedNewHigh = false;
    }

    void Update()
    {
        if (timerText == null) return;

        elapsedTime += Time.deltaTime;
        UpdateTimerUI();
    }

    public void AddCoin(int amount = 1)
    {
        coinsCollected += amount;
        UpdateCoinUI();
        // check new high‐score
        if (!notifiedNewHigh && coinsCollected >= initialHighScore)
        {
            notifiedNewHigh = true;
            AudioManager.Instance.PlaySFX("newHigh");
            if (highScoreText != null)
                highScoreText.text = $"You got the High Score: {initialHighScore}";
        }
    }

    void UpdateCoinUI()
    {
        if (coinText != null)
            coinText.text = $"Coin: {coinsCollected}";
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
            timerText.text = $"Time: {FormatTime(elapsedTime)}";
    }

    string FormatTime(float t)
    {
        int minutes = (int)(t / 60);
        int seconds = (int)(t % 60);
        return $"{minutes:00}:{seconds:00}";
    }

    private void UpdateHighScoreUI()
    {
        if (highScoreText != null)
            highScoreText.text = $"High Score: {initialHighScore}";
    }
}
