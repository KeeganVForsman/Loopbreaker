using UnityEngine;

public class SceneBossManager : MonoBehaviour
{
    public GameObject portalToNext; // Assign the portal GameObject in inspector
    public int totalBossesInScene = 2;

    private int defeatedBosses = 0;

    void Start()
    {
        if (portalToNext != null)
            portalToNext.SetActive(false); // Disable portal initially
    }

    public void RegisterBossDefeat()
    {
        defeatedBosses++;
        Debug.Log("Boss defeated! " + defeatedBosses + "/" + totalBossesInScene);

        if (defeatedBosses >= totalBossesInScene)
        {
            Debug.Log("All bosses in scene defeated.");
            if (portalToNext != null)
                portalToNext.SetActive(true);
        }
    }
}