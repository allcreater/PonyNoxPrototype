using UnityEngine;
using System.Collections.Generic;

public abstract class SpellBehaviour : MonoBehaviour
{
	[Range(0.0f, 100.0f)]
	public float m_CooldownTime = 1.0f;
    public float m_ManaCost = 0.0f;

    public string m_SpellName = "";

    public CasterBehaviour m_Caster;

    public Sprite m_Icon;

	public bool IsAvailable
	{
		get { return Time.timeSinceLevelLoad > (m_lastCastTime + m_CooldownTime); }
	}

	private float m_lastCastTime = 0.0f;

	public void BeginCast(Vector3 target)
	{
		Debug.Log(m_SpellName + " fired!");

		m_lastCastTime = Time.timeSinceLevelLoad;

		BeginCastImpl(target);
	}


    public abstract void BeginCastImpl(Vector3 target);
}


public class CasterBehaviour : MonoBehaviour
{
	public GameObject m_spellContainer;

    public Segment m_ManaPoints;

	public SpellBehaviour[] m_AvailableSpells;

	protected void UpdateSpellsList()
	{
		m_AvailableSpells = m_spellContainer.GetComponents<SpellBehaviour>();
	}

	public void Cast(uint activeSpell, Vector3 target)
	{
		if (m_AvailableSpells == null || activeSpell >= m_AvailableSpells.Length)
			return;
		
		var spell = m_AvailableSpells[activeSpell];
        if (spell.IsAvailable && m_ManaPoints.currentValue >= spell.m_ManaCost)
        {
            m_ManaPoints.ChangeValue(-spell.m_ManaCost);
            spell.m_Caster = this;
            spell.BeginCast(target);
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
