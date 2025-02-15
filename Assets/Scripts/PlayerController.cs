using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float speed = 10f;
    public string inputAxis;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float move = Input.GetAxis(inputAxis);
        rb.linearVelocity = new Vector2(0, move * speed);
    }
}
