using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBullet : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D;

    public float speed = 5;

    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        Fire();
    }

    public void Fire()
    {
        myRigidbody2D.linearVelocity = Vector2.down * speed; // Moves downward
        Debug.Log("Enemy bullet fired!");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null) 
        {
            Debug.Log("Player hit!");
            Destroy(collision.gameObject); 
            Destroy(gameObject); 
        }
    }
}

