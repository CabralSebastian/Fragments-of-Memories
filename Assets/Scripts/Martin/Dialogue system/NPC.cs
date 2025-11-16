using UnityEngine;
using UnityEngine.Audio;

public class NPC : MonoBehaviour, IDialogue
{
    public Dialogue Dialogue;
    
    public Dialogue DialogueData => Dialogue;

    [SerializeField] private GameObject activatableNPC;
    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    public void EndConversation()
    {
        Debug.Log("Termine: " + gameObject.name);
        gameObject.SetActive(false);
        if (activatableNPC != null)
        {
            activatableNPC.SetActive(true);
        }
    }

    public void StartConversation()
    {
        GameEvents.TriggerDialogueStarted(this);
    }

}
