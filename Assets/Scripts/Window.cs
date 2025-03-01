using UnityEngine;
using UnityEngine.UI;

public abstract class Window : MonoBehaviour
{
    [SerializeField] private CanvasGroup _windowGroup;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _actionButton;

    [SerializeField] private CommonPanel _commonPanel;

    protected CanvasGroup WindowGroup =>
        _windowGroup;
    protected Button RestartButton =>
        _restartButton;
    protected Button ActionButton =>
        _actionButton;

    protected CommonPanel CommonPanel =>
        _commonPanel;

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(OnButtonRestartClick);
        _actionButton.onClick.AddListener(OnButtonReturnClick);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(OnButtonRestartClick);
        _actionButton.onClick.RemoveListener(OnButtonReturnClick);
    }

    protected abstract void OnButtonRestartClick();
    protected abstract void OnButtonReturnClick();

    public abstract void Open();
    public abstract void Close();
}
