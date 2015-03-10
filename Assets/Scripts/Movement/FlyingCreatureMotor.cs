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
    
    //Will work only for alive creature
    protected override void FixedUpdateImpl()
    {
        m_rigidBody.AddForce(MovementImpulse, ForceMode.Impulse);
    }

    protected override void OnDeath()
    {
        m_rigidBody.useGravity = true;
    }
}
