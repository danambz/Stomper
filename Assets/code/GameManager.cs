using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private TextMeshProUGUI coinText;
    private TextMeshProUGUI timerText;

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

        // 2) reset counters
        coinsCollected = 0;
        elapsedTime = 0f;

        // 3) push that to screen
        UpdateCoinUI();
        UpdateTimerUI();
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
}
