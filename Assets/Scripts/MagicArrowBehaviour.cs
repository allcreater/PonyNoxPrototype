using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class MagicArrowBehaviour : ThrowableSpellBehaviour
{
    public string m_TargetTeam;

    public float m_Speed;
    public float m_Acceleration;

    public float m_HomingDelay = 0.25f;
    public float m_Lifetime = 3.0f;

    public int m_BounceLayerMask = 1 << 0;

    private float m_startTime;
    private SeekableTarget m_target;
    private NearestTargetSelector m_targetSelector;

    void Start()
    {
        m_targetSelector = GetComponent<NearestTargetSelector>();
        m_startTime = Time.time;
    }

    void Update()
    {
        m_target = m_targetSelector.Targets.FirstOrDefault(x => x.m_Team == m_TargetTeam && x.IsAlive);
    }

    void FixedUpdate()
    {
        var dir = (m_target != null)? m_target.transform.position - transform.position : m_Velocity;
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
