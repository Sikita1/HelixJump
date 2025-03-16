using UnityEngine;

public class ButtonExtraHeart : MonoBehaviour
{
    [SerializeField] private ExtraHeartPanel _extraHeart;

    public void Open()
    {
        _extraHeart.gameObject.SetActive(true);
    }

    public void Close()
    {
        _extraHeart.gameObject.SetActive(false);
    }
}
