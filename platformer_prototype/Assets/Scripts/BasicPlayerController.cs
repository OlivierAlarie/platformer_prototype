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
    public float TimeToMaxSpeed = 1f;//Time to reach max speed (in Seconds)
    public float JumpHeight = 1f; //Target height for the character
    public float JumpLeniency = 0.15f; // Seconds of leniency where a jump attempt is registered before reaching the ground / after leaving the ground without jumping
    public float Gravity = -9.81f;

    [Header("Model")]
    public Transform RootGeometry;

    private Vector3 _targetDirection = Vector3.zero;
    private float _targetVelocityXZ = 0f;
    private float _targetVelocityChangeRate = 0f;
    private float _targetVelocityY = 0f;
    private float _terminalVelocityY = -53f;
    private bool _playerJumping = false;
    private float _playerJumpTimer = 0f;
    private float _playerCoyoteTimer = 0f;

    

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        MainCamera.transform.LookAt(CameraRoot);
        _originalCameraLocalPosition = MainCamera.transform.localPosition;
        _maxCameraDistance = Vector3.Distance(CameraRoot.transform.position, MainCamera.transform.position);

        _targetVelocityChangeRate = 1 / TimeToMaxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        CameraCheck();
        JumpAndGravityCheck();
        MovePlayer();
    }

    void CameraCheck()
    {
        //Calculate Camera Rotation based of mouse movement
        _targetRotationH += Input.GetAxis("Camera X") * CameraSensitivityX * Time.deltaTime;
        _targetRotationV += Input.GetAxis("Camera Y") * CameraSensitivityY * Time.deltaTime;

        //Clamp Vertical Rotation
        _targetRotationV = Mathf.Clamp(_targetRotationV, MaxUpwardAngle, MaxDownwardAngle);

        CameraRoot.transform.rotation = Quaternion.Euler(_targetRotationV, _targetRotationH, 0.0f);
        //Check for Environement Collision

        RaycastHit hit;
        Vector3 dir = MainCamera.transform.position - CameraRoot.transform.position;
        bool collided = Physics.Raycast(CameraRoot.transform.position, dir.normalized, out hit, _maxCameraDistance);
        if (collided && hit.collider.name != "PlayerCharacter")
        {
            MainCamera.transform.localPosition = CameraRoot.transform.InverseTransformPoint(hit.point);
        }
        else
        {
            MainCamera.transform.localPosition = _originalCameraLocalPosition;
        }
    }

    void JumpAndGravityCheck()
    {
        if (_characterController.isGrounded)
        {
            _targetVelocityY = 0;
            _playerJumping = false;
            if (Input.GetButtonDown("Jump") || _playerJumpTimer > 0)
            {
                _targetVelocityY = Mathf.Sqrt(JumpHeight * -2f * Gravity);
                _playerJumping = true;
            }
            _playerJumpTimer = 0;
            _playerCoyoteTimer = JumpLeniency;
        }
        else
        {
            //Coyote Time
            //Early jump stop
            if (Input.GetButtonUp("Jump") && _playerJumping && _targetVelocityY > 0)
            {
                _targetVelocityY *= 0.5f;
            }

            //Jump Leniency
            if (Input.GetButtonDown("Jump"))
            {
                if (_playerCoyoteTimer > 0)
                {
                    _targetVelocityY = Mathf.Sqrt(JumpHeight * -2f * Gravity);
                    _playerJumping = true;
                }
                else
                {
                    _playerJumpTimer = JumpLeniency;
                }
            }
            _playerCoyoteTimer -= Time.deltaTime;
            _playerJumpTimer -= Time.deltaTime;

            _targetVelocityY += Gravity * Time.deltaTime;
            if (_targetVelocityY < _terminalVelocityY)
            {
                _targetVelocityY = _terminalVelocityY;
            }
        }
    }

    void MovePlayer()
    {
        Vector3 newDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;

        if(newDirection == Vector3.zero)
        {
            _targetVelocityXZ -= MaxSpeed * _targetVelocityChangeRate * Time.deltaTime; //10 * 1 * 0.5
            if (_targetVelocityXZ < 0)
            {
                _targetVelocityXZ = 0;
            }
        }
        else
        {
            _targetVelocityXZ += MaxSpeed * _targetVelocityChangeRate * Time.deltaTime;
            if(_targetVelocityXZ > MaxSpeed)
            {
                _targetVelocityXZ = MaxSpeed;
            }
            //Use CameraRoots rotation, making forward always in front of the camera
            _targetDirection = Quaternion.Euler(0.0f, CameraRoot.rotation.eulerAngles.y, 0.0f) * newDirection;
            RootGeometry.transform.LookAt(_characterController.transform.position + _targetDirection);
        }

        //Keep the direction to magnitude 1
        _targetDirection.y = 0;
        _targetDirection.Normalize();
        //Multiply by whatever currentVelocity we target
        _targetDirection *= _targetVelocityXZ;
        _targetDirection.y = _targetVelocityY;

        _characterController.Move(_targetDirection * Time.deltaTime);
    }
}
