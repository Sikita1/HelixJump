using TMPro;
using UnityEngine;

public class CoinView : MonoBehaviour
{
    [SerializeField] private TMP_Text _tMPText;

    public void ShowCoins(int count)
    {
        _tMPText.text = count.ToString();
    }
}
