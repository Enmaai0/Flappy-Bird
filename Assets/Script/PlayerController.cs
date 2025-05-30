using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private bool isDead = false;
    private Rigidbody2D rb;

    [Header("飞行设置")]
    public float jumpForce = 7f;
    public float rotationSpeed = 3f;
    public float maxRotation = 25f;
    public float minRotation = -90f;

    [Header("位置设置")]
    public Vector3 readyPos = new Vector3(-0.5f, 0.5f, 0f);

    [Header("死亡设置")]
    public float fallGravityScale = 3f; // 死亡时的重力倍数


    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        SetReadyState();
    }

    void Update()
    {
        if (GameManager.Instance.currentState == GameState.Playing && !isDead)
        {
            UpdateRotation();
        }
    }

    public void Jump()
    {
        rb.linearVelocity = Vector2.up * jumpForce;
    }

    public void SetReadyState()
    {
        isDead = false;
        transform.position = readyPos;
        transform.rotation = Quaternion.identity;
        animator.speed = 1;
        rb.bodyType = RigidbodyType2D.Static;
        animator.SetTrigger("Idle");
    }

    public void SetPlayingState()
    {
        isDead = false;
        animator.SetTrigger("Fly");
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    public void SetGameOverState()
    {
        isDead = true;
        animator.speed = 0;
        // 让鸟继续掉落
        rb.gravityScale = fallGravityScale;
    }

    void UpdateRotation()
    {
        float rotation;
        if (rb.linearVelocity.y > 0)
        {
            rotation = maxRotation;
        }
        else
        {
            rotation = Mathf.Lerp(maxRotation, minRotation, -rb.linearVelocityY / 10f);
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, rotation), rotationSpeed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pipe")
        {
            if (!isDead)
                GameManager.Instance.GameOver();
        }
        else if (collision.gameObject.tag == "Ground")
        {
            if (isDead)
            {
                rb.bodyType = RigidbodyType2D.Static;
            }
            else
            {
                GameManager.Instance.GameOver();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "ScoreTrigger")
        {
            GameManager.Instance.AddScore();
        }
    }
}
