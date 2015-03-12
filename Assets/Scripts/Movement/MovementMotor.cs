using UnityEngine;
using System.Collections.Generic;

public class MovementMotor : MonoBehaviour
{
    public float m_MovementImpulse = 10.0f;

    //public Quaternion PreferableOrientation;

    public Vector3 MovementImpulse
    {
        get { return m_movementDirection * m_MovementImpulse; }
    }

    public Vector3 MovementDirection
    {
        get { return m_movementDirection; }
        set
        {
            float magnitude = value.magnitude;
            if (magnitude > 1.0f)
                m_movementDirection = value / magnitude;
            else
                m_movementDirection = value;
        }
    }
    private Vector3 m_movementDirection;
}

public abstract class LivingCreatureMotor : MovementMotor
{
    protected LivingCreatureBehaviour LivingCreature
    {
        get
        {
            if (m_livingCreature == null)
                m_livingCreature = GetComponent<LivingCreatureBehaviour>();

            return m_livingCreature;
        }
    }
    private LivingCreatureBehaviour m_livingCreature;

    private bool m_stillAlive = true;

    void FixedUpdate()
    {
        if (LivingCreature == null)
            FixedUpdateImpl();
        else
            if (LivingCreature.IsAlive)
            {
                FixedUpdateImpl();
                if (m_stillAlive == false)
                    OnRevive();

                m_stillAlive = true;
            }
            else if (m_stillAlive == true)
            {
                OnDeath();
                m_stillAlive = false;
            }
            
    }

    protected abstract void FixedUpdateImpl();
    protected virtual void OnDeath() { }
    protected virtual void OnRevive() { }
}