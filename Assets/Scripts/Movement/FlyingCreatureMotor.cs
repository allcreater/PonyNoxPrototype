using UnityEngine;
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
	
	// Update is called once per frame
	void Update ()
	{

	}
    
    //Will work only for alive creature
    protected override void FixedUpdateImpl()
    {
        m_rigidBody.AddForce(MovementImpulse, ForceMode.Impulse);
    }
}
