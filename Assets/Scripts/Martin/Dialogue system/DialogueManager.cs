using JetBrains.Annotations;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
 public static DialogueManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool inputEnabled = true;

    private void Start()
    {
        GameEvents.OnDialogueEnded += EnableInput;
        GameEvents.OnDialogueStarted += DisableInput;
    }

    private void OnDestroy()
    {
        GameEvents.OnDialogueEnded -= EnableInput;
        GameEvents.OnDialogueStarted -= DisableInput;
    }

    private void EnableInput()
    {
        inputEnabled = true;
    }

    private void DisableInput(IDialogue dialogue)
    {
        inputEnabled = false;
    } 
}
