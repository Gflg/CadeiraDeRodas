using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class ChairController : MonoBehaviour
{
    public InputManager inp;
    public List<WheelCollider> throttleWheels;
    public List<WheelCollider> steeringWheels;
    public float strenghtCoefficient = 20000f;
    public float maxTurn = 45f;
    // Start is called before the first frame update
    void Start()
    {
        inp = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        print(inp.steer);
        foreach (WheelCollider wheel in throttleWheels)
        {
            wheel.motorTorque = strenghtCoefficient * Time.deltaTime * inp.throttle;
        }
        foreach (WheelCollider wheel in steeringWheels)
        {
            wheel.steerAngle = maxTurn * inp.steer;
        }
    }
}
