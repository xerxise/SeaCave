using System.Linq;
using UnityEngine;

public class BubbleOnOff : MonoBehaviour
{
    public Transform Cart;
    public float Ondist;
    public float Offdist;

    Transform[] BubbleTr;
    GameObject[] BubbleIns;

    void Awake()
    {
        Cart.GetComponent<CartMove>().WaypointEnterListener += WaypointCallBack;

        BubbleCtrl[] BubbleBox = GetComponentsInChildren<BubbleCtrl>(true);
        BubbleTr = BubbleBox.Select((t) => t.GetComponent<Transform>()).ToArray<Transform>();
        BubbleIns = BubbleTr.Select((t) => t.gameObject).ToArray<GameObject>();
    }

    public void WaypointCallBack(int waypoint)
    {
        float sqrDiatance;
        int BubbleMax = BubbleTr.Length;

        for (int i = 0; i < BubbleMax; i++)
        {
            sqrDiatance = (Cart.position - BubbleTr[i].position).sqrMagnitude;

            if (sqrDiatance < Ondist)
            {
                BubbleIns[i].SetActive(true);
            }
            else if (sqrDiatance > Offdist)
            {
                BubbleIns[i].SetActive(false);
            }
        }
    }
}
