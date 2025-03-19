using DG.Tweening;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform _timerRottableTransfrom;
    [SerializeField] private TMP_Text _timerText;

    [Header("Settings")]
    [SerializeField] private float _rotationDuration;
    [SerializeField] private Ease _ease;

    private float _elapsedTimer;

    private void Awake()
    {
        
    }
    private void Start()
    {
        PlayRotationAnimation();
        StartTimer();
    }

    private void PlayRotationAnimation()
    {
        _timerRottableTransfrom.DORotate(new Vector3(0f, 0f, -360f), _rotationDuration,RotateMode.FastBeyond360).SetLoops(-1,LoopType.Restart).SetEase(_ease);
    }

    private void StartTimer()
    {
        _elapsedTimer = 0f;
        InvokeRepeating("UpdateTimer", 0f, 1f);
    }

    private void UpdateTimer()
    {
        _elapsedTimer += 1f;
        int minutes = Mathf.FloorToInt(_elapsedTimer / 60f);
        int second = Mathf.FloorToInt(_elapsedTimer % 60f);

        _timerText.text = string.Format("{0:00}:{1:00}", minutes, second);
    }
}
