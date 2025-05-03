<<<<<<< HEAD
﻿using UnityEngine;
=======
using UnityEngine;
>>>>>>> b1c1e8a5879243eeed8ea5e039e874adacfccfcc
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

/// <summary>
/// Drives your inventory display.  Listens for Inventory.Instance.onInventoryChanged
/// and rebuilds the list of slots under contentParent whenever it fires.
/// </summary>
public class InventoryUI : MonoBehaviour
{
    [Header("UI References")]
    [Tooltip("Drag your InvSlot prefab here")]
    [SerializeField] private GameObject slotPrefab;
    [Tooltip("Drag the Grid/Content transform under your inventoryPanel here")]
    [SerializeField] private Transform contentParent;

    private Inventory inventory;

    void Awake()
    {
        inventory = Inventory.Instance;
    }

    void OnEnable()
    {
        inventory.onInventoryChanged.AddListener(Refresh);
        Refresh();
    }

    void OnDisable()
    {
        if (inventory != null)
            inventory.onInventoryChanged.RemoveListener(Refresh);
    }

    /// <summary>
    /// Clears out any existing slots and re-instantiates one per item in inventory.
    /// </summary>
    public void Refresh()
    {
        if (slotPrefab == null || contentParent == null)
        {
            Debug.LogError("InventoryUI: slotPrefab or contentParent is missing!");
            return;
        }

        // Destroy old slots
        foreach (Transform t in contentParent)
            Destroy(t.gameObject);

        // Build a new slot for each item
        List<ItemData> items = inventory.items;
        for (int i = 0; i < items.Count; i++)
        {
            ItemData item = items[i];
            GameObject go = Instantiate(slotPrefab, contentParent);
            InvSlot slot = go.GetComponent<InvSlot>();
            if (slot == null)
            {
                Debug.LogError("InventoryUI: slotPrefab has no InvSlot component!");
                continue;
            }

            slot.item = item;

            // cache the icon image reference
            slot.iconImage = go.transform.Find("Icon")?.GetComponent<Image>();
            if (slot.iconImage == null)
            {
                Debug.LogError("InvSlot prefab is missing a child Image named 'Icon'!");
            }
            else
            {
                slot.iconImage.sprite = item.icon;
            }

            // let the InvSlot listen for pointer clicks
            slot.slotGO = go;
        }
    }
}
