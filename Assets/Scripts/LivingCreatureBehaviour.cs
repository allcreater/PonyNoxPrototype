using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class LivingCreatureBehaviour : MonoBehaviour
{
	public Segment m_HitPoints;

    public bool IsAlive
    {
        get { return m_HitPoints.currentValue > 0.0f; }
    }

    public Animator m_Animator;

	void Start ()
	{
		//AddEffect(MagicalEffectFactory.CreateEffect());
	}

	void Update ()
	{
        if (m_Animator != null)
        {
            m_Animator.SetBool("IsDead", !IsAlive);
        }
	}
}