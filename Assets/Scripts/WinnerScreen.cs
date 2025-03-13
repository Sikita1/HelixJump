using System;

public class WinnerScreen : Window
{
    public event Action PlayButtonClicked;
    public event Action GetRewardClicked;
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
        PlayButtonClicked?.Invoke();
        RestartButton.interactable = false;
        ActionButton.interactable = false;
    }

    protected override void OnButtonReturnClick()
    {
        GetRewardClicked?.Invoke();
        RestartButton.interactable = false;
        ActionButton.interactable = false;
    }

    protected override void OnCloseButtonClick()
    {
        CloseButtonClicked?.Invoke();
    }
}
