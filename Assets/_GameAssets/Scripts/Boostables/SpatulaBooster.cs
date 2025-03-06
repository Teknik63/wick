using UnityEngine;

public class SpatulaBooster : MonoBehaviour, IBoostables
{
    [Header("References")]
    [SerializeField] private Animator _spatulaAnimator;

    [Header("Settings")]
    [SerializeField] private float _jumpforce;


    private bool isActivated;


    public void Boost(PlayerController playerController)
    {
        if(isActivated) { return; }
        PlayboostAnimation();
        Rigidbody playerRigidbody = playerController.GetPlayerRigidbody();
        playerRigidbody.linearVelocity = new Vector3(playerRigidbody.linearVelocity.x, 0f, playerRigidbody.linearVelocity.y);
        playerRigidbody.AddForce(transform.forward * _jumpforce, ForceMode.Impulse);
        isActivated = true;
        Invoke(nameof(ResetBoostAnimation), 0.2f);
    }

    private void PlayboostAnimation()
    {
        _spatulaAnimator.SetTrigger(Consts.Spatula_Anim.IS_SPATULA_Anim);
    }

    private void ResetBoostAnimation()
    {
        isActivated = false;
    }
}
