using RootMotion.FinalIK;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashGrab : MonoBehaviour
{
    //
    [SerializeField]
    AirVRCameraRig _rig;
    //입력을 받아 처리하기 위한 설정
    [SerializeField]
    Transform rightHand, leftHand;
    //움직이게 할 것:손....?
    [SerializeField]
    GameObject Claw;
    [SerializeField]
    FABRIK ik;
    [SerializeField]
    Transform Target;
    //집은 물건
    Transform pickObj;
    Transform initialPosition;

    Ray ray;
    RaycastHit hit;
    [SerializeField]
    AudioSource clawSound;
    [SerializeField]
    Camera _Eye;
    [SerializeField]
    LayerMask layer;
    bool isPickable;
    void Start()
    {
        initialPosition = transform;
        isPickable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ray = _Eye.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 300))
            {
                if ((bool)hit.collider?.gameObject.tag.Contains("Linkable"))
                {
                    //움직임 시도
                    StartCoroutine(Pick(hit.transform));
                }
            }
        }
        if(AirVRInput.GetDown(_rig, AirVRInput.Button.RIndexTrigger))
        {
            ray = new Ray(rightHand.position, rightHand.forward);
            if(Physics.Raycast(ray, out hit, 300))
            {
                switch (hit.collider?.tag)
                {
                    case "Linkable":
                        StartCoroutine(Pick(hit.transform));
                        break;
                    default:

                        break;
                }
            }
        }
        if (!isPickable)
        {
            if(pickObj == null)
            {
                isPickable = true;
            }
        }
    }

    IEnumerator Pick(Transform pickTarget)
    {
        if (isPickable)
        {
            pickObj = pickTarget;
            ik.solver.target = pickTarget;
            pickObj.GetComponentInChildren<Trashes>().Picked();
            isPickable = false;
            yield return null;
        }
    }

}
