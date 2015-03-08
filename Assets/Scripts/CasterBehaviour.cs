using UnityEngine;
using System.Collections.Generic;

public abstract class SpellBehaviour : MonoBehaviour
{
	[Range(0.0f, 100.0f)]
	public float m_CooldownTime = 1.0f;
    public float m_ManaCost = 0.0f;

	public bool IsAvailable
	{
		get { return Time.timeSinceLevelLoad > (m_lastCastTime + m_CooldownTime); }
	}

	private float m_lastCastTime = 0.0f;

	public void BeginCast()
	{
		Debug.Log("Fire!");
		m_lastCastTime = Time.timeSinceLevelLoad;
		BeginCastImpl();
	}


	public abstract void BeginCastImpl();
}


public class CasterBehaviour : MonoBehaviour
{
	public GameObject m_spellContainer;

    public Segment m_ManaPoints;

	protected SpellBehaviour[] m_AvailableSpells;

	protected void UpdateSpellsList()
	{
		m_AvailableSpells = m_spellContainer.GetComponents<SpellBehaviour>();
	}

	public void Cast(uint activeSpell)
	{
		if (m_AvailableSpells == null || activeSpell >= m_AvailableSpells.Length)
			return;
		
		var spell = m_AvailableSpells[activeSpell];
        if (spell.IsAvailable && m_ManaPoints.currentValue >= spell.m_ManaCost)
        {
            m_ManaPoints.ChangeValue(-spell.m_ManaCost);
            spell.BeginCast();
        }
	}

	void Start ()
	{
		UpdateSpellsList();
	}
	
	void Update ()
	{
	
	}
}
