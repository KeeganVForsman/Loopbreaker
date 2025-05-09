using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlowManager : MonoBehaviour
{
    public static GameFlowManager Instance { get; private set; }

    public int boss1SceneIndex = 2;
    public int boss2SceneIndex = 3;

    private int firstBoss;
    private int secondBoss;
    private bool firstBossDefeated = false;
    private bool bossOrderInitialized = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeBossOrder();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool BothBossesDefeated()
    {
        return firstBossDefeated;
        // If you're planning more than 2 bosses later, expand this logic accordingly
    }

    void InitializeBossOrder()
    {
        if (bossOrderInitialized) return;

        if (Random.Range(0, 2) == 0)
        {
            firstBoss = boss1SceneIndex;
            secondBoss = boss2SceneIndex;
        }
        else
        {
            firstBoss = boss2SceneIndex;
            secondBoss = boss1SceneIndex;
        }

        bossOrderInitialized = true;
        Debug.Log($"Boss order initialized: First = {firstBoss}, Second = {secondBoss}");
    }

    public void StartFirstBoss()
    {
        InitializeBossOrder();
        SceneManager.LoadScene(firstBoss);
    }

    public void OnBossDefeated()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;

        if (!firstBossDefeated && currentScene == firstBoss)
        {
            firstBossDefeated = true;
            SceneManager.LoadScene(secondBoss);
        }
        else if (firstBossDefeated && currentScene == secondBoss)
        {
            Debug.Log("Both bosses defeated! Load end scene or victory screen.");
            SceneManager.LoadScene("Victory");
        }
        else
        {
            Debug.LogWarning("Called OnBossDefeated from an unexpected scene!");
        }
    }

    public int GetFirstBossSceneIndex() => firstBoss;
    public int GetSecondBossSceneIndex() => secondBoss;
}