using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScreen : MonoBehaviour
{
	public void ReStartGame()
	{
		SceneManager.LoadScene("Level 1");
	}

	public void MainMenu()
	{
		SceneManager.LoadScene("Main Menu");
	}

	public void ExitGame()
	{
		Debug.Log("Exit!");
		Application.Quit();
	}
}
