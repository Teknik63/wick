using UnityEngine;

public class GoldWheatCollectible : MonoBehaviour
{
    [SerializeField] private PlayerController _playController;
    [SerializeField] private float _IncreaseMovementSpeed;
    [SerializeField] private int _resetBoostDuration;


    public void Collect()
    {
        _playController.SetMovementSpeed(_IncreaseMovementSpeed, _resetBoostDuration);
        Destroy(gameObject);
    }
}
