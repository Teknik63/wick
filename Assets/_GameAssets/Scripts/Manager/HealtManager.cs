using UnityEngine;

public class HealtManager : MonoBehaviour
{
    [SerializeField] private int _maxHealt = 3;

    private int _currentHealt;

    private void Start()
    {
        _currentHealt = _maxHealt;   
    }

    public void Damage(int damageAmount)
    {
        if(_currentHealt > 0)
        {
            _currentHealt -= damageAmount;
            if(_currentHealt <=0 )
            {

            }
        }
        
    }

    public void Heal(int healAmount)
    {
        if(_currentHealt < _maxHealt)
        {
            _currentHealt = Mathf.Min(_currentHealt + healAmount, _maxHealt);
        }
    }
}
