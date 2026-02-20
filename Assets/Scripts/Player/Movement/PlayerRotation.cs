using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotation : MonoBehaviour
{
	[SerializeField] Rigidbody rigidBodyP;
	Vector2 moveInput;

	[Header("Rotation Settings")]
	[SerializeField] private float speed = 10.0f;
	[SerializeField] private Vector3 rotationOffset = Vector3.zero;

	void LateUpdate()
	{
		Vector3 targetDirection = (InputControl.Instance.MouseHit.point - transform.position);
		targetDirection.y = 0;
		targetDirection.Normalize();

		if (targetDirection != Vector3.zero)
		{
			Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

			rigidBodyP.MoveRotation(Quaternion.Slerp(
				transform.rotation,
				targetRotation * Quaternion.Euler(rotationOffset),
				speed * Time.fixedDeltaTime
			));
		}
		
	}
}
