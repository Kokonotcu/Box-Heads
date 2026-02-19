using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] Rigidbody rigidBodyP;
	private Vector2 moveInput;

	[Header("Movement Settings")]
	[SerializeField] private float speed = 10.0f;

	private void OnEnable()
	{
		InputControl.Instance.SubscribeStarted(Move, "Move", true);
		InputControl.Instance.SubscribeCancelled(Move, "Move");
	}
    private void OnDisable()
    {
        InputControl.Instance?.UnsubsribeAll(Move, "Move");
        InputControl.Instance?.UnsubsribeAll(Move, "Move");
    }

    private void FixedUpdate()
	{
		rigidBodyP.linearVelocity = new Vector3(moveInput.x * speed, rigidBodyP.linearVelocity.y, moveInput.y * speed);
	}

	public void Move(InputAction.CallbackContext ctx)
	{
		moveInput = ctx.ReadValue<Vector2>();
	}
}
