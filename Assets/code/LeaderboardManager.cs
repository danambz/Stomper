using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

[Serializable]
public class ScoreEntry
{
    public string playerName;
    public int coins;
    public float time;
}

[Serializable]
class ScoreEntryList
{
    public List<ScoreEntry> entries = new List<ScoreEntry>();
}

/// <summary>
/// A singleton manager that keeps a top‐N leaderboard,
/// sorted by highest coins, then fastest time.
/// Persists data to PlayerPrefs as JSON.
/// </summary>
public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager Instance { get; private set; }

    [Header("Settings")]
    [Tooltip("How many entries to keep on the leaderboard.")]
    public int maxEntries = 10;

    // Key used for PlayerPrefs storage
    const string PrefsKey = "LeaderboardData";

    // In‐memory list of entries
    private List<ScoreEntry> entries = new List<ScoreEntry>();

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Load();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Adds a new result to the leaderboard, then trims and saves.
    /// </summary>
    public void AddEntry(string playerName, int coins, float time)
    {
        var newEntry = new ScoreEntry
        {
            playerName = playerName,
            coins = coins,
            time = time
        };

        entries.Add(newEntry);

        // Sort: highest coins first, then lowest time
        entries = entries
            .OrderByDescending(e => e.coins)
            .ThenBy(e => e.time)
            .Take(maxEntries)
            .ToList();

        Save();
    }

    /// <summary>
    /// Returns a copy of the current top entries.
    /// </summary>
    public List<ScoreEntry> GetTopEntries()
    {
        // Return a copy so callers can't mutate our internal list
        return new List<ScoreEntry>(entries);
    }

    /// <summary>
    /// Clears all saved leaderboard data (both memory & PlayerPrefs).
    /// </summary>
    public void ClearLeaderboard()
    {
        entries.Clear();
        PlayerPrefs.DeleteKey(PrefsKey);
    }

    // ────────────────────────────────────────────────────────────────────────────

    private void Load()
    {
        // Load JSON from PlayerPrefs
        string json = PlayerPrefs.GetString(PrefsKey, "");
        if (string.IsNullOrEmpty(json))
            return;

        try
        {
            var wrapper = JsonUtility.FromJson<ScoreEntryList>(json);
            entries = wrapper.entries ?? new List<ScoreEntry>();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to load leaderboard data: {ex}");
            entries = new List<ScoreEntry>();
        }
    }

    private void Save()
    {
        var wrapper = new ScoreEntryList { entries = entries };
        string json = JsonUtility.ToJson(wrapper);
        PlayerPrefs.SetString(PrefsKey, json);
        PlayerPrefs.Save();
    }
}
