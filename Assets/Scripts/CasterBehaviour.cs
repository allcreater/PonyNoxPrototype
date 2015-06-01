﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;



public class CasterBehaviour : MonoBehaviour
{
	public GameObject m_spellContainer;
	public SpellBehaviour[] m_AvailableSpells;

    public TargetInfo Target { get; set; }

    public bool IsBusy { get; protected set; }

	protected void UpdateSpellsList()
	{
		m_AvailableSpells = m_spellContainer.GetComponentsInChildren<SpellBehaviour>().Where(x => x.m_Icon).ToArray();
	}

    private IEnumerator SpellWaitingProcess(SpellBehaviour activeSpell)
    {
        IsBusy = true;

        while (activeSpell.IsInProgress)
            yield return new WaitForEndOfFrame();

        IsBusy = false;
    }

    public bool Cast(uint activeSpell)
	{
        if (IsBusy)
            return false;

		if (m_AvailableSpells == null || activeSpell >= m_AvailableSpells.Length)
			return false;

        var manaAttribute = AttributeBehaviour.GetAttributeComponent(transform.FindChild("Stats").gameObject, "ManaPoints");

		var spell = m_AvailableSpells[activeSpell];
        if (spell.IsAvailable && (!manaAttribute || manaAttribute.m_Amount.currentValue >= spell.m_ManaCost))
        {
            spell.BeginCast(this);
            if (manaAttribute)
                manaAttribute.m_Amount.ChangeValue(-spell.m_ManaCost);
        }
        StartCoroutine(SpellWaitingProcess(spell));
        
        return true;
	}

	void Start ()
	{
        IsBusy = false;
		UpdateSpellsList();
	}
	
	void Update ()
	{
        //UpdateSpellsList();
	}
}
