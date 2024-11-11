using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;

    private InputAction moveAction;
    private InputAction attackAction;

    private Vector2 moveInput;

    private void Awake()
    {
        // 액션을 찾아서 할당
        moveAction = inputActions.FindAction("Move");
        attackAction = inputActions.FindAction("Attack");
    }

    private void OnEnable()
    {
        // 액션을 활성화
        moveAction.Enable();
        attackAction.Enable();

        // 이동처리 이벤트 등록
        moveAction.performed += OnMove;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log($"Move : {moveInput}");
    }
}
