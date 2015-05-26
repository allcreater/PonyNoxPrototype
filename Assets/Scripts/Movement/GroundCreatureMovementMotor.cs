using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(GroundDetector))]
public class GroundCreatureMovementMotor : LivingCreatureMotor
{
    public float m_JumpImpulse = 1000.0f;
    public Vector3 m_CenterOfMass;
    public Animator m_Animator;

    private Rigidbody m_rigidBody;
    private GroundDetector m_groundDetector;
	void Start ()
	{
        m_rigidBody = GetComponent<Rigidbody>();
        m_groundDetector = GetComponent<GroundDetector>();

        m_rigidBody.centerOfMass = m_CenterOfMass;

		OnRevive ();
	}

    public void Jump()
    {
        Vector3 direction = (m_groundDetector.GroundNormal + new Vector3(MovementImpulse.x, 0.0f, MovementImpulse.z)*0.01f).normalized;
        Debug.DrawRay(transform.position, direction, Color.blue);
        if (m_groundDetector.IsGrounded)
            m_rigidBody.AddForce(direction * m_JumpImpulse, ForceMode.Impulse);
    }

    //Will work only for alive creature
    protected override void FixedUpdateImpl()
    {
        /*
        //achtung! говноподход!
        var normalVS = transform.InverseTransformDirection(m_groundDetector.GroundNormal);
        float pitch = Mathf.Asin(normalVS.z) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, Mathf.Atan2(MovementDirection.x, MovementDirection.z) * Mathf.Rad2Deg, 0);
        */
        var normalVS = transform.InverseTransformDirection(m_groundDetector.GroundNormal);
        float pitch = Mathf.Asin(normalVS.z) * Mathf.Rad2Deg;

		var vel = GetAngleVelocitiesToRotate(transform.rotation, Quaternion.Euler(pitch, Mathf.Atan2(MovementDirection.x, MovementDirection.z) * Mathf.Rad2Deg, 0)) * 1.5f;
		m_rigidBody.angularVelocity = vel;

        if (m_groundDetector.IsGrounded)
        {
            float speedAttenuation = Mathf.Exp(-m_rigidBody.velocity.magnitude * 0.1f);
            Vector3 groundedDirection = new Vector3(MovementImpulse.x, 0.0f, MovementImpulse.z) * speedAttenuation;
            m_rigidBody.AddForce(groundedDirection, ForceMode.Impulse);
        }

        float dampeningK = 1.0f - m_rigidBody.velocity.sqrMagnitude * 0.001f;
        //m_rigidBody.velocity = m_rigidBody.velocity * dampeningK;


        float speedAhead = transform.InverseTransformDirection(m_rigidBody.velocity).z;
        if (speedAhead < 1.0f && m_groundDetector.IsGrounded && MovementDirection.magnitude > 0.5f)
            speedAhead = 10.0f;

        m_Animator.SetFloat("SpeedAhead", speedAhead);
        m_Animator.SetBool("IsJumping", !m_groundDetector.IsGrounded);
    }

    protected override void OnDeath()
    {
		m_Animator.enabled = false;
        /*
        foreach (var rb in GetComponentsInChildren<Rigidbody>())
        {
            if (rb == m_rigidBody)
                continue;

            rb.isKinematic = false;
            rb.detectCollisions = true;
            rb.transform.SetParent(null);
        }
        */
        foreach (var component in GetComponentsInChildren<PhysicsOnOff>())
            component.IsPhysicsActive = true;

        foreach (var component in GetComponentsInChildren<PhysicsOnOff>())
            if (component.m_DetachObjectOnEnablePhysics) component.transform.SetParent(null);
        /*
        foreach (var rb in GetComponentsInChildren<Rigidbody>())
            if (rb != m_rigidBody) rb.transform.SetParent(null);

        transform.DetachChildren();
         */
    }


    protected override void OnRevive()
    {
        //transform.eulerAngles = new Vector3(0, Mathf.Atan2(MovementDirection.x, MovementDirection.z) * Mathf.Rad2Deg, 0);
        MovementDirection = transform.forward * 0.01f;
		m_Animator.enabled = true;

        /*
        foreach (var rb in GetComponentsInChildren<Rigidbody>())
        {
            if (rb == m_rigidBody)
                continue;

            rb.isKinematic = true;
            rb.detectCollisions = false;
        }
        */
        foreach (var component in GetComponentsInChildren<PhysicsOnOff>())
            component.IsPhysicsActive = false;
    }
}
