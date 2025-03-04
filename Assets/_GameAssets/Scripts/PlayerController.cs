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


    private Rigidbody _rigidbodyPlayer;
    private float _horizontalInput, _verticalInput;
    private Vector3 _movementDirection;
    private bool isSliding;

    private void Awake()
    {
        _rigidbodyPlayer = GetComponent<Rigidbody>();
        _rigidbodyPlayer.freezeRotation = true;
    }
    private void Update()
    {
        SetInputs();
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
        else if(Input.GetKey(_jumpKey) && _canJump && isGrounded())
        {
            _canJump = false;
            SetPlayerJumping();
            Invoke("ResetJumping", _jumpCoolDown);
        }
    }

    private void SetPlayerMovement()
    {
        _movementDirection = _orientationTransform.forward * _verticalInput + _orientationTransform.right * _horizontalInput;
        if(isSliding)
        {
            _rigidbodyPlayer.AddForce(_movementDirection.normalized * _movementSpeed *_slideMultiplier, ForceMode.Force);

        }
        else
        {
            _rigidbodyPlayer.AddForce(_movementDirection.normalized * _movementSpeed, ForceMode.Force);
        }

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
        if(isSliding)
        {
            _rigidbodyPlayer.linearDamping = _slideDrag;
        }
        else
        {
            _rigidbodyPlayer.linearDamping = _groundDrag;
        } 
    }



    private void ResetJumping()
    {
        _canJump = true;
    }

    private bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f,_groundLayer);
    }
}
