using UnityEngine;
using TMPro;
using System;
using System.Collections;

public class UnscaledTimer : MonoBehaviour
{
    private const string SaveKeyTime = "SaveKeyTime";

    [SerializeField] private TMP_Text _timerText;

    private float _currentTime;
    private bool _isTimerRunning = false;

    private float _minutesInHour = 60f;
    private float _secondInMinute = 60f;

    //private float _healthRegenRate = 50f;

    private Coroutine _coroutine;

    private WaitForSecondsRealtime _wait;
    private float _delay = 1f;

    public event Action TimerExpired;

    private void OnApplicationQuit()
    {
        //SaveTimer();
    }

    private void Awake()
    {
        _wait = new WaitForSecondsRealtime(_delay);
    }

    public void StartTimer()
    {
        //Load();

        _isTimerRunning = true;
        UpdateTimer();

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(RunTimer());
    }

    public void StopTimer()
    {
        _currentTime = 50f;
        //Save();
        _isTimerRunning = false;
        TimerEnded();
    }

    private void UpdateTimer()
    {
        if (_timerText != null)
            _timerText.text = FormatTime(_currentTime);
    }

    //public void SaveTimer()
    //{
    //    Save();
    //}

    private IEnumerator RunTimer()
    {
        while (_isTimerRunning && _currentTime > 0f)
        {
            UpdateTimer();

            yield return _wait;

            _currentTime--;
        }

        if (_currentTime <= 0f)
        {
            _currentTime = 0f;
            StopTimer();
        }
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / _minutesInHour);
        int seconds = Mathf.FloorToInt(time % _secondInMinute);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void TimerEnded()
    {
        _timerText.text = "FULL";
        TimerExpired?.Invoke();
    }

    //private void Save()
    //{
    //    SaveManager.Save(SaveKeyTime, GetSaveTimer());
    //}

    //private SaveData.TimerData GetSaveTimer()
    //{
    //    var data = new SaveData.TimerData()
    //    {
    //        CurrentTime = _currentTime
    //    };

    //    return data;
    //}

    //private void Load()
    //{
    //    var data = SaveManager.Load<SaveData.TimerData>(SaveKeyTime);

    //    _currentTime = data.CurrentTime;
    //}
}