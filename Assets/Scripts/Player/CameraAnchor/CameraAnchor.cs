using UnityEngine;
using UnityEngine.InputSystem;

public class CameraAnchor : MonoBehaviour
{
    [SerializeField] private Transform playerTransform; // Reference to the player's transform
	[SerializeField] private float mouseFollowAlpha; // Offset from the mouse position
	[SerializeField] private Vector3 offset; // Offset from the player position

	public Vector3 GetTarget()
    {
		Vector3 target = (playerTransform.position + offset);
		Vector3 target2 = Mouse.current.position.ReadValue();
		Vector2 preDiff = target2 - new Vector3(Screen.width / 2, Screen.height / 2);
		Vector3 diff = new Vector3(preDiff.x, 0, preDiff.y * (90f / Camera.main.transform.rotation.eulerAngles.x));
		float mag = Mathf.Clamp(diff.magnitude / (Screen.height/2f), 0f, 1f);

		return target + diff * mouseFollowAlpha * mag;
	}
}
