using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DigitalRuby.Tween;

public class PathFishOnOff : MonoBehaviour
{
    public Transform Cart;
    public PathFishCtrl[] PathFishbox;
    public float Ondist;
    public float Offdist;
    private void Awake()
    {
        PathFishbox = GetComponentsInChildren<PathFishCtrl>(true);
        Cart.GetComponent<CartMove>().WaypointEnterListener += WaypointCallBack;
    }
    public void WaypointCallBack(int waypoint)
    {
        // Debug.Log(Mathf.Sqrt((Cart.position - Fishbox[1].transform.position).sqrMagnitude));
        for (int i = 0; i < PathFishbox.Length; i++)
        {
            if ((Cart.position - PathFishbox[i].transform.position).sqrMagnitude < Ondist)
            {
                PathFishbox[i].gameObject.SetActive(true);
            }
            else if ((Cart.position - PathFishbox[i].transform.position).sqrMagnitude > Offdist)
            {
                PathFishbox[i].gameObject.SetActive(false);
            }
        }
    }
}