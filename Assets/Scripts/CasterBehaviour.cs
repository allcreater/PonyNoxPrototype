using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class SpellInfo
{
    public Sprite sprite {get; set; }
    public string name {get; set; }
}

public class CasterBehaviour : MonoBehaviour
{
	public GameObject m_SpellContainer;

    public IList<SpellInfo> Spells { get; private set; }
    public TargetInfo Target { get; set; }
    public bool IsBusy { get; protected set; }

    protected SpellBehaviour[] m_availableSpells;

	protected void UpdateSpellsList()
	{
        m_availableSpells = m_SpellContainer.GetComponentsInChildren<SpellBehaviour>().Where(x => x.m_Icon).ToArray();

        Spells = (from x in m_availableSpells select new SpellInfo() { name = x.m_SpellName, sprite = x.m_Icon }).ToList();
	}

    protected IEnumerator SpellWaitingProcess(SpellBehaviour activeSpell)
    {
        IsBusy = true;

        while (activeSpell.IsInProgress)
            yield return new WaitForEndOfFrame();

        IsBusy = false;
    }

    /*
    public bool Cast(uint activeSpell)
	{
        if (IsBusy)
            return false;

        if (m_availableSpells == null || activeSpell >= m_availableSpells.Length)
			return false;

        var spell = m_availableSpells[activeSpell];
        if (spell.IsAvailable)
            spell.BeginCast(this);

        StartCoroutine(SpellWaitingProcess(spell));
        
        return true;
	}
    */

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
