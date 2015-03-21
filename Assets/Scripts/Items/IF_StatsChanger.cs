using UnityEngine;
using System.Collections.Generic;

public interface IUsableItem
{
    void OnItemUsed(ItemUsedEventArgs args);
}

public class IF_StatsChanger : MonoBehaviour, IUsableItem
{
    public float m_deltaHP;
    public float m_deltaMP;

    public void OnItemUsed(ItemUsedEventArgs args)
    {
        var lc = args.Owner.GetComponent<LivingCreatureBehaviour>();
        if (lc != null)
        {
            lc.m_HitPoints.ChangeValue(m_deltaHP);
        }

        var caster = args.Owner.GetComponent<CasterBehaviour>();
        if (caster != null)
        {
            caster.m_ManaPoints.ChangeValue(m_deltaMP);
        }
        
        args.Item.Dispose();
    }
}
