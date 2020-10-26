using System.Linq;
using UnityEngine;

public class EnvironmentOnOff : MonoBehaviour
{
    public Transform Cart;
    public float Ondist;
    public float Offdist;

    Transform[] EnviTr;
    GameObject[] EnviIns;

    void Awake()
    {
        Cart.GetComponent<CartMove>().WaypointEnterListener += WaypointCallBack;

        EnviCtrl[] enviBox = GetComponentsInChildren<EnviCtrl>(true);
        EnviTr = enviBox.Select((t) => t.GetComponent<Transform>()).ToArray<Transform>();
        EnviIns = EnviTr.Select((t) => t.gameObject).ToArray<GameObject>();
    }

    public void WaypointCallBack(int waypoint)
    {
        float sqrDiatance;
        int enviMax = EnviTr.Length;

        for (int i = 0; i < enviMax; i++)
        {
            sqrDiatance = (Cart.position - EnviTr[i].position).sqrMagnitude;

            if (sqrDiatance < Ondist)
            {
                EnviIns[i].SetActive(true);
            }
            else if (sqrDiatance > Offdist)
            {
                EnviIns[i].SetActive(false);
            }
        }
    }
}