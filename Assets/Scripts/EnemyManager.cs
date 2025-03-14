using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs; 
    public int rows = 3;
    public int columns = 6;
    public float moveSpeed = 1.0f;
    public float moveDownAmount = 0.5f;
    public float moveInterval = 1.0f;
    
    private List<GameObject> enemies = new List<GameObject>();
    private int totalEnemies;
    private bool movingRight = true;
    public float gcd = 1.5f; //global cooldown similar to an MMO skill
    private float lastBullet = 0f;

   void Start()
    {
        if (SceneManager.GetActiveScene().name == "Main Game")
        {
            RemoveInitialEnemies(); 
            SpawnEnemies();         
            StartCoroutine(MoveEnemies());
        }
    }


   public void RemoveInitialEnemies()
    {
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in allEnemies)
        {
            Destroy(enemy);
        }

        enemies.Clear();

    }





    void SpawnEnemies()
    {
        float startX = -columns / 2f;
        float startY = (rows / 2f) + 2f; //initial height

        float spacingX = 1.5f; //spacing between enemies
        float spacingY = 1.5f;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                int enemyIndex = row % enemyPrefabs.Length; // Cycle through enemy types
                GameObject enemy = Instantiate(enemyPrefabs[enemyIndex], 
                    new Vector3(startX + (col * spacingX), startY - (row * spacingY), 0), Quaternion.identity);

            // Ensure the enemy is fully enabled
            enemy.SetActive(true);

            // Enable Animator if it exists
            Animator animator = enemy.GetComponent<Animator>();
            if (animator != null)
            {
                animator.enabled = true;
            }

            // Enable Collider if it exists
            Collider2D collider = enemy.GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = true;
            }

            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.enabled = true;
            }
            
                enemies.Add(enemy);
            }
        }
        totalEnemies = enemies.Count;
    }

    IEnumerator MoveEnemies()
    {
        while (enemies.Count > 0)
        {
            float moveDirection = movingRight ? 1f : -1f;
            bool shouldMoveDown = false;

            // Move all enemies
            foreach (GameObject enemy in enemies)
            {
                if (enemy != null)
                {
                    enemy.transform.position += new Vector3(moveDirection * moveSpeed, 0, 0);

                    // Check if an enemy hits the edge
                    if (Mathf.Abs(enemy.transform.position.x) >= 6)
                    {
                        shouldMoveDown = true;
                    }
                }
            }

            // If an enemy hit the edge, move down and reverse direction
            if (shouldMoveDown)
            {
                movingRight = !movingRight;
                foreach (GameObject enemy in enemies)
                {
                    if (enemy != null)
                    {
                        enemy.transform.position += new Vector3(0, -moveDownAmount, 0);
                    }
                }
            }

            yield return new WaitForSeconds(moveInterval);
        }
    }

    public void TryFireBullet(Transform enemyFirePoint, GameObject bulletPrefab)
    {
        if (Time.time - lastBullet >= gcd)
        {
            lastBullet = Time.time; // Update last fired time
            Instantiate(bulletPrefab, enemyFirePoint.position, Quaternion.identity);
        }
    }

    public void EnemyDestroyed(GameObject enemy)
    {
        enemies.Remove(enemy);
        Destroy(enemy);

        // Speed up when enemies are destroyed
        moveInterval *= 0.9f;
    }
}
