using UnityEngine;

public class CameraAnchor : MonoBehaviour
{
    [SerializeField] private Transform playerTransform; // Reference to the player's transform
	[SerializeField] private Vector3 offset; // Offset from the player position

	// Update is called once per frame
	public Vector3 GetTarget()
    {
         return (playerTransform.position +  offset);
	}
}
