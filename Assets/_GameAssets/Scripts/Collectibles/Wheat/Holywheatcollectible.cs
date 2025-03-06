using UnityEngine;

public class Holywheatcollectible : MonoBehaviour,ICollectible
{
    [SerializeField] private PlayerController _playController;
    [SerializeField] private float _IncreaseJumpForce;
    [SerializeField] private int _resetBoostDuration;


    public void Collect()
    {
        _playController.SetJumpforce(_IncreaseJumpForce, _resetBoostDuration);
        Destroy(gameObject);
    }
}
