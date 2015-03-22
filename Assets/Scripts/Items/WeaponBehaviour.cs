﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PickableItem))]
public class WeaponBehaviour : MonoBehaviour
{
    private PickableItem m_item;
    public PickableItem Item
    {
        get
        {
            if (m_item == null)
            {
                m_item = GetComponent<PickableItem>();
                if (m_item == null) Debug.LogWarning("missing item component");
            }
            return m_item;
        }
        protected set
        {
            m_item = value;
        }
    }

    public string AnimationTrigger;

    public void Fire(GameObject owner, Vector3 target)
    {
        var animator = owner.GetComponentInChildren<Animator>();
        animator.SetTrigger(AnimationTrigger);


        var spell = GetComponent<SpellBehaviour>();
        if (spell != null)
        {
            spell.m_Caster = owner.GetComponent<CasterBehaviour>(); //костыль?
            spell.BeginCast(target);
        }
    }
}