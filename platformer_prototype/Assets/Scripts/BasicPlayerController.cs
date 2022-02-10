using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlayerController : MonoBehaviour
{
    [Header("Camera")]
    public Camera _mainCamera;
    public Transform _cameraRoot;
    public float _cameraSensitivityX = 360;
    public float _cameraSensitivityY = 360;
    public float _maxDownwardAngle = 20;
    public float _maxUpwardAngle = -60;

    private float _targetRotationH = 0;
    private float _targetRotationV = 0;

    [Header("Character")]
    public CharacterController _characterController;
    public float _playerMaxSpeed = 5f;
    public float _playerJumpSpeed = 5f;
    public float _playerJumpLeniency = 0f;
    public float _playerJumpGravMult = 1f;
    public float _gravity = -9.81f;
    public float _terminalVelocityY = -53f;
    public Transform _rootGeometry;

    private Vector3 _targetDirection = Vector3.zero;
    private float _targetVelocityY = 0f;
    private bool _playerJumping = false;

    private enum PlayerStates 
    {
        Idle,
        Walk,
        Jump
    }

    // Start is called before the first frame update
    void Start()
    {
        _mainCamera.transform.LookAt(_cameraRoot);
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    // Update is called once per frame
    void Update()
    {
        InputsCheck();
        CameraCheck();
        GravityCheck();
        MovePlayer();
    }

    void InputsCheck()
    {
        //Horizontal Movement
        _targetDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized;
        //Vertical Movement
        if(_characterController.isGrounded)
        {
            _playerJumping = false;
            _playerJumpGravMult = 1f;
            if (_playerJumpLeniency > 0 || Input.GetKeyDown(KeyCode.Space))
            {
                _targetVelocityY = _playerJumpSpeed;
                _playerJumping = true;
            }
        }
        else
        {

            if (Input.GetKeyUp(KeyCode.Space) && _playerJumping || _targetVelocityY <= 0)
            {
                _playerJumpGravMult = 2f;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {

            }
        }
    }

    void CameraCheck()
    {
        //Calculate Camera Rotation based of mouse movement

        _targetRotationH += Input.GetAxis("Mouse X") * _cameraSensitivityX * Time.deltaTime;
        _targetRotationV += Input.GetAxis("Mouse Y") * _cameraSensitivityY * Time.deltaTime;

        //Clamp Vertical Rotation
        _targetRotationV = Mathf.Clamp(_targetRotationV, _maxUpwardAngle, _maxDownwardAngle);

        _cameraRoot.transform.rotation = Quaternion.Euler(_targetRotationV, _targetRotationH, 0.0f);
    }

    void GravityCheck()
    {
        if (!_characterController.isGrounded)
        {
            _targetVelocityY += _gravity * _playerJumpGravMult * Time.deltaTime;
            if (_targetVelocityY < _terminalVelocityY)
            {
                _targetVelocityY = _terminalVelocityY;
            }
        }
    }

    void MovePlayer()
    {
        //Use _cameraRoots rotation, making forward always in front of the camera
        _targetDirection = Quaternion.Euler(0.0f, _cameraRoot.rotation.eulerAngles.y, 0.0f) * _targetDirection;
        //assign targetDirection to Geometry ?
        _rootGeometry.transform.LookAt(_characterController.transform.position + _targetDirection);


        _targetDirection *= _playerMaxSpeed;
        _targetDirection.y = _targetVelocityY;

        _characterController.Move(_targetDirection * Time.deltaTime);
    }
}
