using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackedControllerMovement : MonoBehaviour
{
    [SerializeField]
    public Transform rig;
    [SerializeField]
    public Transform head;
    [SerializeField]
    public Transform wheelchair;

    private SteamVR_Controller.Device device { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;
    private Vector3[] contPos;
    private int i;

    private Vector3 diferenca;

    private void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        contPos = new Vector3[2];
        contPos[0] = Vector3.zero;
        contPos[1] = Vector3.zero;
        i = 1;
        diferenca = rig.position - wheelchair.position;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(diferenca);
        if (device.GetPress(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger))
        {
            contPos[i] = transform.position;
            i++;
            if (i == 2)
                i = 0;

            Debug.Log(i);
            if (device.velocity.sqrMagnitude > 0.08)
            {
                Vector3 dir = contPos[1] - contPos[0];
                if (dir.y < 0 && dir.z < 0 || dir.y > 0 && dir.z > 0)
                {
                    rig.position = rig.position + head.forward * (3/2) * device.velocity.magnitude * Time.deltaTime;
                    wheelchair.position = rig.position - diferenca;
                }
                else
                {
                    dir = contPos[0] - contPos[1];
                    if(dir.y > 0 && dir.z > 0 || dir.y < 0 && dir.z < 0)
                    {
                        rig.position = rig.position + head.forward * (3/2) * device.velocity.magnitude * Time.deltaTime;
                        wheelchair.position = rig.position - diferenca;
                    }
                }

                rig.position = new Vector3(rig.position.x, 0, rig.position.z);
            }
        }
    }
}
