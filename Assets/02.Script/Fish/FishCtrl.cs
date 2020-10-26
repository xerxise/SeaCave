using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FishCtrl : MonoBehaviour
{
    public float MinSpeed = 1f, MaxSpeed = 3f;
    public float MinDelayRotate = 1f, MaxDelayRotate = 3f;
    public Vector3 Size = Vector3.one;
    public GameObject[] MovePrefabs;
    public List<Transform> MoveObj = new List<Transform>();
    public bool isAddSpped = false;

    Transform tr;
    Animator[] Animator = null;
    Rigidbody[] MoveObjRigid;
    float speed;

    void Awake()
    {
        tr = GetComponent<Transform>();
        Animator = new Animator[MoveObj.Count];
        MoveObjRigid = new Rigidbody[MoveObj.Count];
    }

    void Start()
    {
        for (int cnt = 0; cnt < MoveObj.Count; cnt++)
        {
            MoveObj[cnt].localPosition = new Vector3(
                Random.Range(-Size.x / 2, Size.x / 2),
                Random.Range(-Size.y / 2, Size.y / 2),
                Random.Range(-Size.z / 2, Size.z / 2)
            );

            if (MoveObj[cnt].CompareTag("FISH"))
            {
                Animator[cnt] = MoveObj[cnt].GetComponent<Animator>();
                StartCoroutine(MoveCoroutine(MoveObj[cnt]));
            }
            else
            {
                StartCoroutine(MoveCoroutine(MoveObj[cnt].GetChild(0)));
            }

            MoveObjRigid[cnt] = MoveObj[cnt].GetComponent<Rigidbody>();
        }
    }

    public void ReStart()
    {
        Animator = new Animator[MoveObj.Count];
        MoveObjRigid = new Rigidbody[MoveObj.Count];

        for (int cnt = 0; cnt < MoveObj.Count; cnt++)
        {
            Animator[cnt] = MoveObj[cnt].GetComponent<Animator>();
            StartCoroutine(MoveCoroutine(MoveObj[cnt]));
            StartCoroutine(MoveCoroutine(MoveObj[cnt].GetChild(0)));
            MoveObjRigid[cnt] = MoveObj[cnt].GetComponent<Rigidbody>();
        }

        StartCoroutine(AddSpeed());
    }

    void Update()
    {
        int cnt;
        int maxCnt = MoveObj.Count;

        for (cnt = 0; cnt < maxCnt; cnt++)
        {
            speed = Random.Range(MinSpeed, MaxSpeed);

            if (isAddSpped)
            {
                speed *= 5.0f;
            }

            Animator[cnt].speed = speed / 5.0f;
        }

        Vector3 vecFow = new Vector3(0.0f, 0.0f, 1.0f);
        for (cnt = 0; cnt < maxCnt; cnt++)
        {
            MoveObj[cnt].Translate(vecFow * speed * Time.deltaTime);
        }

        Vector3 vecZero = new Vector3(0.0f, 0.0f, 0.0f);
        for (cnt = 0; cnt < maxCnt; cnt++)
        {
            MoveObjRigid[cnt].velocity = vecZero;
        }
    }

    IEnumerator MoveCoroutine(Transform moveTr)
    {
        float sizeX = Size.x / 2;
        float sizeY = Size.y / 2;
        float sizeZ = Size.z / 2;
        float randomDelay;
        Vector3 randomTarget;

        while (moveTr != null)
        {
            randomTarget = new Vector3(
                Random.Range(tr.position.x + -sizeX, tr.position.x + sizeX),
                Random.Range(tr.position.y + -sizeY, tr.position.y + sizeY),
                Random.Range(tr.position.z + -sizeZ, tr.position.z + sizeZ)
            );
            randomDelay = Random.Range(2, 8);
            moveTr.DOLookAt(randomTarget, randomDelay);
            yield return YieldInstructionCache.WaitForSeconds(randomDelay);
        }
    }

    IEnumerator AddSpeed()
    {
        isAddSpped = true;
        yield return YieldInstructionCache.WaitForSeconds(2.0f);
        isAddSpped = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, Size);
    }
}