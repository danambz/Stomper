using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public float switchTime = 2f;

    private Rigidbody2D rb;
    private int direction = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Patrol());
    }

    IEnumerator Patrol()
    {
        while (true)
        {
            rb.velocity = new Vector2(direction * speed, rb.velocity.y);
            yield return new WaitForSeconds(switchTime);
            direction *= -1;
            transform.localScale = new Vector3(direction, 1, 1);
        }
    }
}
