using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public delegate void EnemyDied(int points);
    public static event EnemyDied OnEnemyDied;

    public int enemyPoints = 10;
    public GameObject bulletPrefab;
    public Transform firePoint; // Empty GameObject as a fire position
    public float fireRate = 2f; // Time between shots

    private void Start()
    {
        StartCoroutine(ShootAtPlayer());
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
            Debug.Log("Enemy hit!");
            OnEnemyDied?.Invoke(enemyPoints);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        
    }

    IEnumerator ShootAtPlayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2f, 5f));

            if (bulletPrefab != null)
            {
                Instantiate(bulletPrefab, transform.position, Quaternion.identity); 
            }
        }
    }

}


