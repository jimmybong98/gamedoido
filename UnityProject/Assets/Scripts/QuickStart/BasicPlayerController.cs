using UnityEngine;

namespace Gamedoido
{
    [RequireComponent(typeof(CharacterController))]
    public class BasicPlayerController : MonoBehaviour
    {
        public float walkSpeed = 4f;
        public float sprintSpeed = 6.5f;
        public float jumpHeight = 1.2f;
        public float gravity = -18f;
        public float lookSensitivity = 2.2f;
        public float maxLookAngle = 80f;
        public float footstepInterval = 0.5f;

        private CharacterController _controller;
        private NoiseEmitter _noiseEmitter;
        private Transform _cameraTransform;
        private float _verticalVelocity;
        private float _cameraPitch;
        private float _footstepTimer;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _noiseEmitter = GetComponent<NoiseEmitter>();
            Camera playerCamera = GetComponentInChildren<Camera>();
            if (playerCamera != null)
            {
                _cameraTransform = playerCamera.transform;
            }
        }

        private void Start()
        {
            LockCursor(true);
        }

        private void Update()
        {
            HandleLook();
            HandleMovement();
            HandleCursorToggle();
        }

        private void HandleLook()
        {
            if (_cameraTransform == null || Cursor.lockState != CursorLockMode.Locked)
            {
                return;
            }

            float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

            transform.Rotate(Vector3.up * mouseX);

            _cameraPitch -= mouseY;
            _cameraPitch = Mathf.Clamp(_cameraPitch, -maxLookAngle, maxLookAngle);
            _cameraTransform.localRotation = Quaternion.Euler(_cameraPitch, 0f, 0f);
        }

        private void HandleMovement()
        {
            if (_controller == null)
            {
                return;
            }

            float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;
            Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            Vector3 move = transform.TransformDirection(moveInput.normalized);
            _controller.Move(move * speed * Time.deltaTime);

            if (_controller.isGrounded && _verticalVelocity < 0f)
            {
                _verticalVelocity = -2f;
            }

            if (_controller.isGrounded && Input.GetButtonDown("Jump"))
            {
                _verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            _verticalVelocity += gravity * Time.deltaTime;
            _controller.Move(Vector3.up * _verticalVelocity * Time.deltaTime);

            HandleFootsteps(moveInput);
        }

        private void HandleFootsteps(Vector3 moveInput)
        {
            if (_noiseEmitter == null || !_controller.isGrounded)
            {
                return;
            }

            if (moveInput.sqrMagnitude <= 0.01f)
            {
                _footstepTimer = 0f;
                return;
            }

            _footstepTimer += Time.deltaTime;
            if (_footstepTimer >= footstepInterval)
            {
                _noiseEmitter.EmitFootstep();
                _footstepTimer = 0f;
            }
        }

        private void HandleCursorToggle()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                LockCursor(Cursor.lockState != CursorLockMode.Locked);
            }
        }

        private void LockCursor(bool shouldLock)
        {
            Cursor.lockState = shouldLock ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !shouldLock;
        }
    }
}
