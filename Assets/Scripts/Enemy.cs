using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
         if (SceneManager.GetActiveScene().name != "Main Menu")
        {
            StartCoroutine(ShootAtPlayer());
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet")) // Only destroy when hit by player's bullet
        {
            Debug.Log("Enemy hit by player bullet!");
            OnEnemyDied?.Invoke(enemyPoints);
            Destroy(collision.gameObject); 
            Destroy(gameObject);
        }
    }


    IEnumerator ShootAtPlayer()
    {
        while (true)
        {
            float randomFireRate = Random.Range(3f, 6f);
            yield return new WaitForSeconds(randomFireRate);

            if (bulletPrefab != null && Object.FindFirstObjectByType<EnemyManager>() != null)
            {
                Object.FindFirstObjectByType<EnemyManager>().TryFireBullet(transform, bulletPrefab);
            }
        }
    }


}


