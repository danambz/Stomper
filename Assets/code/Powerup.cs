using UnityEngine;
using System.Collections;

public enum PowerupType
{
    ExtraHealth,
    SpeedBoost,
    JumpBoost
}

[RequireComponent(typeof(Collider2D))]
public class Powerup : MonoBehaviour
{
    [Header("Powerup Settings")]
    public PowerupType type = PowerupType.ExtraHealth;
    public float duration = 10f;
    public int healthAmount = 1;
    public float speedMultiplier = 1.5f;
    public float jumpMultiplier = 2f;

    void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        AudioManager.Instance.PlaySFX("powerup");
        var player = other.GetComponent<Player>();
        if (player == null) return;

        switch (type)
        {
            case PowerupType.ExtraHealth:
                player.AddHealth(healthAmount);
                break;
            case PowerupType.SpeedBoost:
                player.ApplySpeedBoost(speedMultiplier, duration);
                break;
            case PowerupType.JumpBoost:
                player.ApplyJumpBoost(jumpMultiplier, duration);
                break;
        }

        Destroy(gameObject);
    }
}
