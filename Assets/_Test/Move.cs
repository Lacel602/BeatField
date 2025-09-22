using UnityEngine;

public class Move : MonoBehaviour
{
    public Rigidbody2D rb;

    public float Speed = 5f;

    public float XGravityStrength = -5f;

    public Vector3 direction = Vector3.right;

    public float ImpactStrength = 20f;

    public bool isRight;

    private void Start()
    {
        rb.AddForce(direction.normalized * ImpactStrength * rb.mass, ForceMode2D.Impulse);
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            rb.linearVelocity = new Vector2 (0, rb.linearVelocity.y);
            isRight = !isRight;
            direction = isRight ? Vector3.right : Vector3.left;

            rb.AddForce(direction.normalized * ImpactStrength * rb.mass, ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
        Vector2 force = direction.normalized * XGravityStrength * rb.mass;
        rb.AddForce(force, ForceMode2D.Force);
        rb.linearVelocityY = Speed;
    }
}
