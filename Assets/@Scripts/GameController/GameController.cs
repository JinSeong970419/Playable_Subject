using UnityEngine;

public enum GameState
{
    Ready,
    Playing,
    GameOver
}

public class GameController : MonoBehaviour
{
    [Header("스포너 오브젝트")]
    public RailSpawner railSpawner;
    public TraySpawner traySpawner;

    private GameState _state = GameState.Ready;

    void Start()
    {
        // 임시 게임 자동 시작
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

        // TODO 게임 결과 표시
    }

    public GameState GetState()
    {
        return _state;
    }
}