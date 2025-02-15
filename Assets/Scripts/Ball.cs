using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float initialSpeed = 30f;
    [SerializeField] private float speedIncrease = 1.1f; // نسبة زيادة السرعة عند الاصطدام بالمضرب
    private Rigidbody2D rb;
    private ScoreManager scoreManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scoreManager = FindFirstObjectByType<ScoreManager>();

        LaunchBall();
    }

    void LaunchBall()
    {
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(-1f, 1f);

        rb.linearVelocity = new Vector2(x, y).normalized * initialSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            scoreManager?.AddScore(collision.gameObject.name);

            // زيادة السرعة عند الاصطدام بالمضرب
            float currentSpeed = rb.linearVelocity.magnitude;
            rb.linearVelocity = rb.linearVelocity.normalized * (currentSpeed * speedIncrease);
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            // ضمان أن الكرة لا تفقد سرعتها عند الاصطدام بالجدران
            float currentSpeed = rb.linearVelocity.magnitude;
            rb.linearVelocity = rb.linearVelocity.normalized * currentSpeed;
        }
    }

    void FixedUpdate()
    {
        // التأكد من أن السرعة لا تنخفض تحت الحد الأدنى
        if (rb.linearVelocity.magnitude < initialSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * initialSpeed;
        }
    }
}
