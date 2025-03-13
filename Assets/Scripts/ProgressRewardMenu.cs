using UnityEngine;

public class ProgressRewardMenu : MonoBehaviour
{
    private const string SaveKeyScene = "saveScene";

    [SerializeField] private Coin _coin;
    [SerializeField] private SliderView _sliderView;
    [SerializeField] private RewardMeneger _rewardMeneger;

    private int _currentValue;

    private int _reward = 100;
    private int _targetValue = 10;

    private void Start()
    {
        ValueChange();

        if (_currentValue == 0)
            GetRewardPassing(_reward);
    }

    private void GetRewardPassing(int coin)
    {
        _coin.Add(coin);
        _rewardMeneger.RewardPileOfCoin();
    }

    private void ValueChange()
    {
        _currentValue = LoadScene() % _targetValue;

        _sliderView.SliderValueChange(_currentValue, _targetValue);
    }

    private int LoadScene()
    {
        SaveData.SceneController data = SaveManager.Load<SaveData.SceneController>(SaveKeyScene);
        return data.CurrentScene;
    }
}
