using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour
{
    [Header("Scene / Leaderboard")]
    [Tooltip("Exact name of your Menu scene (where the Leaderboard panel lives).")]
    public int nextSceneBuildIndex = -1;

    [Header("Slow-Mo Settings")]
    [Tooltip("How slow the game gets (e.g. 0.25 = ¼ speed)")]
    public float slowMoFactor = 0.25f;
    [Tooltip("How long to stay in slow-mo (real seconds)")]
    public float slowMoDuration = 1f;

    [Header("Name Entry UI")]
    [Tooltip("Drag your NameEntryUI (the panel with InputField + Submit) here")]
    public NameEntryUI nameEntryUI;

    bool _triggered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (_triggered || !other.CompareTag("Player")) return;
        _triggered = true;
        StartCoroutine(WinRoutine());
    }

    IEnumerator WinRoutine()
    {
        // 1) Slow everything down
        Time.timeScale = slowMoFactor;
        Time.fixedDeltaTime = 0.02f * slowMoFactor;

        // 2) Play your win sound
        AudioManager.Instance?.PlaySFX("win");

        // 3) Wait in real time so the slow-mo actually shows
        yield return new WaitForSecondsRealtime(slowMoDuration);

        // 4) Reset time back to normal
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;

        // 5) Ask player for their name
        bool gotName = false;
        string playerName = "Player";
        nameEntryUI.Show(name => {
            playerName = name;
            gotName = true;
        });

        // wait until they hit Submit
        while (!gotName)
            yield return null;

        // 6) Submit to leaderboard
        if (LeaderboardManager.Instance != null && GameManager.Instance != null)
        {
            LeaderboardManager.Instance.AddEntry(
                playerName: playerName,
                coins: GameManager.Instance.coinsCollected,
                time: GameManager.Instance.elapsedTime
            );
        }

        // 6) Advance to the next scene (or wrap to 0)
        int current = SceneManager.GetActiveScene().buildIndex;
        int target = nextSceneBuildIndex >= 0
                       ? nextSceneBuildIndex
                       : current + 1;

        if (target < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(target);
        else
            SceneManager.LoadScene(0);
    }

}
