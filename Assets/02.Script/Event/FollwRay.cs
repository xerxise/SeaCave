using UnityEngine;
using DG.Tweening;

public class FollwRay : MonoBehaviour
{
    public CartMove CartMove;
    public int EventWaypoint = -1;

    
    DOTweenPath DOTweenPath;

    void Awake()
    {
        CartMove.WaypointEnterListener += WaypointCallBack;
        DOTweenPath = GetComponent<DOTweenPath>();
    }

    public void WaypointCallBack(int waypoint)
    {
        if (EventWaypoint == waypoint)
        {
            CartMove.WaypointEnterListener -= WaypointCallBack;
            DOTweenPath.tween.Play();
        }
    }
}