using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private const string SaveKeyScene = "saveScene";

    [SerializeField] private TMP_Text _nextSceneText;
    [SerializeField] private TMP_Text _currentSceneText;
    [SerializeField] private TMP_Text _previousLevel;
    [SerializeField] private TMP_Text _previousLevel1;

    [SerializeField] private Energy _health;

    [SerializeField] private ButtonExtraHeart _buttonExtra;

    private int _currentNumberScene;

    private void Awake()
    {
        _currentNumberScene = LoadScene();

        _nextSceneText.text = (_currentNumberScene + 1).ToString();
        _currentSceneText.text = _currentNumberScene.ToString();
        _previousLevel.text = (_currentNumberScene - 1).ToString();
        _previousLevel1.text = (_currentNumberScene - 2).ToString();
    }

    public void StartLevel()
    {
        if (_health.CurrentEnergy > 0)
            SceneManager.LoadScene(0);
        else
            _buttonExtra.Open();
    }

    private int LoadScene()
    {
        SaveData.SceneController data = SaveManager.Load<SaveData.SceneController>(SaveKeyScene);
        return data.CurrentScene;
    }
}
