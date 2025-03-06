using UnityEngine;

public class Holywheatcollectible : MonoBehaviour,ICollectible
{
    [SerializeField] private WheatDesingSO _wheatDesingSO;
    [SerializeField] private PlayerController _playController;



    public void Collect()
    {
        _playController.SetJumpforce(_wheatDesingSO.IncreaseDecreaseMultiplier, _wheatDesingSO.ResetBoostDuration);
        Destroy(gameObject);
    }
}
