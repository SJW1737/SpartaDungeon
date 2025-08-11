using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    private Vector2 curMovementInput;
    public float jumpPower;
    public LayerMask groundLayerMask;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;

    [SerializeField] private float groundRayLength = 0.6f;
    [SerializeField] private float groundRaySpread = 0.2f;
    [SerializeField] private float groundRayStartOffset = 0.05f;

    private Vector2 mouseDelta;

    [HideInInspector]
    public bool canLook = true;

    private Rigidbody _rigidbody;

    private Coroutine speedBoostCo;

    private void ApplySpeedBoost(float duration, float multiplier)
    {
        if (speedBoostCo != null) StopCoroutine(speedBoostCo);
        speedBoostCo = StartCoroutine(SpeedBoostRoutine(duration, multiplier));
    }

    private IEnumerator SpeedBoostRoutine(float duration, float multiplier)
    {
        float originalSpeed = moveSpeed;
        moveSpeed = originalSpeed * multiplier;

        float t = duration;
        while (t > 0f)
        {
            t -= Time.deltaTime;
            yield return null;
        }

        moveSpeed = originalSpeed;
        speedBoostCo = null;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook();
        }
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            curMovementInput = context.ReadValue<Vector2>();
        else if (context.phase == InputActionPhase.Canceled)
            curMovementInput = Vector2.zero;
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
        }
    }

    private void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y;
        _rigidbody.velocity = dir;
    }

    private void CameraLook()
    {
         camCurXRot += mouseDelta.y * lookSensitivity;
         camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
         cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

         transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    bool IsGrounded()
    {
    Vector3 basePos = transform.position + Vector3.up * groundRayStartOffset;

    Vector3[] origins = new Vector3[]
    {
        basePos + transform.forward * groundRaySpread,
        basePos + -transform.forward * groundRaySpread,
        basePos + transform.right * groundRaySpread,
        basePos + -transform.right * groundRaySpread,
    };

    for (int i = 0; i < origins.Length; i++)
    {
        if (Physics.Raycast(origins[i], Vector3.down, groundRayLength, groundLayerMask, QueryTriggerInteraction.Ignore))
        {
            return true;
        }
    }

    return false;
}

    public void ToggleCursor(bool toggle)
    {
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }
}
