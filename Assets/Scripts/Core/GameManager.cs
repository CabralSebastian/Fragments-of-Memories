using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance;
  public Camera MainCamera;
  [HideInInspector] public CharacterController Player;
  [HideInInspector] public StaffController Staff;
  [HideInInspector] public float IsAstralWorld = 0;
  public PeepSkill Peep => Player.AstralSkills.Peep;
  public Synthesizer Synthesizer => Player.Staff.Synthesizer;
  [SerializeField] private GameObject LoseScreen;
  [SerializeField] private GameObject WinScreen;
  [SerializeField] private Transform _spawnPoint;
  [SerializeField] private GameObject _worldRoot;
  private List<WorldObject> _worldObjects;

  public bool RememberAstralize { get; set; } = false;
  public bool RememberIvyLabyrinth { get; set; } = false;
  public bool RememberWaterfalls { get; set; } = false;

  private void Awake()
  {
    transform.SetParent(null);
    if (Instance && Instance != this)
    {
      Instance.UpdateGameManager(_worldRoot, _spawnPoint);
      Destroy(gameObject);
      return;
    }

    _worldObjects = new List<WorldObject>(_worldRoot.GetComponentsInChildren<WorldObject>());

    Instance = this;
    SceneManager.sceneLoaded += OnSceneLoaded;
    DontDestroyOnLoad(gameObject);
  }

  public void UpdateGameManager(GameObject worldRoot, Transform spawnPoint)
  {
    _worldObjects = new List<WorldObject>(worldRoot.GetComponentsInChildren<WorldObject>());
    _spawnPoint = spawnPoint;
  }

  public void Astralize()
  {
    IsAstralWorld = 1;
    foreach (WorldObject worldObject in _worldObjects)
      worldObject.ResetMeshCollider();

    if (Synthesizer && Synthesizer.State == SynthesizerState.ACTIVE)
      Synthesizer.SynthesizeOvelaps();
  }

  public void UnAstralize()
  {
    IsAstralWorld = 0;
    foreach (WorldObject worldObject in _worldObjects)
      worldObject.ResetMeshCollider();

    if (Synthesizer && Synthesizer.State == SynthesizerState.ACTIVE)
      Synthesizer.SynthesizeOvelaps();
  }

  public void LockCursor()
  {
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
  }

  public void UnlockCursor()
  {
    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;
  }

  public void WaterReset()
  {
    Player.TeleportTo(_spawnPoint.position);
  }

  /* UI-UX */
  public static void RestartGame()
  {
    Time.timeScale = 1f;
    // Destroy(Instance);
    SceneManager.LoadScene("Start");
  }

  public static void MainMenu()
  {
    Time.timeScale = 1f;
    // Destroy(Instance);
    SceneManager.LoadScene("Main Menu");
  }

  public void Lose()
  {
    Time.timeScale = 0f;
    Player.SetPause(true);
    UnlockCursor();

    LoseScreen.SetActive(true);
  }

  internal void Win()
  {
    Time.timeScale = 0f;
    Player.SetPause(true);
    UnlockCursor();

    WinScreen.SetActive(true);
  }

  private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
  {
    string sceneName = SceneManager.GetActiveScene().name;
    if (sceneName == "Main Menu")
      Destroy(gameObject);
  }
}
