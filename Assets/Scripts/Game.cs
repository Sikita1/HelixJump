using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private EndGameScreen _endGameScreen;
    [SerializeField] private WinnerScreen _winnerScreen;
    
    private FinishPlatform _finishPlatform;
    private DangerSegment[] _dangerSegments;

    private void OnEnable()
    {
        _winnerScreen.PlayButtonClicked += OnPlayButtonClick;
        _endGameScreen.RestartButtonClicked += OnRestartButtonClick;
    }

    private void OnDisable()
    {
        _winnerScreen.PlayButtonClicked -= OnPlayButtonClick;
        _endGameScreen.RestartButtonClicked -= OnRestartButtonClick;
        _finishPlatform.GameWin -= OnGameWin;

        for (int i = 0; i < _dangerSegments.Length; i++)
            _dangerSegments[i].GameOver -= OnGameOver;
    }

    private void Start()
    {
        _finishPlatform = FindObjectOfType<FinishPlatform>();
        _dangerSegments = FindObjectsOfType<DangerSegment>();

        for (int i = 0; i < _dangerSegments.Length; i++)
            _dangerSegments[i].GameOver += OnGameOver;

        _finishPlatform.GameWin += OnGameWin;
        //OnPlayButtonClick();
        //Time.timeScale = 0;
        _winnerScreen.Close();
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
