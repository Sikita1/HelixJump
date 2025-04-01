using UnityEngine;
using TMPro;
using System;
using System.Collections;
using YG;

public class Energy : MonoBehaviour
{
    private const string TotalEnergy = "totalEnergy";
    private const string NextEnergyTime = "nextEnergyTime";
    private const string LastAddedTime = "lastAddedTime";

    [SerializeField] private TMP_Text _textEnergy;
    [SerializeField] private TMP_Text _textTimer;
    [SerializeField] private int _maxEnergy = 5;

    private DateTime _nextEnergyTime;
    private DateTime _lastAddedTime;

    private int _restoreDuration = 180;
    private int _maxValue = 5;

    private bool _isRestoring = false;
    private Coroutine _coroutine;

    public int CurrentEnergy { get; private set; } = 5;

    private void Start()
    {
        Load();
        CalculateEnergyAfterOffline();

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(RestoreRoutine());
    }

    private void OnEnable()
    {
        Load();
        CalculateEnergyAfterOffline();
    }

    private void OnDisable()
    {
        Save();
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    private void CalculateEnergyAfterOffline()
    {
        if (CurrentEnergy >= _maxEnergy)
            return;

        DateTime currentTime = DateTime.Now;
        DateTime storedNextEnergyTime = _nextEnergyTime;

        if (currentTime > storedNextEnergyTime)
        {
            TimeSpan timePassed = currentTime - storedNextEnergyTime;
            int energyToAdd = (int)(timePassed.TotalSeconds / _restoreDuration);

            if (energyToAdd > 0)
            {
                CurrentEnergy = Mathf.Min(_maxEnergy, CurrentEnergy + energyToAdd);

                _lastAddedTime = currentTime;
                _nextEnergyTime = storedNextEnergyTime.AddSeconds(energyToAdd * _restoreDuration);

                Save();
            }
        }
    }

    public void TakeLife()
    {
        Load();

        if (CurrentEnergy == 0)
            return;

        CurrentEnergy--;
        Save();
        UpdateEnergy();

        _nextEnergyTime = AddDuration(DateTime.Now, _restoreDuration);

        if (_isRestoring == false)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(RestoreRoutine());
        }
    }

    public void Add(int count)
    {
        Load();
        CurrentEnergy = Mathf.Min(_maxValue, CurrentEnergy + count);
        Save();

        UpdateTimer();
        UpdateEnergy();
    }

    private IEnumerator RestoreRoutine()
    {
        UpdateTimer();
        UpdateEnergy();

        _isRestoring = true;

        while (CurrentEnergy < _maxEnergy)
        {
            DateTime currentTime = DateTime.Now;
            DateTime counter = _nextEnergyTime;

            bool isAdding = false;

            while (currentTime > counter)
            {
                if (CurrentEnergy < _maxEnergy)
                {
                    isAdding = true;
                    CurrentEnergy++;
                    DateTime timeToAdd = _lastAddedTime > counter ? _lastAddedTime : counter;
                    counter = AddDuration(timeToAdd, _restoreDuration);
                }
                else
                {
                    break;
                }
            }

            if (isAdding)
            {
                _lastAddedTime = DateTime.Now;
                _nextEnergyTime = counter;
            }

            UpdateTimer();
            UpdateEnergy();
            Save();

            yield return null;
        }

        _isRestoring = false;
    }

    private DateTime AddDuration(DateTime time, int duration) =>
        time.AddSeconds(duration);

    private void IdentifyingRightLanguageInText(string language, string text)
    {
        if (YG2.lang == language)
            _textTimer.text = text;
    }

    private void UpdateTimer()
    {
        if (CurrentEnergy >= _maxEnergy)
        {
            IdentifyingRightLanguageInText("ru", "ְּÊׁ.");
            IdentifyingRightLanguageInText("en", "FULL");
            IdentifyingRightLanguageInText("tr", "TAM");

            return;
        }

        TimeSpan t = _nextEnergyTime - DateTime.Now;

        if (t.TotalSeconds <= 0)
        {
            _textTimer.text = "00:00";
            return;
        }

        int minutes = (int)t.TotalMinutes;
        int seconds = t.Seconds;

        string value = String.Format("{0:D2}:{1:D2}", minutes, seconds);

        _textTimer.text = value;
    }

    private void UpdateEnergy()
    {
        _textEnergy.text = CurrentEnergy.ToString();
    }

    private DateTime StringToDate(string date)
    {
        if (String.IsNullOrEmpty(date))
            return DateTime.Now;

        return DateTime.Parse(date);
    }

    private void Load()
    {
        CurrentEnergy = PlayerPrefs.GetInt(TotalEnergy, _maxEnergy);
        _nextEnergyTime = StringToDate(PlayerPrefs.GetString(NextEnergyTime, DateTime.Now.ToString("O")));
        _lastAddedTime = StringToDate(PlayerPrefs.GetString(LastAddedTime, DateTime.Now.ToString("O")));
    }

    private void Save()
    {
        PlayerPrefs.SetInt(TotalEnergy, CurrentEnergy);
        PlayerPrefs.SetString(NextEnergyTime, _nextEnergyTime.ToString("O"));
        PlayerPrefs.SetString(LastAddedTime, _lastAddedTime.ToString("O"));
        PlayerPrefs.Save();
    }
}