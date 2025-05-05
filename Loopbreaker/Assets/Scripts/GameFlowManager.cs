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

    void InitializeBossOrder()
    {
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
    }

    public int GetFirstBossSceneIndex() => firstBoss;
    public int GetSecondBossSceneIndex() => secondBoss;

    public void OnBossDefeated()
    {
        if (!firstBossDefeated)
        {
            firstBossDefeated = true;
            SceneManager.LoadScene(secondBoss);
        }
        else
        {
            Debug.Log("Both bosses defeated! Load end scene or victory screen.");
            // SceneManager.LoadScene("VictoryScene"); // Uncomment if needed
        }
    }
}