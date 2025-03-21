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
    private bool _isTimerRunning;
    private Tween _rotationTween;
    private string _finalTimer;

    private void Awake()
    {
        
    }
    private void Start()
    {
        PlayRotationAnimation();
        StartTimer();
        GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;
    }

    private void GameManager_OnGameStateChanged(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Pause:
                StopTimer();
                break;
            case GameState.Resume:
                ResumeTimer();
                break;
            case GameState.GameOver:
                FinishTimer();
                break;
        }
    }

    private void PlayRotationAnimation()
    {
       _rotationTween = _timerRottableTransfrom.DORotate(new Vector3(0f, 0f, -360f), _rotationDuration,RotateMode.FastBeyond360).SetLoops(-1,LoopType.Restart).SetEase(_ease);
    }

    private void StartTimer()
    {
        _isTimerRunning = true;
        _elapsedTimer = 0f;
        InvokeRepeating(nameof(UpdateTimerUI), 0f, 1f);
    }
    private void StopTimer()
    {
        _isTimerRunning= false;
        CancelInvoke(nameof(UpdateTimerUI));
        _rotationTween.Pause();
    }

    private void ResumeTimer()
    {
        if (!_isTimerRunning) 
        {
            _isTimerRunning = true;
            InvokeRepeating(nameof(UpdateTimerUI), 0f, 1f);
            _rotationTween.Play();
        }
    }

    private void FinishTimer()
    {
        StopTimer();
        _finalTimer = GetformattedElapsedTime();
    }

    public string GetformattedElapsedTime()
    {
        int minutes = Mathf.FloorToInt(_elapsedTimer / 60f);
        int second = Mathf.FloorToInt(_elapsedTimer % 60f);

       return string.Format("{0:00}:{1:00}", minutes, second);
    }

    private void UpdateTimerUI()
    {
        if (!_isTimerRunning) { return; }

        _elapsedTimer += 1f;
        int minutes = Mathf.FloorToInt(_elapsedTimer / 60f);
        int second = Mathf.FloorToInt(_elapsedTimer % 60f);

        _timerText.text = string.Format("{0:00}:{1:00}", minutes, second);
    }

    public string GetFinalTime()
    {
        return _finalTimer;
    }
}
