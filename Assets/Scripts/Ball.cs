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
        if (collision.gameObject.CompareTag("GoalWall"))
        {
            // تحديد اللاعب الذي سُجلت النقطة لصالحه بناءً على اسم الجدار
            if (collision.gameObject.name == "Player1Goal")
            {
                scoreManager?.AddScore("Player2"); // Player2 يحصل على النقطة
                Debug.Log("Goal Scored by Player2");
            }
            else if (collision.gameObject.name == "Player2Goal")
            {
                scoreManager?.AddScore("Player1"); // Player1 يحصل على النقطة
                Debug.Log("Goal Scored by Player1");
            }
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
