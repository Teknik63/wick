using Unity.VisualScripting;
using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _orientationTransform;
    [SerializeField] private Transform _playetVisualTransfrom;

    [Header("Settings")]
    [SerializeField] private float _rotationSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    void Update()
    {
        Vector3 viewDirection =_playerTransform.position -  new Vector3(transform.position.x, _playerTransform.position.y, transform.position.z);
        _orientationTransform.forward = viewDirection.normalized;

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 inputDirection = _orientationTransform.forward * verticalInput + _orientationTransform.right * horizontalInput;

        if(inputDirection != Vector3.zero)
        {
            _playetVisualTransfrom.forward = Vector3.Slerp(_playetVisualTransfrom.forward, inputDirection.normalized, Time.deltaTime * _rotationSpeed);
        }
        
    }
}
