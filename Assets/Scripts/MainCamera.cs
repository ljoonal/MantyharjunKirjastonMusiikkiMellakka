using UnityEngine;

/** Controls the main camera's position to follow the player smoothly. */
public class MainCamera : MonoBehaviour
{
	public Transform target;
	public float smoothSpeed = 12.0f;
	private Vector3 offset = new Vector3(-1.2f, 11, 6);
	public int zoom;

	void Zoom()
	{
		offset.x -= zoom;
		offset.y += zoom;
		offset.z += zoom;
		zoom = 0;
	}
	void FixedUpdate()
	{
		if (zoom != 0)
		{
			Zoom();
		}
		Vector3 desiredPosition = target.position + offset;
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.fixedDeltaTime);
		transform.position = smoothedPosition;

		transform.LookAt(target);
	}
}
