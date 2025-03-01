using UnityEngine;
using TMPro;

public class SliderProgressPlatform : MonoBehaviour
{
    [SerializeField] private TMP_Text _maxPlatformText;
    [SerializeField] private SliderView _sliderView;
    [SerializeField] private Game _game;

    private float _maxPlatformOnLevel => _game.MaxLevelCount;

    private Mouse _mouse => _game.Mouse;

    private void OnEnable()
    {
        _mouse.ChangePassPlatform += OnPassedPlatformChanged;
    }

    private void OnDisable()
    {
        _mouse.ChangePassPlatform -= OnPassedPlatformChanged;
    }

    private void Start()
    {
        _maxPlatformText.text = _maxPlatformOnLevel.ToString();
    }

    private void OnPassedPlatformChanged(float target)
    {
        _sliderView.SliderValueChange(target, _maxPlatformOnLevel);
    }
}
