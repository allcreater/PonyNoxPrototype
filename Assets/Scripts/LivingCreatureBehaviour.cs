using UnityEngine;
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