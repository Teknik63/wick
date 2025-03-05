using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _orientationTransform;

    [Header("Movement Settings")]
    [SerializeField] private float _movementSpeed ;
    [SerializeField] private KeyCode _movementKey;


    [Header("Jump Settings")]
    [SerializeField] private KeyCode _jumpKey;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _airMultiplier;
    [SerializeField] private float _airDrag;
    [SerializeField] private bool _canJump;
    [SerializeField] private float _jumpCoolDown;

    [Header("Slide Settings")]
    [SerializeField] private KeyCode _slideKey;
    [SerializeField] private float _slideMultiplier;
    [SerializeField] private float _slideDrag;




    [Header("Ground Check Setting")]
    [SerializeField] private float _playerHeight;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _groundDrag;

    private StateController _stateController;
    private Rigidbody _rigidbodyPlayer;
    private float _horizontalInput, _verticalInput;
    private Vector3 _movementDirection;
    private bool isSliding;

    private void Awake()
    {
        _stateController = GetComponent<StateController>();
        _rigidbodyPlayer = GetComponent<Rigidbody>();
        _rigidbodyPlayer.freezeRotation = true;
    }
    private void Update()
    {
        SetInputs();
        SetStates();
        SetPlayerDrag();
        LimitPlayerSpeed();
        
    }
    private void FixedUpdate()
    {
        SetPlayerMovement();
        
    }

    private void SetInputs()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(_slideKey))
        {
            isSliding = true;
        }
        else if(Input.GetKey(_movementKey))
        {
            isSliding = false;
        }
        else if(Input.GetKey(_jumpKey) && _canJump && IsGrounded())
        {
            _canJump = false;
            SetPlayerJumping();
            Invoke("ResetJumping", _jumpCoolDown);
        }
    }

    private void SetStates()
    {
        var movementDireciton = GetMovementDireciton();
        var isGrounded = IsGrounded();
        var isSliding = Isliding();
        var currentState = _stateController.GetCurrentState();

        var newState = currentState switch
        {
            _ when movementDireciton == Vector3.zero && isGrounded && !isSliding => PlayerState.Idle,
            _ when movementDireciton != Vector3.zero && isGrounded && !isSliding => PlayerState.Move,
            _ when movementDireciton != Vector3.zero && isGrounded && isSliding => PlayerState.Slide,
            _ when movementDireciton == Vector3.zero && isGrounded && isSliding => PlayerState.SlideIdle,
            _ when !_canJump && !isGrounded => PlayerState.Jump,
            _ => currentState
        };
        
        if(newState != currentState)
        {
            _stateController.ChangeState(newState);
        }
    }

    private void SetPlayerMovement()
    {
        _movementDirection = _orientationTransform.forward * _verticalInput + _orientationTransform.right * _horizontalInput;

        float forceMultiplier = _stateController.GetCurrentState() switch
        {
            PlayerState.Move => 1f,
            PlayerState.Slide => _slideMultiplier,
            PlayerState.Jump => _airMultiplier,
            _ => 1f
        };

        _rigidbodyPlayer.AddForce(_movementDirection.normalized * _movementSpeed * forceMultiplier, ForceMode.Force);

    }
    private void LimitPlayerSpeed()
    {
        Vector3 flatVelocity = new Vector3(_rigidbodyPlayer.linearVelocity.x, 0f, _rigidbodyPlayer.linearVelocity.z);
        if(flatVelocity.magnitude > _movementSpeed)
        {
            Vector3 limitVelocity = flatVelocity.normalized * _movementSpeed;
           // _rigidbodyPlayer.linearVelocity = limitVelocity;
            _rigidbodyPlayer.linearVelocity = new Vector3(limitVelocity.x,_rigidbodyPlayer.linearVelocity.y,limitVelocity.z);
        }
    }

    private void SetPlayerJumping()
    {
        _rigidbodyPlayer.linearVelocity = new Vector3(_rigidbodyPlayer.linearVelocity.x, 0f, _rigidbodyPlayer.linearVelocity.z);
        _rigidbodyPlayer.AddForce(_orientationTransform.up * _jumpForce, ForceMode.Impulse );
    }
    private void SetPlayerDrag()
    {
        _rigidbodyPlayer.linearDamping = _stateController.GetCurrentState() switch
        { 
            PlayerState.Move => _groundDrag,
            PlayerState.Slide => _slideDrag,
            PlayerState.Jump => _airDrag,
            _ => _rigidbodyPlayer.linearDamping
        };

    }



    private void ResetJumping()
    {
        _canJump = true;
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f,_groundLayer);
    }

    private Vector3 GetMovementDireciton()
    {
        return _movementDirection.normalized;
    }

    private bool Isliding()
    {
        return isSliding;
    }
}
