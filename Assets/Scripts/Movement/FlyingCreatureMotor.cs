using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class FlyingCreatureMotor : LivingCreatureMotor
{
    public Animator m_Animator;

    private Rigidbody m_rigidBody;
	void Start ()
	{
        m_rigidBody = GetComponent<Rigidbody>();
        m_rigidBody.useGravity = false; 
	}
    
    void Update()
    {
        m_Animator.SetBool("IsDead", !LivingCreature.IsAlive);
        m_Animator.SetBool("IsFlying", m_rigidBody.velocity.magnitude > 0.5f);
    }

    //Will work only for alive creature
    protected override void FixedUpdateImpl()
    {
		if (MovementDirection.sqrMagnitude > 0)
			m_rigidBody.angularVelocity = GetAngleVelocitiesToRotate (Quaternion.LookRotation (MovementDirection.normalized)) * 1.5f;
		else
			m_rigidBody.angularVelocity = new Vector3 ();
        m_rigidBody.AddForce(MovementImpulse, ForceMode.Impulse);
    }

    protected override void OnDeath()
    {
        m_rigidBody.useGravity = true;
        m_rigidBody.constraints = RigidbodyConstraints.None;
    }
}
