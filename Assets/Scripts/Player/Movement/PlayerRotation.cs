using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotation : MonoBehaviour
{
	[SerializeField] Rigidbody rigidBodyP;
	Vector2 moveInput;

	[Header("Rotation Settings")]
	[SerializeField] private float speed = 10.0f;
	[SerializeField] private Vector3 rotationOffset = Vector3.zero;

	private void OnEnable()
	{
		InputControl.Instance.SubscribeStarted(Rotate, "Move", true);
		InputControl.Instance.SubscribeCancelled(Rotate, "Move");
	}
    private void OnDisable()
    {
        InputControl.Instance?.UnsubsribeAll(Rotate, "Move");
        InputControl.Instance?.UnsubsribeAll(Rotate, "Move");
    }

    void FixedUpdate()
	{
		if (moveInput!= Vector2.zero)
		{
			rigidBodyP.MoveRotation(Quaternion.Slerp(
				transform.rotation,
				Quaternion.LookRotation(new Vector3(moveInput.x, 0.0f, moveInput.y)) * Quaternion.Euler(rotationOffset),
				speed * Time.deltaTime
			));
		}
	}

	public void Rotate(InputAction.CallbackContext ctx)
	{
		moveInput = ctx.ReadValue<Vector2>();
	}
}
