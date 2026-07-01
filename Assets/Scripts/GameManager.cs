using UnityEngine;

public enum GamePhase
{
    FirstLoop,
    SecondLoop,
    FinalLevel
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GamePhase currentPhase = GamePhase.FirstLoop;
    public Transform player;
    public RespawnPoint bedRespawnPoint;
    public LevelManager levelManager;

    [SerializeField] private bool hasKey;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        UpdateLevelState();
    }

    public bool HasKey()
    {
        return hasKey;
    }

    public void GiveKey()
    {
        hasKey = true;
        Debug.Log("Key collected.");
    }

    public void EnterLoopDoor()
    {
        if (currentPhase == GamePhase.SecondLoop && hasKey)
        {
            GoToFinalLevel();
            return;
        }

        if (currentPhase == GamePhase.FirstLoop)
        {
            currentPhase = GamePhase.SecondLoop;
            UpdateLevelState();
        }

        RespawnPlayerAtBed();
    }

    private void GoToFinalLevel()
    {
        currentPhase = GamePhase.FinalLevel;
        UpdateLevelState();
        Debug.Log("The loop is broken. Go to the final level.");
    }

    private void UpdateLevelState()
    {
        if (levelManager == null)
        {
            return;
        }

        levelManager.ApplyPhase(currentPhase);
    }

    private void RespawnPlayerAtBed()
    {
        if (player == null || bedRespawnPoint == null)
        {
            Debug.LogWarning("GameManager needs a Player and a Bed Respawn Point.");
            return;
        }

        player.position = bedRespawnPoint.GetSpawnPosition();
        player.rotation = bedRespawnPoint.GetSpawnRotation();

        Rigidbody playerRigidbody = player.GetComponent<Rigidbody>();
        if (playerRigidbody != null)
        {
            playerRigidbody.linearVelocity = Vector3.zero;
            playerRigidbody.angularVelocity = Vector3.zero;
        }

        Debug.Log("Player returned to the bed. Current phase: " + currentPhase);
    }
}
