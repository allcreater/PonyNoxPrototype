using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public interface IUsableItem
{
    void OnItemUsed(ItemUsedEventArgs args);
}

public class IF_StatsChanger : MonoBehaviour, IUsableItem
{
    //TODO
    public float m_deltaHP;
    public float m_deltaMP;

    public void OnItemUsed(ItemUsedEventArgs args)
    {
        var lc = args.Owner.GetComponent<LivingCreatureBehaviour>();
        if (lc != null)
        {
            lc.m_HitPoints.ChangeValue(m_deltaHP);
        }
        
        {
            var attribute = AttributeBehaviour.GetAttributeComponent(args.Owner, "ManaPoints");
            if (attribute)
                attribute.m_Amount.ChangeValue(m_deltaMP);
        }


        args.Item.Dispose();
    }
}
