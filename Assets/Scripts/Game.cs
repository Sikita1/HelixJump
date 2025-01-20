using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private EndGameScreen _endGameScreen;
    [SerializeField] private WinnerScreen _winnerScreen;

    [SerializeField] private BallTracking _ballTracking;
    
    private TowerBuilder _towerBuilder;

    private void OnEnable()
    {
        _winnerScreen.PlayButtonClicked += OnPlayButtonClick;
        _endGameScreen.RestartButtonClicked += OnRestartButtonClick;
        _towerBuilder.Ball.GameWin += OnGameWin;
        _towerBuilder.Ball.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        _winnerScreen.PlayButtonClicked -= OnPlayButtonClick;
        _endGameScreen.RestartButtonClicked -= OnRestartButtonClick;
        _towerBuilder.Ball.GameWin -= OnGameWin;
        _towerBuilder.Ball.GameOver -= OnGameOver;
    }

    private void Awake()
    {
        _towerBuilder = new TowerBuilder();
        _towerBuilder.Build(transform, _ballTracking);
    }

    private void Start()
    {
        _winnerScreen.Close();
        _endGameScreen.Close();
    }

    private void OnPlayButtonClick()
    {
        _winnerScreen.Close();
        StartGame();
    }

    private void OnRestartButtonClick()
    {
        _endGameScreen.Close();
        StartGame();
    }

    private void OnGameWin()
    {
        Time.timeScale = 0f;
        _winnerScreen.Open();
    }

    private void OnGameOver()
    {
        Time.timeScale = 0f;
        _endGameScreen.Open();
    }

    private void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
