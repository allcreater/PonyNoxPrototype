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
	}

	// Update is called once per frame
	void Update ()
	{
        var target = m_targetSelector.Targets.Where(x => x.m_Team != "Enemy").FirstOrDefault();
        if (!target)
            return;

        var dir = target.transform.position - transform.position;
        var targetDirection = new Ray(transform.position, dir.normalized);

        m_navigator.Target = target.transform.position;

        m_motor.MovementDirection = m_navigator.DesiredDirection;
        Debug.DrawRay(targetDirection.origin, targetDirection.direction * dir.magnitude, Color.green);


		m_caster.Target = new TargetInfo (target.transform.position, Vector3.up);
		if (dir.magnitude <= 2.0f) {
			m_caster.Cast (0);
			//Debug.Log("cast");
		}
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