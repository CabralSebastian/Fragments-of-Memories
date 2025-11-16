using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using System.ComponentModel;

public class UIDialogue : MonoBehaviour
{
    public GameObject container;

    public TextMeshProUGUI nameText; 
    public TextMeshProUGUI dialogueText;

    private Dialogue dialogue;
    private int currentIndex;
    private IDialogue owner;

    private AudioSource audioSource;


    private void Start()
    {
        GameEvents.OnDialogueStarted += StartDialogue;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnDestroy()
    {
        GameEvents.OnDialogueStarted -= StartDialogue;
    }

    private void StartDialogue(IDialogue dialogue)
    {
        owner = dialogue;
        container.SetActive(true);
        this.dialogue = owner.DialogueData;
        if (dialogue == null || this.dialogue.Lines.Count == 0)
        {
            EndDialogue();
            return;
        }
        currentIndex = 0;

        ShowLine();
    }

    public void NextLines()
    {
        currentIndex++;

        if (currentIndex >= dialogue.Lines.Count)
        {
            EndDialogue();
            return;
        }
        ShowLine();
    }

    private void ShowLine()
    {
        audioSource.Stop();
        
        nameText.text = dialogue.Lines[currentIndex].Speaker;
        dialogueText.text = dialogue.Lines[currentIndex].Text;
        if (dialogue.Lines[currentIndex].clip != null)
        {
            audioSource.PlayOneShot(dialogue.Lines[currentIndex].clip);
        }
    }

    private void EndDialogue()
    {
        audioSource.Stop();
        
        GameEvents.TriggerDialogueEnded();
        owner.EndConversation();
        container.SetActive(false);
        GameManager.Instance.LockCursor();
    }

    
}
