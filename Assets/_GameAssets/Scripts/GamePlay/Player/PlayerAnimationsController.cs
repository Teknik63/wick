using System;
using UnityEngine;

public class PlayerAnimationsController : MonoBehaviour
{
    [SerializeField] private Animator _playAnimator;


    private PlayerController _playerContoller;
    private StateController _stateController;

    private void Awake()
    {
        _playerContoller = GetComponent<PlayerController>();
        _stateController = GetComponent<StateController>();

    }

    private void Start()
    {
        _playerContoller.OnPlayerJumped += PlayerController_OnPlayerJumped;
    }



    private void Update()
    {

        if (GameManager.Instance.GetCurrentState() != GameState.Play && GameManager.Instance.GetCurrentState() != GameState.Resume)
        {
            return;
        }

        SetPlayerAnimations();
    }
    private void PlayerController_OnPlayerJumped()
    {
        _playAnimator.SetBool(Consts.PlayerAnimations.IS_JUMPING, true);
        Invoke(nameof(ResetJumping), 0.5f);
    }

    private void ResetJumping()
    {
        _playAnimator.SetBool(Consts.PlayerAnimations.IS_JUMPING, false);
        

    }

    private void SetPlayerAnimations()
    {
        var currentState = _stateController.GetCurrentState();

        switch(currentState)
        {
            case PlayerState.Idle:
                _playAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, false);
                _playAnimator.SetBool(Consts.PlayerAnimations.IS_MOVING, false);
                break;
            case PlayerState.Move:
                _playAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, false);
                _playAnimator.SetBool(Consts.PlayerAnimations.IS_MOVING, true);
                break;
            case PlayerState.SlideIdle:
                _playAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, true);
                _playAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING_ACTIVE, false);
                break;
            case PlayerState.Slide:
                _playAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, true);
                _playAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING_ACTIVE, true);
                break;
        }
    }
}
