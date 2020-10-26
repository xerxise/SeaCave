using System.Collections;
using UnityEngine;

public class GroupFishWave : MonoBehaviour
{
    public Transform Target;
    public float Speed;

    Animator anim;
    float animSpeed;

    Transform Tr;
    Rigidbody Rb;

    void Start()
    {
        anim = GetComponent<Animator>();
        Tr = GetComponent<Transform>();
        Rb = GetComponent<Rigidbody>();

        StartCoroutine(RandomSpeed());
    }

    void Update()
    {
        //이동
        Vector3 diff = Target.position - Tr.position;
        float dis = diff.sqrMagnitude;

        if (dis <= 1.0f)
        {
            Tr.Translate(Vector3.forward * 0.001f * Time.deltaTime);
        }
        else
        {
            Tr.Translate(Vector3.forward * Speed * Time.deltaTime);
        }

        //회전
        Vector3 vec = diff.normalized;
        Quaternion toQuaternion = Quaternion.LookRotation(vec);
        Tr.rotation = Quaternion.Lerp(Tr.rotation, toQuaternion, 0.2f);

        Rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
    }

    IEnumerator RandomSpeed()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(5f);
            animSpeed = Random.Range(0.75f, 1.25f);
            anim.speed = animSpeed;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Target.position, 1f);
    }
}