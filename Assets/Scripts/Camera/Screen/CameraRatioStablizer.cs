using UnityEngine;

public class CameraRatioStablizer : MonoBehaviour
{
	[Header("Camera Settings")]
	[SerializeField][Range(0.1f, 2.5f)] float scale = 1.0f;

    private Camera _camera;
	private float desiredSceneArea = 16 * 10;

	private void Start()
	{
		_camera = GetComponent<Camera>();
	}

	private void Update()
	{
		float screenArea = Screen.width * Screen.height;

		// Units: m/pixel
		float ratio = Mathf.Sqrt(desiredSceneArea * scale / screenArea);

		_camera.orthographicSize = (0.5f * ratio * Screen.height);
	}
}
