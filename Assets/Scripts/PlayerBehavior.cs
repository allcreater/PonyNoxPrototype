using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CasterBehaviour))]
[RequireComponent(typeof(GroundDetector))]
public class PlayerBehavior : MonoBehaviour
{
    public float m_force = 7500.0f;
    public float m_jumpImpulse = 1000.0f;
	public float m_GroundCheckDistance = 0.1f;

    public Animator m_animator;
    public Camera m_playerCamera;

    private Vector3 m_moveDirection = Vector3.zero;
    private Vector3 m_targetPosition = Vector3.zero;

    private GroundDetector m_groundDetector;
	private Rigidbody m_rigidBody;
	private CasterBehaviour m_caster;
    private Collider m_collider;


    public Transform m_sceletonTransform; //TODO
	void Start ()
    {
		m_rigidBody = GetComponent<Rigidbody>();
		m_caster = GetComponent<CasterBehaviour>();
        m_collider = GetComponent<Collider>();
        m_groundDetector = GetComponent<GroundDetector>();
	}

    void TestMousePointer()
    {
        Ray ray_new;
        RaycastHit hit_new;

        //if (Input.GetMouseButton(0))//Input.GetButtonDown("Fire1"))
        {
            ray_new = m_playerCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray_new, out hit_new, Mathf.Infinity, 1))
            {
                if (hit_new.collider != m_collider)
                {
                    //var pos = new Vector3(hit_new.point.x, Terrain.activeTerrain.SampleHeight(hit_new.point) + 1, hit_new.point.z);

                    var normalVS = transform.InverseTransformDirection(m_groundDetector.GroundNormal);
                    //m_animator.SetFloat("x", normalVS.x); m_animator.SetFloat("y", normalVS.y); m_animator.SetFloat("z", normalVS.z);
                    float pitch = Mathf.Asin(normalVS.z) * Mathf.Rad2Deg;
                    //m_animator.SetFloat("x", pitch);

                    var dir = hit_new.point - transform.position;
                    transform.eulerAngles = new Vector3(0, Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg, 0);

                    m_sceletonTransform.localEulerAngles = new Vector3(pitch, 180, 0);

                    m_targetPosition = hit_new.point;
                }
            }
        }
    }

    void FixedUpdate()
	{
        var lc = GetComponent<LivingCreatureBehaviour>();
        if (lc.IsAlive)
            m_rigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        else
        {
            m_rigidBody.constraints = RigidbodyConstraints.None;
            return;
        }

        if (m_groundDetector.IsGrounded)
        {
			m_moveDirection = new Vector3(0, 0.1f, Input.GetButton("Fire2") ? 1.0f : 0.0f);
            m_moveDirection = transform.TransformDirection(m_moveDirection);

			//m_rigidBody.velocity = m_moveDirection * 6.0f;
            m_rigidBody.AddForce(m_moveDirection * 60.0f, ForceMode.Impulse);
            m_rigidBody.sleepThreshold = 0.0f;

            if (Input.GetButton("Jump"))
            {
                if (m_rigidBody.IsSleeping())
                    Debug.Log("Achtung");
                m_rigidBody.AddForce(m_groundDetector.GroundNormal * m_jumpImpulse, ForceMode.Impulse);
            }
        }
        m_rigidBody.velocity = m_rigidBody.velocity * 0.98f;


        //TODO: Ахтунг! Говнокод!
		if (Input.GetKey(KeyCode.Alpha1))
            m_caster.Cast(0, m_targetPosition);
        if (Input.GetKey(KeyCode.Alpha2))
            m_caster.Cast(1, m_targetPosition);
        if (Input.GetKey(KeyCode.Alpha3))
            m_caster.Cast(2, m_targetPosition);
        if (Input.GetKey(KeyCode.Alpha4))
            m_caster.Cast(3, m_targetPosition);
        if (Input.GetKey(KeyCode.Alpha5))
            m_caster.Cast(4, m_targetPosition);
        if (Input.GetKey(KeyCode.Alpha6))
            m_caster.Cast(5, m_targetPosition);
        if (Input.GetKey(KeyCode.Alpha7))
            m_caster.Cast(6, m_targetPosition);

        TestMousePointer();

		m_animator.SetFloat("Speed", transform.InverseTransformDirection(m_rigidBody.velocity).z * 0.3f);
        m_animator.SetBool("Jump", !m_groundDetector.IsGrounded);
    }
}
