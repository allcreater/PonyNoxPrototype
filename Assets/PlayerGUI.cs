using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[RequireComponent(typeof(LivingCreatureBehaviour))]
[RequireComponent(typeof(CasterBehaviour))]
public class PlayerGUI : MonoBehaviour
{
	public Text m_HpIndicator;
	public Text m_MpIndicator;
    public GridLayoutGroup m_SpellsPanel;

	private LivingCreatureBehaviour m_livingCreatureComponent;
    private CasterBehaviour m_casterBehaviourComponent;

	void Start ()
	{
		m_livingCreatureComponent = GetComponent<LivingCreatureBehaviour>();
        m_casterBehaviourComponent = GetComponent<CasterBehaviour>();
	}
	
	void Update ()
	{
		m_HpIndicator.text = string.Format("{0}", m_livingCreatureComponent.m_HitPoints);
        m_MpIndicator.text = string.Format("{0}", m_casterBehaviourComponent.m_ManaPoints);
	}
}
