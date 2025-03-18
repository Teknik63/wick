using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private RectTransform _playerWalkingTransform;
    [SerializeField] private RectTransform _playerSlidingTransform;
    [SerializeField] private RectTransform _boosterSpeedTransform;
    [SerializeField] private RectTransform _boosterJumpTransform;
    [SerializeField] private RectTransform _boosterSlowTransform;

    [Header("Images")]
    [SerializeField] private Image _goldBoosterWheatImage;
    [SerializeField] private Image _holyBoosterWheatImage;
    [SerializeField] private Image _rottenBoosterWheatImage;

    [Header("Sprites")]
    [SerializeField] private Sprite _playerWalkingActiveSprite;
    [SerializeField] private Sprite _playerWalkingPassiveSprite;
    [SerializeField] private Sprite _playerSlidingActiveSprite;
    [SerializeField] private Sprite _playerSlidingPassiveSprite;


    private Image _playerWalkingImage;
    private Image _playerSlidingImage;


    [Header("Settings")]
    [SerializeField] private float _moveDuration;
    [SerializeField] private Ease _moveEase;

    public RectTransform GetBoosterSpeedTransform => _boosterSpeedTransform;
    public RectTransform GetBoosterJumpTransform => _boosterJumpTransform;
    public RectTransform GetBoosterSlowTransform => _boosterSlowTransform;

    public Image GetGoldBoosterWheatImage => _goldBoosterWheatImage;
    public Image GetHolyBoosterWheatImage => _holyBoosterWheatImage;
    public Image GetRottenBoosterWheatImage => _rottenBoosterWheatImage;

    private void Awake()
    {
        _playerWalkingImage = _playerWalkingTransform.GetComponent<Image>();
        _playerSlidingImage = _playerSlidingTransform.GetComponent<Image>();
    }
    private void Start()
    {
        _playerController.OnPlayerStateChanged += _playerController_OnPlayerJumped;
        SetStateUserInterface(_playerWalkingActiveSprite, _playerSlidingPassiveSprite, _playerWalkingTransform, _playerSlidingTransform);
    }

    private void _playerController_OnPlayerJumped(PlayerState playerState)
    {
      switch(playerState)
        {
            case PlayerState.Idle:
            case PlayerState.Move:
                SetStateUserInterface(_playerWalkingActiveSprite, _playerSlidingPassiveSprite, _playerWalkingTransform, _playerSlidingTransform);
                break;

            case PlayerState.Slide:
            case PlayerState.SlideIdle:
                SetStateUserInterface(_playerWalkingPassiveSprite, _playerSlidingActiveSprite, _playerSlidingTransform,  _playerWalkingTransform);
                break;
        }
    }

    private void SetStateUserInterface(Sprite playerWalkingSprite, Sprite playerSlidingSprite, RectTransform activeTransform, RectTransform passiveTransform)
    {
        _playerWalkingImage.sprite = playerWalkingSprite;
        _playerSlidingImage.sprite = playerSlidingSprite;

        activeTransform.DOAnchorPosX(-25f, _moveDuration).SetEase(_moveEase);
        passiveTransform.DOAnchorPosX(-61f, _moveDuration).SetEase(_moveEase);
    }

    private IEnumerator SetBoosterUserInterface(RectTransform activeTransform,Image boosterImage,Image wheatImage,Sprite activeSprite,Sprite passiveSprite,
        Sprite activeWheatSprite,Sprite passiveWheatSprite,float duraiton)
    {
        boosterImage.sprite = activeSprite;
        wheatImage.sprite = activeWheatSprite;
        activeTransform.DOAnchorPosX(25f, _moveDuration).SetEase(_moveEase);
        yield return new WaitForSeconds(duraiton);
        boosterImage.sprite = passiveSprite;
        wheatImage.sprite = passiveWheatSprite;
        activeTransform.DOAnchorPosX(61f, _moveDuration).SetEase(_moveEase);
    }

    public void playerBoosterUIAnimations(RectTransform activeTransform, Image boosterImage, Image wheatImage, Sprite activeSprite, Sprite passiveSprite,
        Sprite activeWheatSprite, Sprite passiveWheatSprite, float duraiton)
    {
        StartCoroutine(SetBoosterUserInterface(activeTransform, boosterImage, wheatImage, activeSprite, passiveSprite, activeWheatSprite, passiveWheatSprite, duraiton));
    }
}
