using UnityEngine;

public class Healing : MonoBehaviour
{
    private Health player;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.F))
        {
            player.FillToMax();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            player = null;
        }
    }
}
