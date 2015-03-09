﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public struct Segment
{
	public float currentValue;
	public float maximumValue;

	//отношение текущего значения к максимальному
	public float Rate
	{
		get { return currentValue / maximumValue; }
	}

	//Значение остаётся в диапазоне [0, maximumValue]
	public void ChangeValue(float value)
	{
		currentValue += value;
		currentValue = Mathf.Clamp(currentValue, 0.0f, maximumValue);
	}

	public override string ToString()
	{
		return string.Format("{0:0.0}/{1:0.0}", currentValue, maximumValue);
	}
}

public class LivingCreatureBehaviour : MonoBehaviour
{
	public Segment m_HitPoints;

    public bool IsAlive
    {
        get { return m_HitPoints.currentValue > 0.0f; }
    }

    public Animator m_Animator;

	private List<MagicalEffect> effectsList = new List<MagicalEffect>();

	public void AddEffect(MagicalEffect effect)
	{
		effectsList.Add(effect);
	}
	public void UpdateEffects(float dt)
	{
		foreach (var effect in effectsList)
			effect.UpdateEffect(this, dt);
		effectsList.RemoveAll((a) => !a.IsActive);
	}

	void Start ()
	{
		//AddEffect(MagicalEffectFactory.CreateEffect());
	}

	void Update ()
	{
		UpdateEffects(Time.deltaTime);

        if (m_Animator != null)
        {
            m_Animator.SetBool("IsDead", !IsAlive);
        }
	}
}