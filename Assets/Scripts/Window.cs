using UnityEngine;
using UnityEngine.UI;

public abstract class Window : MonoBehaviour
{
    [SerializeField] private CanvasGroup _windowGroup;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _actionButton;
    [SerializeField] private Button _closeButton;

    [SerializeField] private CommonPanel _commonPanel;

    protected CanvasGroup WindowGroup =>
        _windowGroup;
    protected Button RestartButton =>
        _restartButton;
    protected Button ActionButton =>
        _actionButton;
    protected Button CloseButton =>
        _closeButton;
    protected CommonPanel CommonPanel =>
        _commonPanel;

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(OnButtonRestartClick);
        _actionButton.onClick.AddListener(OnButtonReturnClick);
        _closeButton.onClick.AddListener(OnCloseButtonClick);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(OnButtonRestartClick);
        _actionButton.onClick.RemoveListener(OnButtonReturnClick);
        _closeButton.onClick.RemoveListener(OnCloseButtonClick);
    }

    protected abstract void OnButtonRestartClick();
    protected abstract void OnButtonReturnClick();
    protected abstract void OnCloseButtonClick();

    public abstract void Open();
    public abstract void Close();
}
