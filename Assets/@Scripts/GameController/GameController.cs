using UnityEngine;

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

    private GameState _state = GameState.Ready;

    void Start()
    {
        // �ӽ� ���� �ڵ� ����
        StartGame();
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
    }

    public GameState GetState()
    {
        return _state;
    }
}