using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBullet : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D;
    private SpriteRenderer spriteRenderer;

    public float speed = 5;

    public float flipInterval = 0.2f;

    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        Fire();
        StartCoroutine(FlipSpriteRepeatedly());
    }

    public void Fire()
    {
        myRigidbody2D.linearVelocity = Vector2.down * speed; // Moves downward
        Debug.Log("Enemy bullet fired!");
    }

    IEnumerator FlipSpriteRepeatedly()
    {
        while (true)
        {
            yield return new WaitForSeconds(flipInterval);

            // Flip both X and Y for a "spinning" effect
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            Debug.Log("Player hit!");
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Enemy")) 
        {
            // Ignore collisions with enemies
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
    }
    }

}

