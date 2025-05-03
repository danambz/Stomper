using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    // your collected items
    public List<ItemData> items = new List<ItemData>();

    // event for UI to refresh
    public UnityEvent onInventoryChanged = new UnityEvent();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Adds an item to the inventory and notifies listeners.
    /// </summary>
    public void Add(ItemData item)
    {
        items.Add(item);
        if (onInventoryChanged != null)
            onInventoryChanged.Invoke();
    }

    /// <summary>
    /// Removes an item from the inventory and notifies listeners.
    /// </summary>
    public bool Remove(ItemData item)
    {
        bool ok = items.Remove(item);
        if (ok && onInventoryChanged != null)
            onInventoryChanged.Invoke();
        return ok;
    }
}
