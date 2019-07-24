using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackedControllerMovement : MonoBehaviour
{
    [SerializeField]
    public Transform rig;
    [SerializeField]
    public Transform head;

    private SteamVR_Controller.Device device { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;
    private Vector3[] contPos;
    private int i;

    private void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        contPos = new Vector3[2];
        contPos[0] = Vector3.zero;
        contPos[1] = Vector3.zero;
        i = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (device.GetPress(Valve.VR.EVRButtonId.k_EButton_Grip))
        {
            contPos[i] = transform.position;
            i++;
            if (i == 2)
                i = 0;

            if (device.velocity.sqrMagnitude > 1)
            {
                Vector3 dir = contPos[1] - contPos[0];
                if (dir.y < 0 && dir.z < 0 || dir.y > 0 && dir.z > 0)
                {
                    rig.position = rig.position + head.forward * device.velocity.magnitude * Time.deltaTime;
                }
                else
                {
                    dir = contPos[0] - contPos[1];
                    if(dir.y > 0 && dir.z > 0 || dir.y < 0 && dir.z < 0)
                    {
                        rig.position = rig.position + head.forward * device.velocity.magnitude * Time.deltaTime;
                    }
                }

                rig.position = new Vector3(rig.position.x, 0, rig.position.z);
            }
        }
    }
}
