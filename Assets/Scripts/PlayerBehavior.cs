using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GroundCreatureMovementMotor))]
[RequireComponent(typeof(CasterBehaviour))]
public class PlayerBehavior : MonoBehaviour
{
    public Camera m_playerCamera;
    public InventoryBehaviour m_InventoryBehaviour;

	private CasterBehaviour m_caster;
    private Collider m_collider;
    private GroundCreatureMovementMotor m_motor;

    private Vector3 m_targetPosition;
	void Start ()
    {
		m_caster = GetComponent<CasterBehaviour>();
        m_collider = GetComponent<Collider>();
        m_motor = GetComponent<GroundCreatureMovementMotor>();
	}

    void TestMousePointer()
    {
        Ray ray_new;
        RaycastHit hit_new;

        //if (Input.GetMouseButton(0))//Input.GetButtonDown("Fire1"))
        {
            ray_new = m_playerCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray_new, out hit_new, Mathf.Infinity, 1))
            {
                if (hit_new.collider != m_collider)
                {
                    //var pos = new Vector3(hit_new.point.x, Terrain.activeTerrain.SampleHeight(hit_new.point) + 1, hit_new.point.z);
                    //var normalVS = transform.InverseTransformDirection(m_groundDetector.GroundNormal);
                    //float pitch = Mathf.Asin(normalVS.z) * Mathf.Rad2Deg;

                    var dir = hit_new.point - transform.position;

                    //m_sceletonTransform.localEulerAngles = new Vector3(pitch, 180, 0);

                    m_targetPosition = hit_new.point;

                    if (Input.GetButton("Fire2"))
                        m_motor.MovementDirection = dir;
                    else
                        m_motor.MovementDirection = dir.normalized * 0.01f;//Vector3.zero;
                }
            }
        }
    }

    void Update()
	{
        var lc = GetComponent<LivingCreatureBehaviour>();
        if (!lc.IsAlive)
            return;

        if (Input.GetButton("Jump"))
            m_motor.Jump();

        //TODO: Ахтунг! Говнокод!
		if (Input.GetKey(KeyCode.Alpha1))
            m_caster.Cast(0, m_targetPosition);
        if (Input.GetKey(KeyCode.Alpha2))
            m_caster.Cast(1, m_targetPosition);
        if (Input.GetKey(KeyCode.Alpha3))
            m_caster.Cast(2, m_targetPosition);
        if (Input.GetKey(KeyCode.Alpha4))
            m_caster.Cast(3, m_targetPosition);
        if (Input.GetKey(KeyCode.Alpha5))
            m_caster.Cast(4, m_targetPosition);
        if (Input.GetKey(KeyCode.Alpha6))
            m_caster.Cast(5, m_targetPosition);
        if (Input.GetKey(KeyCode.Alpha7))
            m_caster.Cast(6, m_targetPosition);

        if (Input.GetButtonDown("Fire1") && m_InventoryBehaviour.ArmedWeapon != null)
        {
            m_InventoryBehaviour.ArmedWeapon.Fire(gameObject, m_targetPosition);
        }

        TestMousePointer();
    }

    void OnTriggerStay(Collider other)
    {
        var item = other.GetComponent<PickableItem>();
        if (item != null)
        {
            if (Input.GetButton("TakeItem"))
                m_InventoryBehaviour.PickUpItem(item);
        }
    }
}
