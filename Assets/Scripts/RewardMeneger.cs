using UnityEngine;
using DG.Tweening;
using System.Collections;

public class RewardMeneger : MonoBehaviour
{
    [SerializeField] private GameObject _pileOfCoinParent;
    [SerializeField] private Vector3[] _initialPos;
    [SerializeField] private int _noCoin;
    [SerializeField] private RectTransform _target;
    [SerializeField] private GameObject _panel;

    private Coroutine _coroutine;

    private void Start()
    {
        _initialPos = new Vector3[_noCoin];

        for (int i = 0; i < _pileOfCoinParent.transform.childCount; i++)
        {
            _initialPos[i] = _pileOfCoinParent.transform.GetChild(i).position;
        }
    }

    private void Reset()
    {
        for (int i = 0; i < _pileOfCoinParent.transform.childCount; i++)
        {
            Transform coin = _pileOfCoinParent.transform.GetChild(i);

            coin.position = _initialPos[i];
            coin.localScale = Vector3.one;
        }
    }

    public void RewardPileOfCoin()
    {
        if (_pileOfCoinParent == null || _target == null)
        {
            Debug.LogError("Не назначены объекты в инспекторе!");
            return;
        }

        Reset();
        
        float delay = 0f;
        
        _panel.SetActive(true);
        _pileOfCoinParent.SetActive(true);

        for (int i = 0; i < _pileOfCoinParent.transform.childCount; i++)
        {
            Transform coin = _pileOfCoinParent.transform.GetChild(i);

            coin.DOScale(1f, 0.3f)
                .SetDelay(delay)
                .SetEase(Ease.OutBack)
                .SetUpdate(true);

            coin.GetComponent<RectTransform>()
                .DOMove(_target.position, 1f)
                .SetDelay(delay + 0.3f)
                .SetEase(Ease.InBack)
                .SetUpdate(true);

            coin.DOScale(0f, 0.3f)
                .SetDelay(delay + 1.8f)
                .SetEase(Ease.OutBack)
                .SetUpdate(true);

            delay += 0.1f;
        }

        if (_coroutine != null)
            StopCoroutine(ClosePanel());

        _coroutine = StartCoroutine(ClosePanel());
    }

    private IEnumerator ClosePanel()
    {
        yield return new WaitForSecondsRealtime(3f);

        _panel.SetActive(false);
    }
}
