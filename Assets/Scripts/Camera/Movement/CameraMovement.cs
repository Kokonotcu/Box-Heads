using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private CameraAnchor target;
		
	[Header("Movement Settings")]
	[SerializeField] private float cameraSpeed = 10.0f;
	[SerializeField] private float triggerDistance = 0.2f;

	private void LateUpdate()
	{
		Vector3 diff = target.GetTarget() - transform.position;
		if (diff.sqrMagnitude > triggerDistance)
			transform.position += diff * Time.deltaTime * cameraSpeed;   
	}
}
