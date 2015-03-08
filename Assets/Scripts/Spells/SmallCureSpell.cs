using UnityEngine;
using System.Collections;

public class SmallCureSpell : SpellBehaviour
{
    void Start()
    {
        m_ManaCost = 3.0f;
    }

	public override void BeginCastImpl()
	{
		var parent = gameObject.transform.parent.GetComponent<LivingCreatureBehaviour>();
		parent.AddEffect(new CureMagicalEffect() { CureForce = 5.0f });
	}
}
