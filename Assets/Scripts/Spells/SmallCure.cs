using UnityEngine;
using System.Collections;

public class SmallCure : SpellBehaviour
{
	void Start ()
	{
	
	}
	
	void Update ()
	{
	
	}

	public override void BeginCastImpl()
	{
		var parent = gameObject.transform.parent.GetComponent<LivingCreatureBehaviour>();
		parent.AddEffect(new CureMagicalEffect() { CureForce = 5.0f });
	}
}
