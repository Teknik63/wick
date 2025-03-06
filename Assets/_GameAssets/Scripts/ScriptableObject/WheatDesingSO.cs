using UnityEngine;

[CreateAssetMenu(fileName = "WheatDesingSO", menuName = "ScriptableObject/WheatDesingSO")]
public class WheatDesingSO : ScriptableObject
{
 

    [SerializeField] private float _IncreaseDecreaseMultiplier;
    [SerializeField] private float _resetBoostDuration;

    public float IncreaseDecreaseMultiplier => _IncreaseDecreaseMultiplier;
    public float ResetBoostDuration => _resetBoostDuration;
    
}
