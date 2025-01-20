using System;

public class EndGameScreen : Window
{
    public event Action RestartButtonClicked;

    public override void Close()
    {
        gameObject.SetActive(false);
        WindowGroup.alpha = 0f;
        ActionButton.interactable = false;
    }

    public override void Open()
    {
        gameObject.SetActive(true);
        WindowGroup.alpha = 1f;
        ActionButton.interactable = true;
    }

    protected override void OnButtonClick()
    {
        RestartButtonClicked?.Invoke();
    }
}
