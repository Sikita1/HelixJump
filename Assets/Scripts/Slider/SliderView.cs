using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderView : MonoBehaviour
{
    private Slider _slider;

    private Coroutine _coroutine;
    private WaitForSeconds _wait;

    private float _delay = 0.002f;
    private float _maxDelta = 0.1f;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _wait = new WaitForSeconds(_delay);
    }

    public void SliderValueChange(float count, float maxValue)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(SmoothSliderChange(count, maxValue));
    }

    private IEnumerator SmoothSliderChange(float target, float maxValue)
    {
        _slider.maxValue = maxValue;

        while (_slider.value != target)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, target, _maxDelta);
            yield return _wait;
        }
    }
}
