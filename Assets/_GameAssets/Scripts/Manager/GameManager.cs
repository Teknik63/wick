using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }
    public  event Action<GameState > OnGameStateChanged;

    [Header("Refrences")]
    [SerializeField] private EggCounterUI eggCounterUI;
    [SerializeField] private WinLoseUI _winLoseUI;
    [Header("Settings")]

    [SerializeField] private int _maxEggCount = 5;

    private int _currentEggcount;
    private GameState _currentGameState;

    private void Awake()
    {
        Instance = this;
    }
    public void OnEggCollect()
    {
        _currentEggcount += 1;
        eggCounterUI.SetEggCounterText(_currentEggcount, _maxEggCount);

        if(_currentEggcount == _maxEggCount)
        {
            eggCounterUI.SetEggcompleted();
            ChangedGameState(GameState.GameOver);
            _winLoseUI.OnGameWin();
        }
    }
    private void OnEnable()
    {
        ChangedGameState(GameState.Play);
    }

    public void ChangedGameState(GameState gameState)
    {
        OnGameStateChanged?.Invoke(gameState);
        _currentGameState = gameState;
        Debug.Log("Game State :" + gameState);
    }

    public GameState GetCurrentState()
    {
        return _currentGameState;
    }
}
