using UnityEngine;
using UnityEngine.UI;

public class Holywheatcollectible : MonoBehaviour,ICollectible
{
    [SerializeField] private WheatDesingSO _wheatDesingSO;
    [SerializeField] private PlayerController _playController;

    [SerializeField] private PlayerStateUI _playerStateUI;


    private RectTransform _playerBoosterTransform;
    private Image _playerBoosterImage;

    private void Awake()
    {
        _playerBoosterTransform = _playerStateUI.GetBoosterJumpTransform;
        _playerBoosterImage = _playerBoosterTransform.GetComponent<Image>();
    }

    public void Collect()
    {
        _playController.SetJumpforce(_wheatDesingSO.IncreaseDecreaseMultiplier, _wheatDesingSO.ResetBoostDuration);
        _playerStateUI.playerBoosterUIAnimations(_playerBoosterTransform, _playerBoosterImage, _playerStateUI.GetHolyBoosterWheatImage, _wheatDesingSO.ActiveSprite, _wheatDesingSO.PassiveSprite, _wheatDesingSO.ActiveWheatSprite, _wheatDesingSO.PassiveWheatSprite, _wheatDesingSO.ResetBoostDuration);
        Destroy(gameObject);
    }
}
