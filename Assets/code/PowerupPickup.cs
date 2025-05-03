//using UnityEngine;
//using System.Collections;

//public enum PowerupType { ExtraHealth, SpeedBoost }

//public class PowerupPickup : MonoBehaviour
//{
//    public PowerupType type;
//    public float duration = 15f;
//    public int healthAmount = 1;
//    public float speedMultiplier = 2f;

//    private void OnTriggerEnter2D(Collider2D other)
//    {
//        if (other.CompareTag("Player"))
//        {
//            var player = other.GetComponent<Player>();
//            StartCoroutine(Apply(player));
//            Destroy(gameObject);
//        }
//    }

//    IEnumerator Apply(Player player)
//    {
//        switch (type)
//        {
//            case PowerupType.ExtraHealth:
//                player.AddHealth(healthAmount);
//                yield break; // one‑time
//            case PowerupType.SpeedBoost:
//                player.speed *= speedMultiplier;
//                yield return new WaitForSeconds(duration);
//                player.speed /= speedMultiplier;
//                yield break;
//        }
//    }
//}
