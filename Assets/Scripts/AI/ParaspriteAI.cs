using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(FlyingCreatureMotor))]
public class ParaspriteAI : MonoBehaviour
{
    public Transform m_Target;

    private FlyingCreatureMotor m_motor;

	void Start ()
	{
        m_motor = GetComponent<FlyingCreatureMotor>();
	}
	
	// Update is called once per frame
	void Update ()
	{
        var direction = m_Target.position - transform.position;
        m_motor.MovementDirection = direction;
	}
}
