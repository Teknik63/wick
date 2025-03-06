using Unity.Cinemachine;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    private PlayerController _playerController;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<ICollectible>(out var collectible))
        {
            collectible.Collect();
        }
/*
        if(other.CompareTag(Consts.WheatTypes.GOLD_WHEAT))
        {
            other.gameObject?.GetComponent<GoldWheatCollectible>().Collect();
        }
        if (other.CompareTag(Consts.WheatTypes.HOLY_WHEAT))
        {
            other.gameObject?.GetComponent<Holywheatcollectible>().Collect();
        }
        if (other.CompareTag(Consts.WheatTypes.ROTTEN_WHEAT))
        {
            other.gameObject?.GetComponent<RottenWheatcollectible>().Collect();
        }
*/
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.TryGetComponent<IBoostables>(out var boostable))
        {
            boostable.Boost(_playerController);
        }
    }
}
