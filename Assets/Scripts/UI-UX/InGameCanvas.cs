using UnityEngine;

public class InGameCanvas : MonoBehaviour
{
	[SerializeField] private GameObject _pauseMenu;
	private bool _isPaused = false;

	public bool _IsPaused => _isPaused;

	public void MainManu()
	{
		// GameManager.Instance.GoToMainMenu();
		GameManager.MainMenu();
	}

	public void ExitGame()
	{
		Debug.Log("Exit!");
		Application.Quit();
	}

	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
			TogglePause();
	}

	public void TogglePause()
	{
		_isPaused = !_isPaused;

		_pauseMenu.SetActive(_isPaused);
		Time.timeScale = _isPaused ? 0f : 1f;

		GameManager.Instance.Player.SetPause(_isPaused);

		if (_isPaused)
			GameManager.Instance.UnlockCursor();
		else
			GameManager.Instance.LockCursor();
	}
}
