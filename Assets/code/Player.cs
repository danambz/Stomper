using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [Range(0, 1000)] public float speed = 300f;
    public float jumpForce = 12f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundRadius = 0.1f;
    public LayerMask whatIsGround;

    [Header("Health UI")]
    [SerializeField] private TextMeshProUGUI healthText;

    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded;

    [Header("Health Values")]
    public int currentHealth = 3;   // starts at 3
    public int maxHealth = 3;   // purely informational now

    // store un‑buffed movement
    private float baseSpeed;
    private float baseJumpForce;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // cache for powerups
        baseSpeed = speed;
        baseJumpForce = jumpForce;

        UpdateHealthUI();
    }

    void Update()
    {



        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            AudioManager.Instance.PlaySFX("jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            anim.SetBool("Jump", true);
        }
    }

    void FixedUpdate()
    {
        // Ground check
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundRadius,
            whatIsGround
        );
        anim.SetBool("Jump", !isGrounded);

        // Move
        float h = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(h * speed * Time.fixedDeltaTime, rb.velocity.y);
        anim.SetBool("Speed", Mathf.Abs(h) > 0.01f);

        // Flip
        if (h != 0)
            transform.localScale = new Vector3(Mathf.Sign(h), 1, 1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Stomp enemy
        if (other.CompareTag("enemy"))
        {
            if (!isGrounded)
            {
                AudioManager.Instance.PlaySFX("stomp");
                Destroy(other.gameObject);
            }

            else
                ChangeHealth(-1);
        }
    }

    /// <summary>
    /// Change health by delta.
    /// Health cannot go below zero, but has no upper cap.
    /// </summary>
    public void ChangeHealth(int delta)
    {
        currentHealth = Mathf.Max(currentHealth + delta, 0);
        UpdateHealthUI();
        if (currentHealth == 0)
        {
            Time.timeScale = 1f;
            // play the special jingle
            AudioManager.Instance.PlaySFX("start");

            string name = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(name);
        }
    }

    /// <summary>
    /// Called by ExtraHealth powerup.
    /// </summary>
    public void AddHealth(int amount)
    {
        ChangeHealth(amount);
    }

    private void UpdateHealthUI()
    {
        if (healthText != null)
            healthText.text = new string('♥', currentHealth);
        else
            Debug.LogWarning("Player: healthText reference not set in Inspector!");
    }

    // ─── Speed Boost ───────────────────────────────────────

    public void ApplySpeedBoost(float multiplier, float duration)
    {
        StopCoroutine("SpeedBoostRoutine");
        StartCoroutine(SpeedBoostRoutine(multiplier, duration));
    }

    IEnumerator SpeedBoostRoutine(float multiplier, float duration)
    {
        speed = baseSpeed * multiplier;
        yield return new WaitForSeconds(duration);
        speed = baseSpeed;
    }

    // ─── Jump Boost ────────────────────────────────────────

    public void ApplyJumpBoost(float multiplier, float duration)
    {
        StopCoroutine("JumpBoostRoutine");
        StartCoroutine(JumpBoostRoutine(multiplier, duration));
    }

    IEnumerator JumpBoostRoutine(float multiplier, float duration)
    {
        jumpForce = baseJumpForce * multiplier;
        yield return new WaitForSeconds(duration);
        jumpForce = baseJumpForce;
    }
}
