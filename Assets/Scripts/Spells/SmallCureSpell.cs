using UnityEngine;
using System.Collections;

public class SmallCureSpell : SpellBehaviour
{
    public override bool IsInProgress
    {
        get
        {
            return false;
        }
    }

    public override void BeginCastImpl(Vector3 target)
	{
		var parent = gameObject.transform.parent.GetComponent<LivingCreatureBehaviour>();
		parent.AddEffect(new CureMagicalEffect() { CureForce = 5.0f });
	}
}
