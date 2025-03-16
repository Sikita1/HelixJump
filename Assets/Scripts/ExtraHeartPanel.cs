using UnityEngine;
using YG;

public class ExtraHeartPanel : MonoBehaviour
{
    private const string AwardExtraHeart = "3";

    [SerializeField] private Energy _energy;

    public void GetAward()
    {
        YG2.RewardedAdvShow(AwardExtraHeart, () =>
        {
            _energy.Add(1);
            gameObject.SetActive(false);
        });
    }
}
