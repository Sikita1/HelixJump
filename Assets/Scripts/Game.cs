using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class Game : MonoBehaviour
{
    private const string SaveKeyScene = "saveScene";

    [SerializeField] private EndGameScreen _endGameScreen;
    [SerializeField] private WinnerScreen _winnerScreen;
    [SerializeField] private MouseTracking _mouseTracking;
    [SerializeField] private Coin _coin;
    [SerializeField] private Progress _progress;
    [SerializeField] private RewardMeneger _rewardMeneger;
    [SerializeField] private RewardMeneger _rewardMenegerADV;

    [SerializeField] private PanelStartLevel _startLevel;
    [SerializeField] private TMP_Text _text;

    private TowerBuilder _towerBuilder;
    private Coroutine _coroutine;

    private int _resurrectionPrice = 400;

    public int CurrentScene = 1;

    private WaitForSecondsRealtime _wait;
    private float _delay = 3f;

    public Mouse Mouse => _towerBuilder.Mouse;
    public int MaxLevelCount => _towerBuilder.MaxLevelIndex;

    private void OnEnable()
    {
        _winnerScreen.PlayButtonClicked += OnPlayButtonClick;
        _winnerScreen.GetRewardClicked += OnGetRewardButtonClick;
        _endGameScreen.RestartButtonClicked += OnRestartButtonClick;
        _endGameScreen.ReturnButtonClicked += OnReturnButtonClick;
        _towerBuilder.Mouse.GameWin += OnGameWin;
        _towerBuilder.Mouse.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        _winnerScreen.PlayButtonClicked -= OnPlayButtonClick;
        _winnerScreen.GetRewardClicked += OnGetRewardButtonClick;
        _endGameScreen.RestartButtonClicked -= OnRestartButtonClick;
        _endGameScreen.ReturnButtonClicked -= OnReturnButtonClick;
        _towerBuilder.Mouse.GameWin -= OnGameWin;
        _towerBuilder.Mouse.GameOver -= OnGameOver;
    }

    private void Awake()
    {
        _towerBuilder = new TowerBuilder();
        _towerBuilder.Build(transform, _mouseTracking);

        _wait = new WaitForSecondsRealtime(_delay);
    }

    private void Start()
    {
        _winnerScreen.Close();
        _endGameScreen.Close();

        StartCoroutine(StartLevel());
    }

    private void OnPlayButtonClick()
    {
        if (_coroutine != null)
            StopCoroutine(PlayButtonClick());

        _coroutine = StartCoroutine(PlayButtonClick());
    }

    private IEnumerator PlayButtonClick()
    {
        _rewardMeneger.RewardPileOfCoin();
        _coin.Add(20);

        yield return _wait;

        _winnerScreen.Close();
        StartGame();
    }

    private void OnRestartButtonClick()
    {
        _endGameScreen.Close();
        StartGame();
    }

    private void OnReturnButtonClick()
    {
        ReturnGame(_resurrectionPrice);
    }

    private void OnGetRewardButtonClick()
    {
        YG2.RewardedAdvShow("0", () =>
        {
            if (_coroutine != null)
                StopCoroutine(RewardButtonClick());

            _coroutine = StartCoroutine(RewardButtonClick());
        });
    }

    private IEnumerator RewardButtonClick()
    {
        _rewardMenegerADV.RewardPileOfCoin();
        _coin.AddReward(20, 2);

        yield return _wait;

        _winnerScreen.Close();
        StartGame();
    }

    private void OnGameWin()
    {
        CurrentScene = LoadNumberScene();
        CurrentScene++;
        SaveNumberScene();
        _progress.gameObject.SetActive(false);
        Time.timeScale = 0f;
        _winnerScreen.Open();
    }

    private void OnGameOver()
    {
        _progress.gameObject.SetActive(false);
        Time.timeScale = 0f;
        _endGameScreen.Open();
    }

    private void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        _progress.gameObject.SetActive(true);
    }

    private void ReturnGame(int price)
    {
        if (_coin.CurrentCount >= price)
        {
            _endGameScreen.Close();
            _coin.TakeAway(price);
            Time.timeScale = 1f;
            _progress.gameObject.SetActive(true);
        }
    }

    private int LoadNumberScene()
    {
        var data = SaveManager.Load<SaveData.SceneController>(SaveKeyScene);

        return data.CurrentScene;
    }

    private void SaveNumberScene()
    {
        SaveManager.Save(SaveKeyScene, GetSaveScene());
    }

    private SaveData.SceneController GetSaveScene()
    {
        var data = new SaveData.SceneController()
        {
            CurrentScene = CurrentScene,
        };

        return data;
    }

    private IEnumerator StartLevel()
    {
        _startLevel.gameObject.SetActive(true);
        _text.text = MaxLevelCount.ToString();
        StartCoroutine(FadeAlpha(0, 1, 0.5f));

        yield return new WaitForSecondsRealtime(4f);

        StartCoroutine(FadeAlpha(1, 0, 0.5f));

        yield return new WaitForSecondsRealtime(0.5f);
        _startLevel.gameObject.SetActive(false);
    }

    private IEnumerator FadeAlpha(float startAlpha, float targetAlpha, float duration)
    {
        float elapsedTime = 0f;
        var canvas = _startLevel.GetComponent<CanvasGroup>();

        while (elapsedTime < duration)
        {
            canvas.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvas.alpha = targetAlpha;
    }
}
