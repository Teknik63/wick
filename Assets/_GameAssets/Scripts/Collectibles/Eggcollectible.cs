using UnityEngine;

public class Eggcollectible : MonoBehaviour, ICollectible
{
    public void Collect()
    {
        GameManager.Instance.OnEggCollect();
        Destroy(gameObject);
    }
}
