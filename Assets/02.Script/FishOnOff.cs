using System.Linq;
using UnityEngine;

public class FishOnOff : MonoBehaviour
{
    public Transform Cart;
    public float Ondist;
    public float Offdist;

    Transform[] FishTr;
    GameObject[] FishIns;

    void Awake()
    {
        Cart.GetComponent<CartMove>().WaypointEnterListener += WaypointCallBack;

        FishCtrl[] Fishbox = GetComponentsInChildren<FishCtrl>(true);
        FishTr = Fishbox.Select((t) => t.GetComponent<Transform>()).ToArray<Transform>();
        FishIns = FishTr.Select((t) => t.gameObject).ToArray<GameObject>();
    }

    public void WaypointCallBack(int waypoint)
    {
        float sqrDiatance;
        int fishMax = FishTr.Length;

        for (int i = 0; i < fishMax; i++)
        {
            sqrDiatance = (Cart.position - FishTr[i].position).sqrMagnitude;

            if (sqrDiatance < Ondist)
            {
                FishIns[i].SetActive(true);
            }
            else if (sqrDiatance > Offdist)
            {
                FishIns[i].SetActive(false);
            }
        }
    }
}