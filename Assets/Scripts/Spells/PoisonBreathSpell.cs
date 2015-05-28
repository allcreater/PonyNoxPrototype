using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoisonBreathSpell : SpellBehaviour
{
	public GameObject m_Prefab;
	public bool m_IsAsynchronious = false;
	public string m_AnimationTriggerName;
	private GameObject m_EffectInstance;
	
	public override bool IsInProgress
	{
		get
		{
			return !m_IsAsynchronious && m_EffectInstance;
		}
	}
	
	public override void BeginCastImpl()
	{
		CasterAnimator.SetTrigger(m_AnimationTriggerName);

		var origin = m_Caster.transform.position + m_Caster.transform.TransformDirection(new Vector3(0.0f, 1.0f, 1.0f));
		var direction = (m_Caster.Target.point + Vector3.up*0.5f - origin).normalized;


		m_EffectInstance = GameObject.Instantiate(m_Prefab, origin, new Quaternion()) as GameObject;
		m_EffectInstance.GetComponent<Rigidbody> ().velocity = m_Caster.GetComponent<Rigidbody> ().velocity + direction * 50.0f;
	}
}
