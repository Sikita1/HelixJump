using System;

public class EndGameScreen : Window
{
    public event Action RestartButtonClicked;
    public event Action ReturnButtonClicked;
    public event Action CloseButtonClicked;

    public override void Close()
    {
        gameObject.SetActive(false);
        WindowGroup.alpha = 0f;
        RestartButton.interactable = false;
        ActionButton.interactable = false;
        CommonPanel.gameObject.SetActive(false);
    }

    public override void Open()
    {
        CommonPanel.gameObject.SetActive(true);
        gameObject.SetActive(true);
        WindowGroup.alpha = 1f;
        RestartButton.interactable = true;
        ActionButton.interactable = true;
    }

    protected override void OnButtonRestartClick()
    {
        RestartButtonClicked?.Invoke();
    }

    protected override void OnButtonReturnClick()
    {
        ReturnButtonClicked?.Invoke();
    }

    protected override void OnCloseButtonClick()
    {
        CloseButtonClicked?.Invoke();
    }
}
