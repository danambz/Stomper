using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ItemPickup : MonoBehaviour
{
    public ItemData item;

    void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // add to inventory
        Inventory.Instance.Add(item);

        // optional feedback: sound, animation...
        Debug.Log($"Picked up {item.itemName}");

        Destroy(gameObject);
    }
}
