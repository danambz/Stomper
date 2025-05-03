<<<<<<< HEAD
﻿using System.Collections;
using UnityEngine;
=======
﻿using UnityEngine;
>>>>>>> b1c1e8a5879243eeed8ea5e039e874adacfccfcc
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour
{
<<<<<<< HEAD
    [Header("Scene / Leaderboard")]
    [Tooltip("If -1, will wrap back to scene 0 (your MainMenu).")]
    public int nextSceneBuildIndex = -1;

    [Header("Slow-Mo Settings")]
    [Tooltip("How slow the game gets (e.g. 0.25 = ¼ speed)")]
    public float slowMoFactor = 0.25f;
    [Tooltip("How long to stay in slow-mo (real seconds)")]
    public float slowMoDuration = 1f;

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
        // keep physics in sync
        Time.fixedDeltaTime = 0.02f * slowMoFactor;

        // 2) Play your win sound
        AudioManager.Instance?.PlaySFX("win");

        // 3) Wait in real time so the slow-mo actually shows
        yield return new WaitForSecondsRealtime(slowMoDuration);

        // 4) Reset time back to normal
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;

        // 5) Submit to leaderboard (if you have one)
        if (LeaderboardManager.Instance != null && GameManager.Instance != null)
        {
            LeaderboardManager.Instance.AddEntry(
                playerName: "Player",
=======
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
>>>>>>> b1c1e8a5879243eeed8ea5e039e874adacfccfcc
                coins: GameManager.Instance.coinsCollected,
                time: GameManager.Instance.elapsedTime
            );
        }

<<<<<<< HEAD
        // 6) Advance to the next scene (or wrap to 0)
        int current = SceneManager.GetActiveScene().buildIndex;
        int target = nextSceneBuildIndex >= 0
                       ? nextSceneBuildIndex
                       : current + 1;
=======
        // 2) Advance scenes
        int current = SceneManager.GetActiveScene().buildIndex;
        int target = nextSceneBuildIndex >= 0
            ? nextSceneBuildIndex
            : current + 1;
>>>>>>> b1c1e8a5879243eeed8ea5e039e874adacfccfcc

        if (target < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(target);
        else
<<<<<<< HEAD
            SceneManager.LoadScene(0);
=======
            SceneManager.LoadScene(0);  // wrap to MainMenu
>>>>>>> b1c1e8a5879243eeed8ea5e039e874adacfccfcc
    }
}
