using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Rigidbody2D rb;

    public float Speed = 5f;

    public float XGravityStrength = -5f;

    public Vector3 direction = Vector3.right;

    public float ImpactStrength = 20f;

    public bool isRight;

    public ParticleSystem effect;

    public LayerMask ground;

    private void Start()
    {
        //rb.AddForce(direction.normalized * ImpactStrength * rb.mass, ForceMode2D.Impulse);
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            //rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            isRight = !isRight;
            //direction = isRight ? Vector3.right : Vector3.left;

            GoToOpposite();
        }
    }

    private void FixedUpdate()
    {
        //Vector2 force = direction.normalized * XGravityStrength * rb.mass;
        //rb.AddForce(force, ForceMode2D.Force);
        rb.linearVelocityY = Speed;
    }

    public float duration = 0.2f;
    private async UniTask GoToOpposite()
    {
        //rb.AddForce(direction.normalized * ImpactStrength * rb.mass, ForceMode2D.Impulse);

        transform.DORotate(new Vector3(0f, 0f, 360f), duration * 0.9f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).From(Vector3.zero);

        transform.DOMoveX(CheckOppositePos().x, duration).SetEase(Ease.Linear).OnComplete(() =>
        {
            PlayEffect().Forget();
        });

    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    PlayEffect().Forget();
    //}

    private async UniTask PlayEffect()
    {
        ParticleSystem newvfx = Instantiate(effect, transform.parent);
        Vector3 pos = transform.position;
        pos.x = isRight ? pos.x + 0.5f : pos.x - 0.5f;
        newvfx.transform.position = pos;
        float zRotation = isRight ? 90f : -90f;
        newvfx.transform.rotation = Quaternion.Euler(0f, 0f, zRotation);
        newvfx.Play();

        await UniTask.WaitForSeconds(3f);

        Destroy(newvfx.gameObject);
    }

    private Vector2 CheckOppositePos()
    {
        RaycastHit2D box = Physics2D.BoxCast(transform.position, new Vector2(1, 1f), 0f, isRight ? Vector2.right : Vector2.left, 100f, ground);

        Vector2 pos = box.transform.position;

        //Debug.Log("Hit " + box.transform.name, box.transform.gameObject);

        pos.x += isRight ? 4.5f : -4.5f;

        return pos;
    }
}
