using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.Events;

public enum PickableItemState
{
    Idle,
    Inventory,
    Equipped
}

[DisallowMultipleComponent]
public class PickableItem : MonoBehaviour
{
    public Sprite m_InventoryIcon;

    public string m_ItemName;
    public string m_Description;
    public float m_Mass;

    public UnityEvent m_ItemPickedEvent;
    public UnityEvent m_ItemReleasedEvent;
    
    public PickableItemState ItemState
    {
        get { return m_itemState; }
        set
        {
            if (m_itemState == value)
                return;
            
            switch (value)
            {
                case PickableItemState.Idle:
                    {
                        SwitchToIdleState();
                    } break;
                case PickableItemState.Inventory:
                    {
                        SwitchToInventoryState();
                    } break;
                case PickableItemState.Equipped:
                    {
                        SwitchToEquippedState();
                    } break;
            }

            m_itemState = value;
        }
    }
    private PickableItemState m_itemState = PickableItemState.Idle;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

    private void SwitchToIdleState()
    {
        gameObject.SetActive(true);
    }
    private void SwitchToInventoryState()
    {
        gameObject.SetActive(false);
    }
    private void SwitchToEquippedState()
    {
        gameObject.SetActive(true);
    }
}
