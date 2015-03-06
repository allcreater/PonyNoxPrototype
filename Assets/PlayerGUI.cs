using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[RequireComponent(typeof(LivingCreatureBehaviour))]
public class PlayerGUI : MonoBehaviour
{
	public Text m_HpIndicator;
	public Text m_MpIndicator;


	private LivingCreatureBehaviour m_livingCreatureComponent;
	// Use this for initialization
	void Start ()
	{
		m_livingCreatureComponent = GetComponent<LivingCreatureBehaviour>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		m_HpIndicator.text = string.Format("Health {0}", m_livingCreatureComponent.hitPoints);

	}
}
