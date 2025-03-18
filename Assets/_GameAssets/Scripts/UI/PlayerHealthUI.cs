using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class PlayerHealthUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image[] _playerHealtImages;
    [Header("Sprites")]
    [SerializeField] private Sprite _playerHealthSprite;
    [SerializeField] private Sprite _playerUnhealthSprite;

    [SerializeField] private RectTransform[] _playerHealthRectTransform;


    [Header("Settings")]
    [SerializeField] private float _scaleDuration;

    private void Awake()
    {
        _playerHealthRectTransform = new RectTransform[_playerHealtImages.Length];

        for (int i = 0; i < _playerHealtImages.Length; i++)
        {
            _playerHealthRectTransform[i] = _playerHealtImages[i].gameObject.GetComponent<RectTransform>();
        }
    }

    public void AnimateDamage()
    {
        for (int i = 0; i < _playerHealtImages.Length; i++)
        {
            if (_playerHealtImages[i].sprite == _playerHealthSprite)
            {
                AnimateDamageSprite(_playerHealtImages[i], _playerHealthRectTransform[i]);
                break;
            }
        }
    }

    public void AnimateDamageAll()
    {
        for (int i = 0; i < _playerHealtImages.Length; i++)
        {
            AnimateDamageSprite(_playerHealtImages[i], _playerHealthRectTransform[i]);
        }
    }

    private void AnimateDamageSprite(Image activeImage,RectTransform activeImageTransform)
    {
        activeImageTransform.DOScale(0f, _scaleDuration).SetEase(Ease.InBack).OnComplete(() =>
        {
            activeImage.sprite = _playerUnhealthSprite;
            activeImageTransform.DOScale(1f, _scaleDuration).SetEase(Ease.OutBack);
        });
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(UnityEngine.Input.GetKeyDown(KeyCode.O))
        {
            AnimateDamage();
        }

        if(UnityEngine.Input.GetKeyDown(KeyCode.P))
        {
            AnimateDamageAll();
        }
    }
}
