using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public enum GamePhase
{
    Level1Loop1,
    Level1Loop2,
    Level1Loop3,
    Level2,
    FinalLevel
}

public class GameManager : MonoBehaviour
{
    public static GameManager Singleton { get; private set; }
    public static GameManager Instance => Singleton;

    [FormerlySerializedAs("currentPhase")] public GamePhase CurrentPhase = GamePhase.Level1Loop1;
    [FormerlySerializedAs("player")] public Transform Player;
    [FormerlySerializedAs("bedRespawnPoint")] public RespawnPoint BedRespawnPoint;
    [FormerlySerializedAs("levelManager")] public LevelManager LevelManager;

    [SerializeField] private float blackScreenDuration = 2f;
    [SerializeField] private bool hasKey;
    private bool isDoorTransitionRunning;
    private bool showBlackScreen;

    void Awake()
    {
        if (Singleton != null && Singleton != this)
        {
            Destroy(gameObject);
            return;
        }

        Singleton = this;
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
        if (isDoorTransitionRunning)
        {
            return;
        }

        StartCoroutine(DoorTransition());
    }

    private IEnumerator DoorTransition()
    {
        isDoorTransitionRunning = true;
        showBlackScreen = true;

        HandleDoorLogic();

        yield return new WaitForSeconds(blackScreenDuration);

        showBlackScreen = false;
        isDoorTransitionRunning = false;
    }

    private void HandleDoorLogic()
    {
        if ((CurrentPhase == GamePhase.Level1Loop2 || CurrentPhase == GamePhase.Level1Loop3) && hasKey)
        {
            GoToLevel2();
            return;
        }

        if (CurrentPhase == GamePhase.Level2)
        {
            GoToFinalLevel();
            return;
        }

        if (CurrentPhase == GamePhase.Level1Loop1)
        {
            CurrentPhase = GamePhase.Level1Loop2;
            UpdateLevelState();
        }
        else if (CurrentPhase == GamePhase.Level1Loop2)
        {
            CurrentPhase = GamePhase.Level1Loop3;
            UpdateLevelState();
        }
        else if (CurrentPhase == GamePhase.Level1Loop3)
        {
            CurrentPhase = GamePhase.Level1Loop1;
            UpdateLevelState();
        }

        RespawnPlayerAtBed();
    }

    private void OnGUI()
    {
        if (!showBlackScreen)
        {
            return;
        }

        Color previousColor = GUI.color;
        GUI.color = Color.black;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
        GUI.color = previousColor;
    }

    private void GoToLevel2()
    {
        CurrentPhase = GamePhase.Level2;
        UpdateLevelState();
        Debug.Log("Level 2 started.");
    }

    private void GoToFinalLevel()
    {
        CurrentPhase = GamePhase.FinalLevel;
        UpdateLevelState();
        Debug.Log("The loop is broken. Go to the final level.");
    }

    private void UpdateLevelState()
    {
        if (LevelManager == null)
        {
            return;
        }

        LevelManager.ApplyPhase(CurrentPhase);
    }

    private void RespawnPlayerAtBed()
    {
        if (Player == null || BedRespawnPoint == null)
        {
            Debug.LogWarning("GameManager needs a Player and a Bed Respawn Point.");
            return;
        }

        Player.position = BedRespawnPoint.GetSpawnPosition();
        Player.rotation = BedRespawnPoint.GetSpawnRotation();

        Rigidbody playerRigidbody = Player.GetComponent<Rigidbody>();
        if (playerRigidbody != null)
        {
            playerRigidbody.linearVelocity = Vector3.zero;
            playerRigidbody.angularVelocity = Vector3.zero;
        }

        Debug.Log("Player returned to the bed. Current phase: " + CurrentPhase);
    }
}
