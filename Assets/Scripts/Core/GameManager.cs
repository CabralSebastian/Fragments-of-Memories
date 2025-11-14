using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance;
  public Camera MainCamera;
  [HideInInspector] public CharacterController Player;
  [HideInInspector] public StaffController Staff;

  public PeepSkill Peep => Player.AstralSkills.Peep;
  public Synthesizer Synthesizer => Player.Staff.Synthesizer;
  [SerializeField] private GameObject LoseScreen;
  [SerializeField] private GameObject WinScreen;
  [SerializeField] private GameObject _action;
  [SerializeField] private TextMeshProUGUI _actionText;

  [SerializeField] private Transform _spawnPoint;
  [SerializeField] private GameObject _worldRoot;
  [SerializeField] private EnemyController[] _enemies;
  private List<WorldObject> _worldObjects;
  [SerializeField] private float _isAstralWorld = 0f;
  [SerializeField] private Material _skyMat;

  public bool IsCombat => _enemies.Any(enemy => enemy.IsInCombat);

  public float IsAstralWorld
  {
    get => _isAstralWorld;
    set
    {
      _isAstralWorld = Mathf.Clamp01(value);

      if (_skyMat == null)
        _skyMat = RenderSettings.skybox;

      if (_skyMat != null)
        _skyMat.SetFloat("_Astralization", _isAstralWorld);
    }
  }

  public bool RememberAstralize { get; set; } = false;
  public bool RememberIvyLabyrinth { get; set; } = false;
  public bool RememberWaterfalls { get; set; } = false;

  private void Awake()
  {
    transform.SetParent(null);
    if (Instance && Instance != this)
    {
      Instance.UpdateGameManager(_worldRoot, _spawnPoint, _enemies);
      Destroy(gameObject);
      return;
    }

    _worldObjects = new List<WorldObject>(_worldRoot.GetComponentsInChildren<WorldObject>());

    Instance = this;
    SceneManager.sceneLoaded += OnSceneLoaded;
    DontDestroyOnLoad(gameObject);
  }

  public void UpdateGameManager(GameObject worldRoot, Transform spawnPoint, EnemyController[] enemies)
  {
    _worldObjects = new List<WorldObject>(worldRoot.GetComponentsInChildren<WorldObject>());
    _spawnPoint = spawnPoint;
    _enemies = enemies;
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

    RenderSettings.skybox = Instance._skyMat;
  }

  public void ShowAction(string actionText)
  {
    _action.SetActive(true);
    _actionText.SetText(actionText);
  }

  public void HideAction()
  {
    _action.SetActive(false);
  }
}
