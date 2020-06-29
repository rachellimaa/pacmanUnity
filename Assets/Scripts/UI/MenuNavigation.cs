using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void Quit()
    {
        Application.Quit();
    }
	
    public void Play()
    {
        SceneManager.LoadScene("GameScene");
    }
    
    public void Credits()
    {
        SceneManager.LoadScene("CreditScene");
    }
}
