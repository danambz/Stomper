using UnityEngine;

public class coin : MonoBehaviour
{
    public int value = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Coin hit {other.name}");
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySFX("coin");
            GameManager.Instance.AddCoin(value);
            Destroy(gameObject);
        }
    }
}
