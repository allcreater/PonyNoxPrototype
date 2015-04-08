using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(GroundDetector))]
public class GroundCreatureMovementMotor : LivingCreatureMotor
{
    public float m_JumpImpulse = 1000.0f;

    public Animator m_Animator;

    private Rigidbody m_rigidBody;
    private GroundDetector m_groundDetector;
	void Start ()
	{
        m_rigidBody = GetComponent<Rigidbody>();
        m_groundDetector = GetComponent<GroundDetector>();

		OnRevive ();
	}

    public void Jump()
    {
        if (m_groundDetector.IsGrounded)
            m_rigidBody.AddForce(m_groundDetector.GroundNormal * m_JumpImpulse, ForceMode.Impulse);
    }

    //Will work only for alive creature
    protected override void FixedUpdateImpl()
    {
        //achtung! говноподход!
        //transform.eulerAngles = new Vector3(0, Mathf.Atan2(MovementDirection.x, MovementDirection.z) * Mathf.Rad2Deg, 0);
        /*
        var normalVS = transform.InverseTransformDirection(m_groundDetector.GroundNormal);
        float pitch = Mathf.Asin(normalVS.z) * Mathf.Rad2Deg;
        */

        transform.eulerAngles = new Vector3(0, Mathf.Atan2(MovementDirection.x, MovementDirection.z) * Mathf.Rad2Deg, 0);

        if (m_groundDetector.IsGrounded)
        {
            Vector3 groundedDirection = new Vector3(MovementImpulse.x, 0.0f, MovementImpulse.z);
            m_rigidBody.AddForce(groundedDirection, ForceMode.Impulse);
        }

        float dampeningK = 1.0f - m_rigidBody.velocity.sqrMagnitude * 0.001f;
        m_rigidBody.velocity = m_rigidBody.velocity * dampeningK;

        m_Animator.SetFloat("SpeedAhead", transform.InverseTransformDirection(m_rigidBody.velocity).z * 0.3f);
        m_Animator.SetBool("IsJumping", !m_groundDetector.IsGrounded);
    }

    protected override void OnDeath()
    {
        m_rigidBody.constraints = RigidbodyConstraints.None;
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
        m_rigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
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
