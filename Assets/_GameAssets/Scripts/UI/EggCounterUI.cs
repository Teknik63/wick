using DG.Tweening;
using TMPro;
using UnityEngine;

public class EggCounterUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text _eggCounterText;

    [Header("References")]
    [SerializeField] private Color _eggCounterColor;
    [SerializeField] private float _colorDuration;
    [SerializeField] private float _scaleDuration;

    private RectTransform _eggCounterRectTransform;


    private void Awake()
    {
        _eggCounterRectTransform = _eggCounterText.GetComponent<RectTransform>();
    }

    public void SetEggCounterText(int counter, int max)
    {
        _eggCounterText.text = counter.ToString() + "/" + max.ToString();
    }

    public void SetEggcompleted()
    {
        _eggCounterText.DOColor(_eggCounterColor, _colorDuration);
        _eggCounterRectTransform.DOScale(1.2f,_scaleDuration).SetEase(Ease.OutBack);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
