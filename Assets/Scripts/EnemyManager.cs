using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        SpawnEnemies();
        StartCoroutine(MoveEnemies());
    }

    void SpawnEnemies()
    {
        float startX = -columns / 2f;
        float startY = rows / 2f;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                int enemyIndex = row % enemyPrefabs.Length; // Cycle through enemy types
                GameObject enemy = Instantiate(enemyPrefabs[enemyIndex], 
                    new Vector3(startX + col, startY - row, 0), Quaternion.identity);
                
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

    public void EnemyDestroyed(GameObject enemy)
    {
        enemies.Remove(enemy);
        Destroy(enemy);

        // Speed up when enemies are destroyed
        moveInterval *= 0.9f;
    }
}
