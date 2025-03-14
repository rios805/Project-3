using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
  public GameObject bulletPrefab;

  public Transform shottingOffset;

  public float moveSpeed = 5f;
  public float screenLimit = 7f;

  public UIManager uiManager;

    void Start()
    {

        Enemy.OnEnemyDied += EnemyOnOnEnemyDied;
    }

    void OnDestroy()
    {
        Enemy.OnEnemyDied -= EnemyOnOnEnemyDied;
    }
    void EnemyOnOnEnemyDied(int points)
    {
      Debug.Log($"Know about dead enemy, points: {points}");
      if (uiManager != null)
      {
          uiManager.AddScore(points);
      }
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetKeyDown(KeyCode.Space))
      {

        GameObject shot = Instantiate(bulletPrefab, shottingOffset.position, Quaternion.identity);
        Debug.Log("Bang!");

        // Destroy(shot, 3f);

      }

      float moveInput = Input.GetAxis("Horizontal"); 
      Vector3 newPosition = transform.position + new Vector3(moveInput * moveSpeed * Time.deltaTime, 0, 0);

      newPosition.x = Mathf.Clamp(newPosition.x, -screenLimit, screenLimit);

      transform.position = newPosition;

    }

     public void Die()
    {
        Debug.Log("Player Died! Switching to Credits...");
        SceneManager.LoadScene("Credits");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyBullet"))
        {
            Die(); 
        }
    }
}
