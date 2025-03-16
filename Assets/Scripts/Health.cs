using UnityEngine;

public class Health : MonoBehaviour
{
    private const string SaveKeyHealth = "saveHealth";

    //[SerializeField] private UnscaledTimer _timer;
    [SerializeField] private HealthView _healthView;

    private int _maxValue = 5;
    private int _minValue = 0;
    //private string _lastHealthLossTimeString;

    //private Coroutine _coroutine;
    //private WaitForSecondsRealtime _wait;

    //private System.DateTime _lastHealthLossTime;

    public int CurrentHealth { get; private set; } = 5;

    //private void OnEnable()
    //{
    //    _timer.TimerExpired += OnTimerExpired;
    //}

    //private void OnDisable()
    //{
    //    _timer.TimerExpired -= OnTimerExpired;
    //}

    //private void Awake()
    //{
    //    _wait = new WaitForSecondsRealtime(_healthRegenRate);
    //}

    private void Start()
    {
        Load();

        //if (CurrentHealth < _maxValue)
        //    StartTimer();
        //else
        //    _timer.StopTimer();

        _healthView.ShowHealth(CurrentHealth);
    }

    public void Add(int count)
    {
        Load();
        CurrentHealth = Mathf.Min(_maxValue, CurrentHealth + count);
        Save();

        _healthView.ShowHealth(CurrentHealth);
    }

    public void TakeLife(int damage)
    {
        Load();
        CurrentHealth = Mathf.Max(_minValue, CurrentHealth - damage);
        //StartTimer();
        Save();

        _healthView.ShowHealth(CurrentHealth);
    }

    private void OnTimerExpired()
    {
        Add(1);
        _healthView.ShowHealth(CurrentHealth);

        Save();

        //if (CurrentHealth < _maxValue)
        //    StartTimer();
    }

    //private void StartTimer()
    //{
    //    _timer.StartTimer();
    //}

    //private void StopTimer()
    //{
    //    _timer.StopTimer();
    //}

    private void Save()
    {
        SaveManager.Save(SaveKeyHealth, GetSaveHealth());
    }

    private void Load()
    {
        var data = SaveManager.Load<SaveData.HealthController>(SaveKeyHealth);

        CurrentHealth = data.Health;
        //_lastHealthLossTimeString = data.LastHealthLossTimeString;
        //_lastHealthLossTime = System.DateTime.Parse(_lastHealthLossTimeString);

        //System.TimeSpan timeSinceLastLoss = System.DateTime.UtcNow - _lastHealthLossTime;
        //_healthRegenRate = (float)timeSinceLastLoss.TotalSeconds;
    }

    private SaveData.HealthController GetSaveHealth()
    {
        var data = new SaveData.HealthController()
        {
            Health = CurrentHealth,
        };

        return data;
    }
}
