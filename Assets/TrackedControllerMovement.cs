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
    private GameObject camera;
    private int i, timer=0;

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
        wheelchair.eulerAngles = new Vector3(wheelchair.eulerAngles.x, head.eulerAngles.y, head.eulerAngles.z);
        wheelchair.position = new Vector3(wheelchair.position.x, wheelchair.position.y, head.position.z);
        //Debug.Log(diferenca);
        if (device.GetPress(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger))
        {
            
            contPos[i] = transform.position;
            i++;
            /*if (i == 2)
                i = 0;*/

            Debug.Log(i);
            if (device.velocity.sqrMagnitude > 0 && i==2)
            {
                Vector3 dir = contPos[1] - contPos[0];
                if (dir.z < 0){
                    rig.position = rig.position - head.forward * (8/2) * device.velocity.magnitude * Time.deltaTime;
                    wheelchair.position = rig.position - diferenca;
                    wheelchair.position = new Vector3(wheelchair.position.x, wheelchair.position.y, head.position.z);
                }
                else if(dir.z > 0){
                    rig.position = rig.position + head.forward * (8/2) * device.velocity.magnitude * Time.deltaTime;
                    wheelchair.position = rig.position - diferenca;
                    wheelchair.position = new Vector3(wheelchair.position.x, wheelchair.position.y, head.position.z);
                }
                /*if (dir.y < 0 && dir.z < 0 || dir.y > 0 && dir.z > 0)
                {
                    rig.position = rig.position - head.forward * (4/2) * device.velocity.magnitude * Time.deltaTime;
                    wheelchair.position = rig.position - diferenca;
                }
                else
                {
                    dir = contPos[0] - contPos[1];
                    if(dir.y > 0 && dir.z > 0 || dir.y < 0 && dir.z < 0)
                    {
                        rig.position = rig.position - head.forward * (4/2) * device.velocity.magnitude * Time.deltaTime;
                        wheelchair.position = rig.position - diferenca;
                    }
                    else{
                        rig.position = rig.position + head.forward * (4/2) * device.velocity.magnitude * Time.deltaTime;
                        wheelchair.position = rig.position - diferenca;
                    }
                }*/

                rig.position = new Vector3(rig.position.x, 0, rig.position.z);
                i=0;
            }
        }
    }
}
