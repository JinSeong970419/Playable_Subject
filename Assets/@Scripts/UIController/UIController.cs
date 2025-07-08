using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("�г�")]
    public GameObject startPanel;
    public GameObject resultPanel;

    [Header("��ư")]
    public Button startButton;
    public Button retryButton;

    [Header("���ھ� ����")]
    public Text scoreText;

    private ScoreSystem _scoreSystem;
    private GameController _gameController;

    void Awake()
    {
        ShowStartUI(true);
        ShowResultUI(false);

        if (startButton != null)
            startButton.onClick.AddListener(OnStartClicked);

        if (retryButton != null)
            retryButton.onClick.AddListener(OnRetryClicked);
    }

    public void Init(ScoreSystem scoreSystem, GameController gameController)
    {
        _scoreSystem = scoreSystem;
        _gameController = gameController;

        if (_scoreSystem != null)
            _scoreSystem.OnScoreChanged += UpdateScoreUI;
    }

    private void OnStartClicked()
    {
        if (_gameController != null)
            _gameController.StartGame();

        ShowStartUI(false);
    }

    private void OnRetryClicked()
    {
        if (_scoreSystem != null)
            _scoreSystem.ResetScore();

        // ���� ����ŸƮ ��ư �ӽ�
        //if (_gameController != null)
        //    _gameController.RestartGame();

        ShowResultUI(false);
        ShowStartUI(false);
    }

    private void UpdateScoreUI(int score)
    {
        if (scoreText != null)
            scoreText.text = score.ToString();
    }

    public void ShowStartUI(bool show)
    {
        if (startPanel != null)
            startPanel.SetActive(show);
    }

    public void ShowResultUI(bool show)
    {
        if (resultPanel != null)
            resultPanel.SetActive(show);
    }
}