using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }

    [Header("Refrences")]
    [SerializeField] private EggCounterUI eggCounterUI;
    [Header("Settings")]

    [SerializeField] private int _maxEggCount = 5;

    private int _currentEggcount;


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
        }
    }
}
