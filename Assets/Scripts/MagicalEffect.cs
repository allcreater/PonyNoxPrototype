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
		float currentTime = m_elapsedTime += dt;

		if (currentTime == 0.0f)
		{
			BeginEffectImpl(target);
			return;
		}
		else if (currentTime > Duration)
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
		return new PoisonMagicalEffect();
	}
}

public class PoisonMagicalEffect : MagicalEffect
{
	public float PoisonForce = 1.0f;

	protected override void BeginEffectImpl(LivingCreatureBehaviour target)
	{
		Debug.Log("Poison!");
	}

	protected override void FinalizeEffectImpl(LivingCreatureBehaviour target)
	{
		Debug.Log("~Poison");
	}

	protected override void UpdateEffectImpl(LivingCreatureBehaviour target, float dt)
	{
		target.hitPoints.ChangeValue(-PoisonForce * dt);
		Debug.Log("Poison updated: " + ElapsedTime);
	}
}