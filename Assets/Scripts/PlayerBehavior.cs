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
                    var dir = hit_new.point - transform.position;

                    //m_sceletonTransform.localEulerAngles = new Vector3(pitch, 180, 0);

                    m_caster.Target = new TargetInfo(hit_new.point, hit_new.normal);

                    
                    if (Input.GetButton("Fire2"))
                        m_motor.MovementDirection = dir;
                    else
                        m_motor.MovementDirection = dir.normalized * 0.01f;//Vector3.zero;
                    
                }
            }
        }
        /*
        if (Input.GetButton("Fire2"))
            m_motor.MovementDirection = ray_new.direction;
        else
            m_motor.MovementDirection = ray_new.direction * 0.01f;//Vector3.zero;
         */
    }

    void Update()
	{
        var lc = GetComponent<LivingCreatureBehaviour>();
        if (!lc.IsAlive)
            return;

        if (Input.GetButtonDown("Jump"))
            m_motor.Jump();

        //TODO: Ахтунг! Говнокод!
		if (Input.GetKey(KeyCode.Alpha1))
            m_caster.Cast(0);
        if (Input.GetKey(KeyCode.Alpha2))
            m_caster.Cast(1);
        if (Input.GetKey(KeyCode.Alpha3))
            m_caster.Cast(2);
        if (Input.GetKey(KeyCode.Alpha4))
            m_caster.Cast(3);
        if (Input.GetKey(KeyCode.Alpha5))
            m_caster.Cast(4);
        if (Input.GetKey(KeyCode.Alpha6))
            m_caster.Cast(5);
        if (Input.GetKey(KeyCode.Alpha7))
            m_caster.Cast(6);

        if (Input.GetButtonDown("Fire1") && m_InventoryBehaviour.ArmedWeapon)
        {
            m_InventoryBehaviour.ArmedWeapon.Fire(gameObject);
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
