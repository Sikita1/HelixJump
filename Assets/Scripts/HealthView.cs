using UnityEngine;
using TMPro;

public class HealthView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public void ShowHealth(int count)
    {
        _text.text = count.ToString();
    }
}
