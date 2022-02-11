using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlayerController : MonoBehaviour
{
    [Header("Camera")]
    public Camera MainCamera;
    public Transform CameraRoot;
    public float CameraSensitivityX = 360;
    public float CameraSensitivityY = 360;
    public float MaxDownwardAngle = 20;
    public float MaxUpwardAngle = -60;

    private float _targetRotationH = 0;
    private float _targetRotationV = 0;
    private float _maxCameraDistance;
    private Vector3 _originalCameraLocalPosition;

    [Header("Character")]
    public CharacterController _characterController;
    public float MaxSpeed = 5f; //Target speed for the character
    public float JumpHeight = 1f; //Target height for the character
    public float JumpLeniency = 0.15f; // Seconds of leniency where a jump attempt is registered before reaching the ground
    public float Gravity = -9.81f;

    [Header("Model")]
    public Transform RootGeometry;

    private Vector3 _targetDirection = Vector3.zero;
    private float _targetVelocityY = 0f;
    private float _terminalVelocityY = -53f;
    private bool _playerJumping = false;
    private float _playerJumpTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        MainCamera.transform.LookAt(CameraRoot);
        _originalCameraLocalPosition = MainCamera.transform.localPosition;
        _maxCameraDistance = Vector3.Distance(CameraRoot.transform.position, MainCamera.transform.position);
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
        _targetDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
        //Vertical Movement
        if(_characterController.isGrounded)
        {
            
            _playerJumping = false;
            if (Input.GetKeyDown(KeyCode.Space) || _playerJumpTimer > 0)
            {
                _targetVelocityY = Mathf.Sqrt(JumpHeight * -2f * Gravity);
                _playerJumping = true;
            }
            _playerJumpTimer = 0;
        }
        else
        {
            //Early jump stop
            if(Input.GetKeyUp(KeyCode.Space) && _playerJumping && _targetVelocityY > 0)
            {
                _targetVelocityY *= 0.5f;
            }

            //Jump Leniency
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _playerJumpTimer = JumpLeniency;
            }
            _playerJumpTimer -= Time.deltaTime;
        }
    }

    void CameraCheck()
    {
        //Calculate Camera Rotation based of mouse movement

        _targetRotationH += Input.GetAxis("Mouse X") * CameraSensitivityX * Time.deltaTime;
        _targetRotationV += Input.GetAxis("Mouse Y") * CameraSensitivityY * Time.deltaTime;

        //Clamp Vertical Rotation
        _targetRotationV = Mathf.Clamp(_targetRotationV, MaxUpwardAngle, MaxDownwardAngle);

        CameraRoot.transform.rotation = Quaternion.Euler(_targetRotationV, _targetRotationH, 0.0f);
        //Check for Environement Collision

        RaycastHit hit;
        Vector3 dir = MainCamera.transform.position - CameraRoot.transform.position;
        bool collided = Physics.Raycast(CameraRoot.transform.position, dir.normalized, out hit, _maxCameraDistance);
        if (collided)
        {
            MainCamera.transform.localPosition = CameraRoot.transform.InverseTransformPoint(hit.point);
        }
        else
        {
            MainCamera.transform.localPosition = _originalCameraLocalPosition;
        }
    }

    void GravityCheck()
    {
        if (!_characterController.isGrounded)
        {
            _targetVelocityY += Gravity * Time.deltaTime;
            if (_targetVelocityY < _terminalVelocityY)
            {
                _targetVelocityY = _terminalVelocityY;
            }
        }
    }

    void MovePlayer()
    {
        //Use CameraRoots rotation, making forward always in front of the camera
        _targetDirection = Quaternion.Euler(0.0f, CameraRoot.rotation.eulerAngles.y, 0.0f) * _targetDirection;
        //assign targetDirection to Geometry ?
        RootGeometry.transform.LookAt(_characterController.transform.position + _targetDirection);

        _targetDirection *= MaxSpeed;
        _targetDirection.y = _targetVelocityY;

        _characterController.Move(_targetDirection * Time.deltaTime);
    }
}
