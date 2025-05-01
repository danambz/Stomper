using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class LeaderboardUI : MonoBehaviour
{
    [Header("UI Prefab & References")]
    [SerializeField] private GameObject entryPrefab;     // LB_Entry prefab
    [SerializeField] private Transform contentParent;   // Content under Viewport

    void Start()
    {
        if (entryPrefab == null || contentParent == null)
        {
            Debug.LogError("[LeaderboardUI] Please assign Entry Prefab and Content Parent in the Inspector.");
            return;
        }

        if (LeaderboardManager.Instance == null)
        {
            Debug.LogError("[LeaderboardUI] No LeaderboardManager found in scene.");
            return;
        }

        Populate();
    }

    public void Populate()
    {
        // clear old rows
        foreach (Transform t in contentParent)
            Destroy(t.gameObject);

        List<ScoreEntry> list = LeaderboardManager.Instance.GetTopEntries();
        if (list == null || list.Count == 0)
        {
            // Optional: show "No scores yet" message here
            return;
        }

        for (int i = 0; i < list.Count; i++)
        {
            var e = list[i];
            GameObject row = Instantiate(entryPrefab, contentParent);

            // Helper to find child TMP safely
            TMP_Text GetText(string name)
            {
                var tf = row.transform.Find(name);
                if (tf == null)
                {
                    Debug.LogWarning($"[LeaderboardUI] Missing child '{name}' in prefab.");
                    return null;
                }
                return tf.GetComponent<TMP_Text>();
            }

            var rankT = GetText("RankText");
            var nameT = GetText("NameText");
            var coinsT = GetText("CoinsText");
            var timeT = GetText("TimeText");

            if (rankT != null) rankT.text = $"{i + 1}.";
            if (nameT != null) nameT.text = e.playerName;
            if (coinsT != null) coinsT.text = e.coins.ToString();
            if (timeT != null) timeT.text = FormatTime(e.time);
        }
    }

    private string FormatTime(float t)
    {
        int m = (int)(t / 60), s = (int)(t % 60);
        return $"{m:00}:{s:00}";
    }
}
