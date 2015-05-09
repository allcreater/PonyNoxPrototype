using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class NearestTargetSelector : MonoBehaviour
{
    public float m_UpdateInterval = 1.0f;
    public float m_VisionRadius = 20.0f;

    public IList<SeekableTarget> Targets { get; private set; }
    public IEnumerable<SeekableTarget> this [string team]
    {
        get
        {
            return Targets.Where(x => x.m_Team == team);
        }
    }

    private float m_lastUpdateTime;

    void Start()
    {
        m_lastUpdateTime = Time.time;
        Targets = new List<SeekableTarget>();
    }

    void Update()
    {
        if (Time.time > m_lastUpdateTime + m_UpdateInterval)
        {
            float sqrVisionRadius = m_VisionRadius * m_VisionRadius;
            var targets = GameObject.FindObjectsOfType<SeekableTarget>();
            Targets = (from x in targets where (transform.position - x.transform.position).sqrMagnitude < sqrVisionRadius orderby (transform.position - x.transform.position).sqrMagnitude select x).ToList();

            m_lastUpdateTime = Time.time;
        }
    }
}
