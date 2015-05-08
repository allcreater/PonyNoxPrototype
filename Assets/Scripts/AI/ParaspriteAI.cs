using UnityEngine;
using System.Linq;
using System.Collections.Generic;

[RequireComponent(typeof(FlyingCreatureMotor))]
public class ParaspriteAI : MonoBehaviour
{
    private FlyingCreatureMotor m_motor;
    private NearestTargetSelector m_targetSelector;

	void Start ()
	{
        m_targetSelector = GetComponent<NearestTargetSelector>();
        m_motor = GetComponent<FlyingCreatureMotor>();
	}
	
	// Update is called once per frame
	void Update ()
	{
        var target = m_targetSelector.Targets.Where(x => x.m_Team != "Enemy").FirstOrDefault();
        if (!target)
            return;

        var direction = target.transform.position - transform.position;
        m_motor.MovementDirection = direction;
	}
}
