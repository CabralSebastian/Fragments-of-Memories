using UnityEngine;

public class LoseScreen : MonoBehaviour
{
	public void ReStartGame()
	{
		GameManager.RestartGame();
	}

	public void MainMenu()
	{
		GameManager.MainMenu();
	}

	public void ExitGame()
	{
		Debug.Log("Exit!");
		Application.Quit();
	}
}
