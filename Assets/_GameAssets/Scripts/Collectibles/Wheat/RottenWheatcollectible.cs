using UnityEngine;

public class RottenWheatcollectible : MonoBehaviour
{
    [SerializeField] private PlayerController _playController;
    [SerializeField] private float _decraseMovementSpeed;
    [SerializeField] private int _resetBoostDuration;


    public void Collect()
    {
        _playController.SetMovementSpeed(_decraseMovementSpeed, _resetBoostDuration);
        Destroy(gameObject);
    }
}
