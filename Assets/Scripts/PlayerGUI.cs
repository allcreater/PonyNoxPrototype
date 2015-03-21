using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

class GameObjectPool
{
    public GameObject m_Template;
    private List<GameObject> m_freeObjects = new List<GameObject>();

    public GameObject GetObject()
    {
        if (m_freeObjects.Count > 0)
        {
            int index = m_freeObjects.Count - 1;

            var result = m_freeObjects[index];
            m_freeObjects.RemoveAt(index);

            return result;
        }

        return GameObject.Instantiate(m_Template);
    }

    public void ReleaseToPool(GameObject obj)
    {
        m_freeObjects.Add(obj);
    }
}

[RequireComponent(typeof(LivingCreatureBehaviour))]
[RequireComponent(typeof(CasterBehaviour))]
public class PlayerGUI : MonoBehaviour
{
	public Text m_HpIndicator;
	public Text m_MpIndicator;
    
    public GridLayoutGroup m_SpellsPanel;
    public GameObject m_SpellIconTemplate;

    public GridLayoutGroup m_InventoryPanel;
    public GameObject m_InventoryItemIconTemplate;

    public InventoryBehaviour m_Inventory;

    public Text m_activeItemDescription;
    public Image m_activeItemIcon;

	private LivingCreatureBehaviour m_livingCreatureComponent;
    private CasterBehaviour m_casterBehaviourComponent;

    private List<GameObject> m_iconsPool = new List<GameObject>();
    private PickableItem m_activeItem;

    public void OnEquipButton()
    {
        if (m_activeItem != null)
        {
            m_Inventory.ArmWeapon(m_activeItem);
        }
    }
    public void OnUseButton()
    {
        if (m_activeItem != null)
        {
            m_Inventory.UseItem(m_activeItem);
        }
    }
    public void OnDropButton()
    {
        if (m_activeItem != null)
        {
            m_Inventory.ReleaseItem(m_activeItem);
            m_activeItem = null;
        }
    }

    private void PreparePool(int count)
    {
        for (int i = 0; i < m_iconsPool.Count; ++i)
            m_iconsPool[i].SetActive(i < count); //активируем нужные, ненужные выключаем

        //добавляем тех, кого не хватает
        for (int i = m_iconsPool.Count; i < count; ++i)
        {
            var obj = GameObject.Instantiate(m_InventoryItemIconTemplate);
            m_iconsPool.Add(obj);
        }
    }


	void Start ()
	{
		m_livingCreatureComponent = GetComponent<LivingCreatureBehaviour>();
        m_casterBehaviourComponent = GetComponent<CasterBehaviour>();

        //TODO
        foreach (var spell in m_casterBehaviourComponent.m_AvailableSpells)
        {
            var icon = GameObject.Instantiate(m_SpellIconTemplate);
            icon.transform.SetParent(m_SpellsPanel.transform, false);
            icon.GetComponentInChildren<Text>().text = spell.m_SpellName;
            icon.GetComponent<Image>().sprite = spell.m_Icon;
        }
	}
	
	void Update ()
	{
		m_HpIndicator.text = string.Format("{0}", m_livingCreatureComponent.m_HitPoints);
        m_MpIndicator.text = string.Format("{0}", m_casterBehaviourComponent.m_ManaPoints);

        var itemList = m_Inventory.Items.ToList();
        PreparePool(itemList.Count);
        for (int i = 0; i < itemList.Count; ++i)
        {
            var item = itemList[i];
            var icon = m_iconsPool[i];

            icon.transform.SetParent(m_InventoryPanel.transform, false);
            icon.GetComponentInChildren<Text>().text = item.m_ItemName;
            icon.GetComponent<Image>().sprite = item.m_InventoryIcon;
            icon.GetComponent<Button>().onClick.AddListener(() => m_activeItem = item);
        }

        if (m_activeItem != null)
        {
            m_activeItemIcon.sprite = m_activeItem.m_InventoryIcon;
            m_activeItemDescription.text = m_activeItem.m_Description;
        }
	}
}
