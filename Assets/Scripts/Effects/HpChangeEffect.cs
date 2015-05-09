using UnityEngine;
using System.Collections.Generic;

public class HpChangeEffect : EffectBehaviour
{
    public float HpChangePerSecond = 1.0f;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	protected override void EffectUpdate ()
	{
        GetTargetsComponent<LivingCreatureBehaviour>().m_HitPoints.ChangeValue(HpChangePerSecond * Time.deltaTime);
	}
}
