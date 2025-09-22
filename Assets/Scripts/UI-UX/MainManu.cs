using UnityEngine;

public class MainManu : MonoBehaviour
{
	// [SerializeField] private LevelData _FirstLevelData;

	public void PlayGame()
	{
		// GameManager.Instance.InitiateLevel(_FirstLevelData);
	}

	public void ExitGame()
	{
		Debug.Log("Exit!");
		Application.Quit();
	}
}
