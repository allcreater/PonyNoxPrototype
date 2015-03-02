using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerBehavior : MonoBehaviour
{
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;

    public Animator animator;

    private Vector3 moveDirection = Vector3.zero;
    private bool grounded = false;

	void Start ()
    {
	}
	
    void FixedUpdate()
    {
        if (grounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        moveDirection += (Physics.gravity * Time.deltaTime);
        
        var characterController = GetComponent<CharacterController>();
        CollisionFlags flags = characterController.Move(moveDirection * Time.deltaTime);
        grounded = (flags & CollisionFlags.CollidedBelow) != 0;

        animator.SetFloat("Speed", characterController.velocity.z*0.3f);
    }
}
