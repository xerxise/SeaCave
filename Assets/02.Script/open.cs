using UnityEngine;

public class open : MonoBehaviour
{
    public Transform right;
    public Transform left;

    Vector3 rend;
    Vector3 lend;

    void Start()
    {
        rend = new Vector3(right.position.x, right.position.y, right.position.z - 15);
        lend = new Vector3(left.position.x, left.position.y, left.position.z + 15);
    }

    void Update()
    {
        right.position = Vector3.MoveTowards(right.position, rend, 7.5f * Time.deltaTime);
        left.position = Vector3.MoveTowards(left.position, lend, 7.5f * Time.deltaTime);
    }
}