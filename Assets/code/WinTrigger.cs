using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour
{
    [Tooltip("If -1, will wrap back to scene 0 (usually your MainMenu).")]
    public int nextSceneBuildIndex = -1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // 1) Submit to your leaderboard (if you have one)
        if (LeaderboardManager.Instance != null && GameManager.Instance != null)
        {
            LeaderboardManager.Instance.AddEntry(
                playerName: "Player",
                coins: GameManager.Instance.coinsCollected,
                time: GameManager.Instance.elapsedTime
            );
        }

        // 2) Advance scenes
        int current = SceneManager.GetActiveScene().buildIndex;
        int target = nextSceneBuildIndex >= 0
            ? nextSceneBuildIndex
            : current + 1;

        if (target < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(target);
        else
            SceneManager.LoadScene(0);  // wrap to MainMenu
    }
}
