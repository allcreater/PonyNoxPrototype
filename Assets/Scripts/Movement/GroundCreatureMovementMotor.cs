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
        if (m_groundDetector.IsGrounded)
            m_rigidBody.AddForce(m_groundDetector.GroundNormal * m_JumpImpulse, ForceMode.Impulse);
    }
    private Vector3 GetAngleVelocitiesToRotate(Vector3 sourceAngles, Vector3 desiredAngles)
    {
        var source = Quaternion.Euler(sourceAngles);
        var target = Quaternion.Euler(desiredAngles);

        var deltaAngles = (source * Quaternion.Inverse(target)).eulerAngles;

        return new Vector3(
            Mathf.DeltaAngle(deltaAngles.x, 0),
            Mathf.DeltaAngle(deltaAngles.y, 0),
            Mathf.DeltaAngle(deltaAngles.z, 0)
            ) / Mathf.PI;
    }
    private Vector3 GetAngleVelocitiesToRotate(Quaternion source, Quaternion target)
    {
        var deltaAngles = (source * Quaternion.Inverse(target)).eulerAngles;

        return new Vector3(
            Mathf.DeltaAngle(deltaAngles.x, 0),
            Mathf.DeltaAngle(deltaAngles.y, 0),
            Mathf.DeltaAngle(deltaAngles.z, 0)
            ) / Mathf.PI;
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

        m_rigidBody.angularVelocity = GetAngleVelocitiesToRotate(transform.rotation, Quaternion.Euler(pitch, Mathf.Atan2(MovementDirection.x, MovementDirection.z) * Mathf.Rad2Deg, 0)) * 1.5f;
        
        if (m_groundDetector.IsGrounded)
        {
            float speedAttenuation = Mathf.Exp(-m_rigidBody.velocity.magnitude * 0.1f);
            Vector3 groundedDirection = new Vector3(MovementImpulse.x, 0.0f, MovementImpulse.z) * speedAttenuation;
            m_rigidBody.AddForce(groundedDirection, ForceMode.Impulse);
        }

        float dampeningK = 1.0f - m_rigidBody.velocity.sqrMagnitude * 0.001f;
        //m_rigidBody.velocity = m_rigidBody.velocity * dampeningK;

        m_Animator.SetFloat("SpeedAhead", transform.InverseTransformDirection(m_rigidBody.velocity).z);
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
        transform.eulerAngles = new Vector3(0, Mathf.Atan2(MovementDirection.x, MovementDirection.z) * Mathf.Rad2Deg, 0);

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
