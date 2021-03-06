using UnityEngine;
using UnityEngine.SceneManagement;

/** Manages scene independent state, so scene transitions for example. */
public class GlobalStateManager : MonoBehaviour
{
	public void PlayGame()
	{
		SceneManager.LoadScene(2);
	}

	public void Scoreboard()
	{
		SceneManager.LoadScene(1);
	}

	public void MainMenu()
	{
		SceneManager.LoadScene(0);
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}

