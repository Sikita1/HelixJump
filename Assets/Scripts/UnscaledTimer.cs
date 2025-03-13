using UnityEngine;
using TMPro;
using System;
using System.Collections;

public class UnscaledTimer : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;

    private float _currentTime;
    private bool _isTimerRunning = false;

    private float _minutesInHour = 60f;
    private float _secondInMinute = 60f;

    private Coroutine _coroutine;

    public event Action TimerExpired;

    public void StartTimer(float startTime)
    {
        _currentTime = startTime;
        _isTimerRunning = true;
        UpdateTimer();

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(RunTimer());
    }

    public void StopTimer()
    {
        _isTimerRunning = false;
        TimerEnded();
    }

    public void UpdateTimer()
    {
        if (_timerText != null)
            _timerText.text = FormatTime(_currentTime);
    }

    private IEnumerator RunTimer()
    {
        while (_isTimerRunning && _currentTime > 0f)
        {
            UpdateTimer();

            yield return new WaitForSecondsRealtime(1f);

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
}