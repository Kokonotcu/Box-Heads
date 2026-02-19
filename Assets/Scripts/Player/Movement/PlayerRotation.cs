using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotation : MonoBehaviour
{
	[SerializeField] Rigidbody rigidBodyP;
	Vector2 moveInput;

	[Header("Rotation Settings")]
	[SerializeField] private float speed = 10.0f;
	[SerializeField] private Vector3 rotationOffset = Vector3.zero;

	void FixedUpdate()
	{
		// 1. Get raw mouse position
		Vector2 mousePos = Mouse.current.position.ReadValue();

		// 2. Create ray directly from camera to mouse position
		Ray ray = Camera.main.ScreenPointToRay(mousePos);

		if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
		{
			// 3. Get direction on the XZ plane (keep Y at 0 to avoid tilting)
			Vector3 targetDirection = (hit.point - transform.position).normalized;
			targetDirection.y = 0;

			if (targetDirection != Vector3.zero)
			{
				// 4. Create target rotation
				Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

				// 5. Smoothly rotate Rigidbody
				rigidBodyP.MoveRotation(Quaternion.Slerp(
					transform.rotation,
					targetRotation * Quaternion.Euler(rotationOffset),
					speed * Time.fixedDeltaTime // Use fixedDeltaTime in FixedUpdate
				));
			}
		}
	}
}
