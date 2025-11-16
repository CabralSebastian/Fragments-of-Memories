using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    public UnityEvent interact;

    private PlayerInteraction lingeringPlayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerInteraction player))
        {
            player.SetNearbyObject(this);
            lingeringPlayer = player;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerInteraction player))
        {
            player.ClearNearbyObject(this);
        }
    }

    private void OnDisable()
    {
        lingeringPlayer.ClearNearbyObject(this);
    }

    public void Interact()
    {
        GameManager.Instance.UnlockCursor();
        interact?.Invoke();
    }

}