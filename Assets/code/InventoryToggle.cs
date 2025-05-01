using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryToggle : MonoBehaviour
{
    [Tooltip("Exact name of your UI Canvas in the Hierarchy")]
    public string uiCanvasName = "Canvas";
    [Tooltip("Exact name of the panel under your UI Canvas")]
    public string panelName = "inventoryPanel";

    GameObject inventoryPanel;

    void Awake()
    {
        // hook into scene reload
        SceneManager.sceneLoaded += OnSceneLoaded;
        FindPanel();
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene _, LoadSceneMode __)
    {
        FindPanel();
    }

    void FindPanel()
    {
        // 1) Grab your UI Canvas by name
        GameObject canvasGO = GameObject.Find(uiCanvasName);
        if (canvasGO == null)
        {
            Debug.LogWarning($"[InventoryToggle] No GameObject named '{uiCanvasName}' found.");
            inventoryPanel = null;
            return;
        }

        // 2) Try to find the panel as a *child* of that canvas
        Transform panelTf = canvasGO.transform.Find(panelName);
        if (panelTf == null)
        {
            Debug.LogWarning($"[InventoryToggle] No child named '{panelName}' under '{uiCanvasName}'.");
            inventoryPanel = null;
            return;
        }

        inventoryPanel = panelTf.gameObject;

        // 3) Hide it at startup if you want it closed by default
        inventoryPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryPanel != null)
                inventoryPanel.SetActive(!inventoryPanel.activeSelf);
            else
                Debug.LogWarning("[InventoryToggle] Can't toggle, panel reference is null.");
        }
    }
}
