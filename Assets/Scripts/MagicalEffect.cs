using UnityEngine;

public abstract class MagicalEffect
{
	public float Duration = 1.0f;

	private float m_elapsedTime = 0.0f;
	public float ElapsedTime
	{
		get { return m_elapsedTime; }
	}

	public bool IsActive
	{
		get { return ElapsedTime <= Duration; }
	}

	public void UpdateEffect(LivingCreatureBehaviour target, float dt)
	{
		float currentTime = m_elapsedTime;
		m_elapsedTime += dt;

		if (currentTime == 0.0f)
		{
			BeginEffectImpl(target);
			return;
		}
		else if (m_elapsedTime > Duration)
		{
			FinalizeEffectImpl(target);
			return;
		}

		UpdateEffectImpl(target, dt);
		
	}

	protected abstract void BeginEffectImpl(LivingCreatureBehaviour target);
	protected abstract void UpdateEffectImpl(LivingCreatureBehaviour target, float dt);
	protected abstract void FinalizeEffectImpl(LivingCreatureBehaviour target);
}

public class MagicalEffectFactory
{
	public static MagicalEffect CreateEffect()
	{
		return new PoisonMagicalEffect() { Duration = 10.0f };
	}
}

public class PoisonMagicalEffect : MagicalEffect
{
	public float PoisonForce = 1.0f;
	private GameObject specialEffect = null;


	protected override void BeginEffectImpl(LivingCreatureBehaviour target)
	{
        var template = TemplateCollection.Instance.FindTemplate("PoisonPlayerEffect");
        specialEffect = GameObject.Instantiate(template, target.transform.position, target.transform.rotation) as GameObject;
		specialEffect.transform.parent = target.transform;

		//Debug.Log("Poison!");
	}

	protected override void FinalizeEffectImpl(LivingCreatureBehaviour target)
	{
		GameObject.Destroy(specialEffect, specialEffect.GetComponent<ParticleSystem>().startLifetime);
		//Debug.Log("~Poison");
	}

	protected override void UpdateEffectImpl(LivingCreatureBehaviour target, float dt)
	{
		specialEffect.GetComponent<ParticleSystem>().Emit(10);
		target.m_HitPoints.ChangeValue(-PoisonForce * dt);
		//Debug.Log("Poison updated: " + ElapsedTime);
	}
}

public class CureMagicalEffect : MagicalEffect
{
	public float CureForce = 5.0f;
	private GameObject specialEffect = null;


	protected override void BeginEffectImpl(LivingCreatureBehaviour target)
	{
        var template = TemplateCollection.Instance.FindTemplate("CurePlayerEffect");
        specialEffect = GameObject.Instantiate(template) as GameObject;//, target.transform.position, target.transform.rotation
		specialEffect.transform.parent = target.transform;
        specialEffect.transform.localPosition = template.transform.position;
		//Debug.Log("Poison!");
	}

	protected override void FinalizeEffectImpl(LivingCreatureBehaviour target)
	{
		GameObject.Destroy(specialEffect, specialEffect.GetComponent<ParticleSystem>().startLifetime);
		//Debug.Log("~Poison");
	}

	protected override void UpdateEffectImpl(LivingCreatureBehaviour target, float dt)
	{
		specialEffect.GetComponent<ParticleSystem>().Emit(3);
		target.m_HitPoints.ChangeValue(CureForce * dt);
		//Debug.Log("Poison updated: " + ElapsedTime);
	}
}