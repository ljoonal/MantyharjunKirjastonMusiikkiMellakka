using UnityEngine;
using UnityEngine.InputSystem;

/** Controls the player character behavior. */
public class PlayerController : MonoBehaviour
{
	public CharacterController characterController;
	public Animator playerAnimator;

	public float moveSpeed = 4.0f;
	public float rotationSpeed = 300.0f;

	private Vector2 movementInput = Vector2.zero;

	/// The right axis of the camera's view projected onto the ground.
	private Vector3 RightDirection
	{
		get
		{
			return Vector3.ProjectOnPlane(Camera.main.transform.right, Vector3.up).normalized;
		}
	}

	/// The forward axis of the camera's view projected onto the ground.
	private Vector3 ForwardDirection
	{
		get
		{
			return Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up).normalized;
		}
	}

	private void FixedUpdate()
	{
		Vector3 movement = new Vector3();
		movement += RightDirection * movementInput.x;
		movement += ForwardDirection * movementInput.y;
		characterController.SimpleMove(movement * moveSpeed);

		// Rotates the player in the direction of the movement if moving.
		if (movement != Vector3.zero)
		{
			Quaternion rotationToMoveDir = Quaternion.LookRotation(movement, Vector3.up);
			playerAnimator.transform.rotation = Quaternion.RotateTowards(playerAnimator.transform.rotation, rotationToMoveDir, rotationSpeed * Time.deltaTime);
		}

		if (movement.x != 0 || movement.z != 0)
			playerAnimator.SetBool("isRunning", true);
		else playerAnimator.SetBool("isRunning", false);
	}

	public void OnMovement(InputAction.CallbackContext value)
	{
		movementInput = value.ReadValue<Vector2>();
	}

}

