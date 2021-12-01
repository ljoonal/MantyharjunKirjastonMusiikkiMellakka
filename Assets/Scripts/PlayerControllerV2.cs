using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerV2 : MonoBehaviour
{
    public CharacterController playerController;
    public Animator playerAnimator;

    public float moveSpeed = 5.0f;
    public float rotationSpeed = 300.0f;

    float horizontal;
    float vertical;
    private void FixedUpdate()
    {
        Vector3 moveDir = -Vector3.forward * vertical + -Vector3.right * horizontal;

        Vector3 projectCameraForward = Vector3.ProjectOnPlane(playerController.transform.forward, Vector3.up);

        transform.position += moveDir * moveSpeed * Time.deltaTime;
        Quaternion rotationToMoveDir = Quaternion.LookRotation(moveDir, Vector3.up);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationToMoveDir, rotationSpeed * Time.deltaTime);
    }
 
    public void OnMoveInput(float hori, float verti)
    {

        this.horizontal = -verti + hori;
        this.vertical = hori + verti;
        
        // run animation
        if(horizontal != 0 || vertical != 0)
        {
            playerAnimator.SetBool("isRunning", true);
        } else
        {
            playerAnimator.SetBool("isRunning", false);
        }
        
    } 

}
