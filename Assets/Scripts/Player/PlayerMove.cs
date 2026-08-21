using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed;   //移動スピード
    [SerializeField] private float horizontalSpeed; //左右スピード

    private Rigidbody rb;
    private PlayerInput input;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInput>();
    }

    private void FixedUpdate()
    {
        var moveVec = input.actions["Move"].ReadValue<Vector2>();
        rb.linearVelocity = new Vector3(moveVec.x * horizontalSpeed, 0, moveSpeed);
    }
}
