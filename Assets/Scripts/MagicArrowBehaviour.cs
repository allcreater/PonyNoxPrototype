using UnityEngine;
using System.Collections.Generic;

public class MagicArrowBehaviour : ThrowableSpellBehaviour
{
    public Transform m_Target;

    public float m_Speed;
    public float m_Acceleration;

    public float m_HomingDelay = 0.25f;
    public float m_Lifetime = 3.0f;

    public int m_BounceLayerMask = 1 << 0;

    private float m_startTime;

    // Use this for initialization
    void Start()
    {
        m_startTime = Time.time;
    }

    void FixedUpdate()
    {
        var dir = (m_Target != null)? m_Target.position - transform.position : m_Velocity;
        if (Time.time <= m_startTime + m_HomingDelay)
            dir = m_Velocity;

        m_Velocity += (dir.normalized * m_Acceleration * Time.fixedDeltaTime);
        m_Velocity = Vector3.ClampMagnitude(m_Velocity, m_Speed);

        transform.position = transform.position + m_Velocity * Time.fixedDeltaTime;

        float dSpeed = m_Velocity.magnitude * Time.fixedDeltaTime;

        
        var ray = new Ray(transform.position, m_Velocity.normalized);

        //отскакиваем от стен
        RaycastHit hit;
        if (Physics.SphereCast(ray, m_ColliderRadius * 2.0f, out hit, dSpeed, m_BounceLayerMask))
        {
            m_Velocity = Vector3.Reflect(m_Velocity, hit.normal);
            return;
        }

        //файрболл задел кого-то
        if (Physics.SphereCast(ray, m_ColliderRadius, dSpeed, m_LayerMask))
        {
            Vector3 effectLocation = transform.position;
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, dSpeed * 2.0f))
                effectLocation += (hitInfo.normal * 0.5f);
            Explode(effectLocation);
        }

        if (Time.time >= m_startTime + m_Lifetime)
            Explode(transform.position);

    }

}
