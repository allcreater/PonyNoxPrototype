using UnityEngine;
using System.Collections.Generic;

[DisallowMultipleComponent]
public class InventoryBehaviour : MonoBehaviour
{
    public float m_MaximumMass = 20.0f;
    public float CurrentMass { get; private set; }

    public IEnumerable<PickableItem> Items
    {
        get { return m_items; }
    }

    private bool m_inventoryDirtyFlag = true;
    private List<PickableItem> m_items = new List<PickableItem>();

    public void RequestUpdate()
    {
        m_inventoryDirtyFlag = true;
    }

    public bool PickUpItem(PickableItem item)
    {
        if (m_items.IndexOf(item) >= 0)
            throw new System.InvalidOperationException("нельзя подобрать уже подобранный объект");

        //с этой фичей отбирать предметы не получится
        /*
        if (item.ItemState != PickableItemState.Idle)
            return false;
        */
        item.ItemState = PickableItemState.Inventory;
        item.transform.SetParent(transform);

        RequestUpdate();
        return true;
    }

    public void ReleaseItem(PickableItem item)
    {
        if (m_items.IndexOf(item) < 0)
            throw new System.InvalidOperationException("предмет не в инвентаре");

        item.ItemState = PickableItemState.Idle;
        item.transform.SetParent(null);

        RequestUpdate();
    }

	void Start ()
	{
	}
	
	void Update ()
	{
        if (m_inventoryDirtyFlag)
        {
            UpdateInventoryState();
            m_inventoryDirtyFlag = false;
        }
	}

    private void UpdateInventoryState()
    {
        m_items.Clear();
        CurrentMass = 0.0f;

        var items = GetComponentsInChildren<PickableItem>(true);
        foreach (var item in items)
        {
            CurrentMass += item.m_Mass;

            m_items.Add(item);
        }
    }
}
