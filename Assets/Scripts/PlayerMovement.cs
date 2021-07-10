using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float clamp;
    private float torque_multiplier;
    private new Rigidbody2D rigidbody;

    public GameObject maskWrapper;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        maskWrapper = GameObject.Find("QSpriteMask");
        clamp = gameObject.GetComponent<ObjNum>().velocity_clamp;
        torque_multiplier = gameObject.GetComponent<ObjNum>().torque_multiplier;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PossessableObjectsClickManager.menuOpen == false)
        {
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                rigidbody.AddTorque((-150.0f * rigidbody.mass * torque_multiplier));
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                rigidbody.AddTorque((150.0f * rigidbody.mass * torque_multiplier));
            }
            rigidbody.angularVelocity = Mathf.Clamp(rigidbody.angularVelocity, -2 * clamp * rigidbody.mass, 2 * clamp * rigidbody.mass);
            //Debug.Log("-------");
            //Debug.Log(rigidbody.angularVelocity);
            //Debug.Log(clamp * rigidbody.mass);
            //Debug.Log("-------");
        }
    }
}
