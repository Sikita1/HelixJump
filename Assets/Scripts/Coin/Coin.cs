using UnityEngine;

public class Coin : MonoBehaviour
{
    private const string SaveKey = "mainSave";

    [SerializeField] private CoinView _coinView;

    public int CurrentCount { get; private set; } = 0;

    private void Start()
    {
        Load();
        _coinView.ShowCoins(CurrentCount);
    }

    private void OnEnable()
    {
        Load();
        _coinView.ShowCoins(CurrentCount);
    }

    public void Add(int count)
    {
        Load();
        CurrentCount += count;
        _coinView.ShowCoins(CurrentCount);
        SaveCoinCount();
    }

    public void AddReward(int count, int reward)
    {
        Load();
        CurrentCount += count * reward;
        _coinView.ShowCoins(CurrentCount);
        SaveCoinCount();
    }

    public void TakeAway(int price)
    {
        Load();
        CurrentCount -= price;
        _coinView.ShowCoins(CurrentCount);
        SaveCoinCount();
    }

    private void SaveCoinCount()
    {
        SaveManager.Save(SaveKey, GetSavaSnapshot());
    }

    private void Load()
    {
        var data = SaveManager.Load<SaveData.PlayerMoneyData>(SaveKey);

        CurrentCount = data.Coin;
    }

    private SaveData.PlayerMoneyData GetSavaSnapshot()
    {
        var data = new SaveData.PlayerMoneyData()
        {
            Coin = CurrentCount
        };

        return data;
    }
}
