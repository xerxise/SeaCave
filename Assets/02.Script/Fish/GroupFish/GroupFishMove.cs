using UnityEngine;

public class GroupFishMove : MonoBehaviour
{
    public bool isLump;
    public Transform Target;
    public float Speed;
    public float MinDistance;

    Transform Tr;
    Rigidbody Rb;

    void Awake()
    {
        Tr = GetComponent<Transform>();
        Rb = GetComponent<Rigidbody>();
        Rb.drag = 0.2f;
    }

    void Update()
    {
        Vector3 diff = Target.position - Tr.position;

        Vector3 vec = diff.normalized;
        Quaternion toQuaternion = Quaternion.LookRotation(vec);

        float distance = Vector3.Distance(Target.localPosition, Tr.localPosition);
        float speed = Speed / (diff.magnitude + 0.01f);
        float maxSpeed = Speed / MinDistance;
        float minSpeed = Speed / 10;

        if (speed >= maxSpeed) speed = maxSpeed;
        else if (speed <= minSpeed) speed = minSpeed;

        Tr.rotation = Quaternion.Lerp(Tr.rotation, toQuaternion, speed / Speed);
        if (isLump)
        {
            //Lump
            Tr.Translate(Vector3.forward * speed * Time.deltaTime);
            Rb.velocity = Vector3.zero;
        }
        else
        {
            //Circle
            Rb.AddForce(Tr.forward * speed * Time.deltaTime);
            Rb.velocity = Tr.forward * speed * Time.deltaTime;
        }
    }
}