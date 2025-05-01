using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Attached to your InvSlot prefab root.  Implements IPointerClickHandler
/// so it can detect left‐ and right‐clicks on itself.
/// </summary>
public class InvSlot : MonoBehaviour, IPointerClickHandler
{
    [HideInInspector] public ItemData item;
    [HideInInspector] public Image iconImage;
    [HideInInspector] public GameObject slotGO;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (item == null)
            return;

        // ─── LEFT CLICK: USE ───────────────────────────────────────────
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            var player = FindObjectOfType<Player>();
            if (player != null && item.type == ItemType.Consumable)
            {
                // health restore
                if (item.healthRestore > 0)
                    player.AddHealth(item.healthRestore);

                // speed boost
                if (item.speedMultiplier != 1f && item.speedDuration > 0f)
                    player.ApplySpeedBoost(item.speedMultiplier, item.speedDuration);

                // jump boost
                if (item.jumpMultiplier != 1f && item.jumpDuration > 0f)
                    player.ApplyJumpBoost(item.jumpMultiplier, item.jumpDuration);
            }

            Inventory.Instance.Remove(item);
            Destroy(slotGO);
        }

        // ─── RIGHT CLICK: DROP ──────────────────────────────────────────
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            // remove from inventory
            Inventory.Instance.Remove(item);

            // spawn the world pickup prefab at player’s feet
            if (item.worldPrefab != null)
            {
                Vector3 dropPos = Vector3.zero;
                var player = FindObjectOfType<Player>();
                if (player != null)
                    dropPos = player.transform.position + Vector3.right * 1f;

                Instantiate(item.worldPrefab, dropPos, Quaternion.identity);
            }

            Destroy(slotGO);
        }
    }
}
