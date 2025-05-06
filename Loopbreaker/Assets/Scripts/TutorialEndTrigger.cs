using UnityEngine;

public class TutorialEndTrigger : MonoBehaviour
{
    public KeyCode endTutorialKey = KeyCode.E; // You can change or trigger this via animation or event

    void Update()
    {
        // Simulate end of tutorial with a key press (replace with your actual tutorial ending logic)
        if (Input.GetKeyDown(endTutorialKey))
        {
            Debug.Log("Tutorial complete. Loading first randomized boss...");
            if (GameFlowManager.Instance != null)
            {
                GameFlowManager.Instance.StartFirstBoss();
            }
            else
            {
                Debug.LogError("GameFlowManager is missing in the scene!");
            }
        }
    }
}