using UnityEngine;
using System.Collections;

public class SeekableTarget : MonoBehaviour
{
    public string m_Team;

    private LivingCreatureBehaviour m_lcb;

    public bool IsAlive
    {
        get
        {
            return (m_lcb && m_lcb.IsAlive);
        }
    }

    void Start()
    {
        m_lcb = GetComponentInChildren<LivingCreatureBehaviour>();
    }
}
