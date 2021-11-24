using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	public CharacterController playerController;
	public Animator playerAnimator;
	public Camera mainCamera;

	public float rotationSpeed = 100f;


	private Vector2 movementInput = Vector2.zero;

	private Vector3 RightDirection
	{
		get
		{
			Vector3 direction = mainCamera.gameObject.transform.right;
			//direction.y = 0;

			return direction.normalized;
		}
	}

	private Vector3 ForwardDirection
	{
		get
		{
			Vector3 direction = mainCamera.gameObject.transform.forward;
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
		Vector3 movement = new Vector3();
		movement += RightDirection * movementInput.x;
		movement += ForwardDirection * movementInput.y;

		playerController.SimpleMove(movement);
		playerAnimator.SetFloat("MovementX", movement.x);
		playerAnimator.SetFloat("MovementY", movement.y);
		playerAnimator.SetFloat("MovementZ", movement.z);

	}
}
