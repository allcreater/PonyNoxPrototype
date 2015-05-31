using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TargetInfo
{
    public Vector3 point { get; private set; }
    public Vector3 normal { get; private set; }

    public TargetInfo(Vector3 _point, Vector3 _normal)
    {
        point = _point;
        normal = _normal;
    }
}

public abstract class SpellBehaviour : MonoBehaviour
{
    [Range(0.0f, 100.0f)]
    public float m_CooldownTime = 1.0f;
    public float m_ManaCost = 0.0f;

    public string m_SpellName = "";

    [HideInInspector]
    public CasterBehaviour m_Caster;

    public Sprite m_Icon;

    public bool m_IsActive = true;

    private float m_lastCastTime = 0.0f;

    public bool IsAvailable
    {
        get { return Time.timeSinceLevelLoad > (m_lastCastTime + m_CooldownTime); }
    }

    public abstract bool IsInProgress { get; }

    public Animator CasterAnimator { get; private set; }


    public void BeginCast(CasterBehaviour caster)
    {
        Debug.Log(m_SpellName + " fired!");

        m_lastCastTime = Time.timeSinceLevelLoad;
        m_Caster = caster;
        CasterAnimator = m_Caster.GetComponentInChildren<Animator>();

        BeginCastImpl();
    }


    public abstract void BeginCastImpl();
}