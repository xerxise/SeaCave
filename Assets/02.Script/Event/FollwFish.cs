using UnityEngine;

public class FollwFish : MonoBehaviour
{
    public CartMove CartMove;
    public int EventWaypoint = -1;

    GroupFishCtrl GroupFishCtrl;

    void Awake()
    {
        if (EventWaypoint >= 0) CartMove.WaypointEnterListener += WaypointCallBack;
        GroupFishCtrl = GetComponent<GroupFishCtrl>();
    }

    public void WaypointCallBack(int waypoint)
    {
        if (EventWaypoint == waypoint)
        {
            CartMove.WaypointEnterListener -= WaypointCallBack;
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}