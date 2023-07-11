using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string sceneName; 

    public void StartGame()
    {
        
        SceneManager.LoadScene(sceneName);
    }

    public void RestartGame()
    {
       
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        
        Application.Quit();
    }
}
