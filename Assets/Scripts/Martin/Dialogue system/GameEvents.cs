using System;
using UnityEngine;

public class GameEvents
{
    public static event Action<IDialogue> OnDialogueStarted;
    public static event Action OnDialogueEnded;

    public static void TriggerDialogueStarted (IDialogue dialogue)
    {
        OnDialogueStarted?.Invoke(dialogue);
    }

    public static void TriggerDialogueEnded()
    {
        OnDialogueEnded?.Invoke();
    }


}
