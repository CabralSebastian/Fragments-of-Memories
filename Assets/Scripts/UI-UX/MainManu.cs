using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManu : MonoBehaviour
{
	// [SerializeField] private LevelData _FirstLevelData;

	public void PlayGame()
	{
		SceneManager.LoadScene("Start");
	}

	public void ExitGame()
	{
		Debug.Log("Exit!");
		Application.Quit();
	}
}
