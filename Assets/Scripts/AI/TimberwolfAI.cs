using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(GroundCreatureMovementMotor))]
public class TimberwolfAI : MonoBehaviour
{
    public Transform m_Target;
    public float m_TargetDetectDistance;


    private GroundCreatureMovementMotor m_Motor;
    private void RetracePath()
    {

    }


	// Use this for initialization
	void Start ()
	{
        m_Motor = GetComponent<GroundCreatureMovementMotor>();
	}
	
	// Update is called once per frame
	void Update ()
	{
        if (m_Target == null)
            return;

        var dir = m_Target.position - transform.position;
        var targetDirection = new Ray(transform.position, dir.normalized);
        /*
	    if (Physics.Raycast(targetDirection, m_TargetDetectDistance))
        {
            NavMeshHit hit;
            NavMesh.SamplePosition(m_Target.position, out hit, 1.0f, NavMesh.AllAreas);
            hit.
        }
        */



        if (dir.sqrMagnitude < m_TargetDetectDistance * m_TargetDetectDistance)
        {
            m_Motor.MovementDirection = dir;
            Debug.DrawRay(targetDirection.origin, targetDirection.direction * m_TargetDetectDistance, Color.green);
        }
        else
        {
            Debug.DrawRay(targetDirection.origin, targetDirection.direction * m_TargetDetectDistance, Color.red);
        }
	}
}
