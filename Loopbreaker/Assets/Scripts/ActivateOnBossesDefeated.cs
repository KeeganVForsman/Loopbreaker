using UnityEngine;

public class ActivateOnBossesDefeated : MonoBehaviour
{
    public GameObject objectToEnable; // Assign the portal or any object you want to enable
    private bool alreadyChecked = false;

    void Start()
    {
        if (objectToEnable == null)
        {
            Debug.LogWarning("No object assigned to enable on boss defeat.");
            objectToEnable = gameObject; // fallback to self
        }

        objectToEnable.SetActive(false);
    }

    void Update()
    {
        if (!alreadyChecked && GameFlowManager.Instance != null)
        {
            if (GameFlowManager.Instance.BothBossesDefeated())
            {
                objectToEnable.SetActive(true);
                alreadyChecked = true;
            }
        }
    }
}