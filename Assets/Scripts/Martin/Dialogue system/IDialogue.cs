using UnityEngine;

public interface IDialogue
{
    Dialogue DialogueData { get; }

    void StartConversation();

    void EndConversation();
}
