﻿using UnityEngine;
using System.Collections;

public class EffectOnCasterSpell : SpellBehaviour
{
    public GameObject m_Effect;
    public bool m_IsAsynchronious = false;

    private GameObject m_EffectInstance;

    public override bool IsInProgress
    {
        get
        {
            return m_IsAsynchronious && m_EffectInstance;
        }
    }

    public override void BeginCastImpl(Vector3 target)
	{
        m_EffectInstance = EffectBehaviour.InstantiateEffect(m_Effect, m_Caster.gameObject);
	}
}
