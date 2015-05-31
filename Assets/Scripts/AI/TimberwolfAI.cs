using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(GroundCreatureMovementMotor))]
public class TimberwolfAI : MonoBehaviour
{
    private NearestTargetSelector m_targetSelector;
    private GroundCreatureMovementMotor m_motor;
    private NavMeshNavigator m_navigator;
	private CasterBehaviour m_caster;

	// Use this for initialization
	void Start ()
	{
        m_motor = GetComponent<GroundCreatureMovementMotor>();
        m_navigator = GetComponent<NavMeshNavigator>();
        m_targetSelector = GetComponent<NearestTargetSelector>();
		m_caster = GetComponent<CasterBehaviour> ();

        m_targetSelector.TargetFilter = x => x.m_Team != "Enemy";
	}

	// Update is called once per frame
	void Update ()
	{
        var target = m_targetSelector.Target;
        if (!target)
            return;

        var dir = target.transform.position - transform.position;
        var targetDirection = new Ray(transform.position, dir.normalized);

        m_navigator.Target = target.transform.position;
		m_navigator.m_StopRadius = 2.0f + GetComponent<Rigidbody> ().velocity.magnitude * 0.1f; //TODO - govnocode!

        m_motor.MovementDirection = m_navigator.DesiredDirection;
		m_motor.RotationFactor = m_navigator.DesiredDirection.magnitude;
        Debug.DrawRay(targetDirection.origin, targetDirection.direction * dir.magnitude, Color.green);


		m_caster.Target = new TargetInfo (target.transform.position, Vector3.up);

		var distance = dir.magnitude;
		if (distance > 3.0f && distance <= 10.0f)
			m_caster.Cast (0);
		else if (distance <= 3.0f)
			m_caster.Cast (1);
	}
}

abstract class AiState
{
    public abstract void Update();
};

class StandAiState : AiState
{
    public override void Update()
    {
        throw new System.NotImplementedException();
    }
}