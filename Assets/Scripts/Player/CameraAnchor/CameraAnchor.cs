using UnityEngine;

public class CameraAnchor : MonoBehaviour
{
    [SerializeField] private Transform bodyTransform; // Reference to the player's transform
	[SerializeField] private Vector3 offset; // Offset from the player position

	// Update is called once per frame
	void Update()
    {
        transform.position = bodyTransform.position +  offset;
	}
}
