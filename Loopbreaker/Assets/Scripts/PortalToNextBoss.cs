using UnityEngine;

public class PortalToNextBoss : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.E;
    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactKey))
        {
            Debug.Log("Player activated portal. Loading next boss...");
            GameFlowManager.Instance.OnBossDefeated();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            // Optional: Show UI prompt like "Press E to enter portal"
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            // Optional: Hide UI prompt
        }
    }
}