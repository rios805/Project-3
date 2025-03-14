using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public float delay = 5f; // Time before switching back to Main Menu

    void Start()
    {
        Invoke("ReturnToMainMenu", delay); 
    }

    void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
