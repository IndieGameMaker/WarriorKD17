using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // [SerializeField] private InputActionAsset inputActions;

    [SerializeField] private InputActionProperty moveAction;
    [SerializeField] private InputActionProperty attackAction;

    public float moveSpeed = 8.0f;
    public float turnSpeed = 10.0f;

    private CharacterController cc;
    private CinemachineCamera cinemachineCamera;

    private Vector2 moveInput;

    private void Awake()
    {
        // 액션을 찾아서 할당
        // moveAction = inputActions.FindAction("Move");
        // attackAction = inputActions.FindAction("Attack");

        cc = GetComponent<CharacterController>();
        cinemachineCamera = GameObject.Find("CinemachineCamera")?.GetComponent<CinemachineCamera>();
    }

    private void OnEnable()
    {
        // 액션을 활성화
        // moveAction.Enable();
        // attackAction.Enable();

        // 이동처리 이벤트 등록
        moveAction.action.performed += OnMove;
        moveAction.action.canceled += (ctx) => moveInput = Vector2.zero;

        // 공격 이벤트 등록
        attackAction.action.performed += OnAttack;
    }

    private void OnDisable()
    {
        // 이동처리 이벤트 해지
        moveAction.action.performed -= OnMove;
        moveAction.action.canceled -= (ctx) => moveInput = Vector2.zero;

        // 공격 이벤트 해지
        attackAction.action.performed -= OnAttack;

        // 액션을 비활성화
        // moveAction.Disable();
        // attackAction.Disable();
    }

    private void Update()
    {
        // 입력값이 없을 경우 실행하지 않는다.
        if (moveInput.sqrMagnitude < 0.1f * 0.1f) return;

        // 시네머신 카메라의 Forward와 Right 방향벡터 추출
        Vector3 camForward = cinemachineCamera.transform.forward;
        Vector3 camRight = cinemachineCamera.transform.right;

        // 높이를 0 설정
        camForward.y = camRight.y = 0.0f;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDir = (camForward * moveInput.y) + (camRight * moveInput.x);
        moveDir.Normalize();

        //cc.SimpleMove(방향 * 속도)
        cc.Move(moveDir * moveSpeed * Time.deltaTime);
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        Debug.Log("공격");
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log($"Move : {moveInput}");
    }
}
