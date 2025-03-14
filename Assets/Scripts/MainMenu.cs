using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public EnemyManager enemyManager; // Reference to EnemyManager

    public void StartGame()
    {
        SceneManager.LoadScene("Main Game"); 
    }

}

