using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CasterBehaviour))]
public class PlayerBehavior : MonoBehaviour
{
    public float m_force = 7500.0f;
    public float m_jumpImpulse = 1000.0f;
	public float m_GroundCheckDistance = 0.1f;

    public Animator m_animator;
    public Camera m_playerCamera;

    private Vector3 m_moveDirection = Vector3.zero;

    private bool m_isGrounded = false;
	private Vector3 m_groundNormal = Vector3.up;

	private Rigidbody m_rigidBody;
	private CasterBehaviour m_caster;

	void Start ()
    {
		m_rigidBody = GetComponent<Rigidbody>();
		m_caster = GetComponent<CasterBehaviour>();
	}

    void TestMousePointer()
    {
        Ray ray_new;
        RaycastHit hit_new;

        //if (Input.GetMouseButton(0))//Input.GetButtonDown("Fire1"))
        {
            ray_new = m_playerCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray_new, out hit_new, Mathf.Infinity, 5))
            {
                if (hit_new.collider != GetComponent<Collider>())
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
        if (m_isGrounded)
        {
			m_moveDirection = new Vector3(0, 0, Input.GetButton("Fire2") ? 1.0f : 0.0f);
            m_moveDirection = transform.TransformDirection(m_moveDirection);

			m_rigidBody.velocity = m_moveDirection * 6.0f;

            if (Input.GetButton("Jump"))
            {
				m_rigidBody.AddForce(m_groundNormal * m_jumpImpulse, ForceMode.Impulse);
            }
        }

		//m_rigidBody.AddForce(m_moveDirection * m_force);

		if (Input.GetButton("Fire1"))
		{
			m_caster.Cast(0);
		}


		CheckGroundStatus();
        TestMousePointer();

		m_animator.SetFloat("Speed", transform.InverseTransformDirection(m_rigidBody.velocity).z * 0.3f);
        m_animator.SetBool("Jump", !m_isGrounded);
    }

	void CheckGroundStatus()
	{
		RaycastHit hitInfo;
#if UNITY_EDITOR
		// helper to visualise the ground check ray in the scene view
		Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * m_GroundCheckDistance));
#endif
		// 0.1f is a small offset to start the ray from inside the character
		// it is also good to note that the transform position in the sample assets is at the base of the character
		if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance))
		{
			m_isGrounded = true;
			m_groundNormal = hitInfo.normal;
			m_animator.applyRootMotion = true;
		}
		else
		{
			m_isGrounded = false;
			m_groundNormal = Vector3.up;
			m_animator.applyRootMotion = false;
		}
	}
}
