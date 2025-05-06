using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBossManager : MonoBehaviour
{
    public GameObject portalToNext; // Assign the portal GameObject in inspector
    public int totalBossesInScene = 2;

    private int defeatedBosses = 0;

    void Start()
    {
        if (portalToNext != null)
            portalToNext.SetActive(false); // Disable portal initially

        ///test problem scene
        //if (SceneManager.GetActiveScene().name == "PokminBossFight") 
        //{
        //    if (portalToNext != null)
        //    {
        //        portalToNext.SetActive(true);
        //        Debug.Log("Manually forcing portal open in test scene.");
        //    }
        //    else
        //    {
        //        Debug.LogWarning("portalToNext is not assigned in test scene.");
        //    }
        //}


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