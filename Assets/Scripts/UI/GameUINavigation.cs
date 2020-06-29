using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUINavigation : MonoBehaviour
{
	[SerializeField] private Canvas pauseCanvas;
	[SerializeField] private Canvas quitCanvas;
	[SerializeField] private Canvas readyCanvas;
	[SerializeField] private Canvas gameOverCanvas;
	[SerializeField] private Button menuButton;
	public float initialDelay;
	private bool _paused;
	private bool _quit;
	
	void Start () 
	{
		StartCoroutine("ShowReadyScreen", initialDelay);
	}
	
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(GameManager.gameState == GameManager.GameState.Scores)
				Menu();
			else
			{
				if(_quit)
					ToggleQuit();
				else
					TogglePause();
			}
		}
	}

	public void HandlerShowReadyScreen()
	{
		StartCoroutine("ShowReadyScreen", initialDelay);
	}

    public void HandlerShowGameOverScreen()
    {
        StartCoroutine("ShowGameOverScreen");
    }

	private IEnumerator ShowReadyScreen(float seconds)
	{
		GameManager.gameState = GameManager.GameState.Init;
		readyCanvas.enabled = true;
		yield return new WaitForSeconds(seconds);
		readyCanvas.enabled = false;
		GameManager.gameState = GameManager.GameState.Game;
	}

    private IEnumerator ShowGameOverScreen()
    {
        gameOverCanvas.enabled = true;
        yield return new WaitForSeconds(2);
        Menu();
    }

	public void TogglePause()
	{
		if(_paused)
		{
			Time.timeScale = 1;
			pauseCanvas.enabled = false;
			_paused = false;
			menuButton.enabled = true;
		}else
		{
			pauseCanvas.enabled = true;
			Time.timeScale = 0.0f;
			_paused = true;
			menuButton.enabled = false;
		}
	}
	
	public void ToggleQuit()
	{
		if(_quit)
        {
            pauseCanvas.enabled = true;
            quitCanvas.enabled = false;
			_quit = false;
		}else
        {
            quitCanvas.enabled = true;
			pauseCanvas.enabled = false;
			_quit = true;
		}
	}

	public void Menu()
	{
		SceneManager.LoadScene("MenuScene");
		Time.timeScale = 1.0f;
	    GameManager.DestroySelf();
	}
    
    public void LoadLevel()
    {
	    GameManager.Level++;
	    SceneManager.LoadScene("GameScene");
    }

}
