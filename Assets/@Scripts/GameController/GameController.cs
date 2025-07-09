using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    Ready,
    Playing,
    GameOver
}

public class GameController : MonoBehaviour
{
    [Header("������ ������Ʈ")]
    public RailSpawner railSpawner;
    public TraySpawner traySpawner;
    public GameObject endGamePanel;
    public Button playNowButton;

    private GameState _state = GameState.Ready;

    // ���� �ð�
    [LunaPlaygroundField("Game End Delay (sec)", 20, "Delay in seconds before game ends after action")]
    public int endTime = 20;

    private void Awake()
    {
        endGamePanel.SetActive(false);
        playNowButton.onClick.AddListener(() =>
        {
            Luna.Unity.Playable.InstallFullGame();
            Luna.Unity.LifeCycle.GameEnded();
        });
    }

    void Start()
    {
        StartGame();
        DOVirtual.DelayedCall(endTime, EndGame);
    }

    public void StartGame()
    {
        if (_state != GameState.Ready)
            return;

        _state = GameState.Playing;

        if (railSpawner != null)
            railSpawner.BeginSpawning();

        if (traySpawner != null)
            traySpawner.BeginSpawning();
    }

    public void EndGame()
    {
        if (_state != GameState.Playing)
            return;

        _state = GameState.GameOver;

        if (railSpawner != null)
            railSpawner.StopSpawning();

        if (traySpawner != null)
            traySpawner.StopSpawning();

        // TODO ���� ��� ǥ��
        endGamePanel.SetActive(true);
    }

    public GameState GetState()
    {
        return _state;
    }
}