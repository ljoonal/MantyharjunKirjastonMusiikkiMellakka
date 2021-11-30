using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	public CharacterController playerController;
	public Animator playerAnimator;
	public Camera mainCamera;

	[Range(0f, 10f)]
	public float rotationSpeed = 1f;
	[Range(0f, 10f)]
	public float movementSpeed = 3f;


	private Vector2 movementInput = Vector2.zero;

	private Vector3 RightDirection
	{
		get
		{
			Vector3 direction = Vector3.ProjectOnPlane(mainCamera.transform.right, Vector3.up);
			//direction.y = 0;

			return direction.normalized;
		}
	}

	private Vector3 ForwardDirection
	{
		get
		{
			Vector3 direction = Vector3.ProjectOnPlane(mainCamera.transform.up, Vector3.up);
			//direction.y = 0;

			return direction.normalized;
		}
	}

	public void OnMovement(InputAction.CallbackContext value)
	{
		movementInput = value.ReadValue<Vector2>();
		
		// animation
		if(movementInput.magnitude > 0)
        {
			playerAnimator.SetBool("isRunning", true);

        }
        else
        {
			playerAnimator.SetBool("isRunning", false);
		}
		
	}


	// Update is called once per frame
	void Update()
	{
		Vector3 movement = Vector3.zero;
		movement += RightDirection * movementInput.x;
		movement += ForwardDirection * movementInput.y;
		movement = movement.normalized * Time.deltaTime * 100 * movementSpeed;

		playerController.SimpleMove(movement);

		playerAnimator.transform.rotation = Quaternion.LookRotation(movement);

		playerAnimator.SetFloat("MovementSpeed", movement.magnitude);
	}
}
