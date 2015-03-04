using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerBehavior : MonoBehaviour
{
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;

    public Animator animator;
    public Camera playerCamera;

    private Vector3 moveDirection = Vector3.zero;
    private bool grounded = false;

	void Start ()
    {
	}

    void TestMousePointer()
    {
        Ray ray_new;
        RaycastHit hit_new;

        //if (Input.GetMouseButton(0))//Input.GetButtonDown("Fire1"))
        {
            ray_new = playerCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray_new, out hit_new, Mathf.Infinity, 5))
            {
                if (hit_new.collider != collider)
                {
                    //var pos = new Vector3(hit_new.point.x, Terrain.activeTerrain.SampleHeight(hit_new.point) + 1, hit_new.point.z);

                    var dir = hit_new.point - transform.position;
                    transform.eulerAngles = new Vector3(0, Mathf.Atan2(dir.x, dir.z)*57.3f, 0);
                }
            }
        }
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

        TestMousePointer();

        animator.SetFloat("Speed", transform.InverseTransformDirection(characterController.velocity).z*0.3f);
    }
}
