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

public class ItemUsedEventArgs
{
    public PickableItem Item;
    public GameObject Owner;

    public ItemUsedEventArgs(PickableItem item, GameObject owner)
    {
        Item = item;
        Owner = owner;
    }
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

    public void UseItem(GameObject owner)
    {
        var args = new ItemUsedEventArgs(this, owner);

        //BroadcastMessage("OnItemUsed", new ItemUsedEventArgs(this, owner));
        var itemLogics = GetComponents<IUsableItem>();
        foreach (var il in itemLogics)
            il.OnItemUsed(args);
    }

    //одноразовый предмет использован и всё такое
    public void Dispose()
    {
        //TODO: если предметов много, уменьшать счётчик, и удалять только при нулевом
        GameObject.Destroy(this.gameObject);
    }

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

        var rigidBody = GetComponent<Rigidbody>();
        rigidBody.isKinematic = false;
    }
    private void SwitchToInventoryState()
    {
        gameObject.SetActive(false);
    }
    private void SwitchToEquippedState()
    {
        gameObject.SetActive(true);

        var rigidBody = GetComponent<Rigidbody>();
        rigidBody.isKinematic = true;

        var colliderList = GetComponentsInChildren<Collider>();
        foreach (var collider in colliderList)
            collider.enabled = false;
    }
}
