using UnityEngine;
using DigitalRuby.Tween;

public class Octopus : MonoBehaviour
{
    public Transform CartTr;
    public GameObject BlackMeteor;
    public int EventWaypoint = -1;
    public float MoveSpeed = 100.0f;
    public float RotateSpeed = 100.0f;

    CartMove CartMove;
    Transform Tr;

    OctState State = OctState.Wait;
    enum OctState
    {
        Wait,
        MoveToCart,
        GoWay
    }

    void Awake()
    {
        CartMove = CartTr.GetComponent<CartMove>();
        CartMove.WaypointEnterListener += WaypointCallBack;
        Tr = GetComponent<Transform>();
    }

    public void WaypointCallBack(int waypoint)
    {
        if (EventWaypoint == waypoint)
        {
            State = OctState.MoveToCart;
        }
        else if (EventWaypoint + 1 == waypoint)
        {
            CartMove.WaypointEnterListener -= WaypointCallBack;
            State = OctState.Wait;
        }
    }

    void Update()
    {
        if (State == OctState.MoveToCart)
        {
            Quaternion lookRotation = Quaternion.LookRotation(CartTr.position - Tr.position);
            Tr.rotation = Quaternion.Lerp(Tr.rotation, lookRotation, RotateSpeed * Time.deltaTime);
            Tr.Translate(Vector3.forward * MoveSpeed * Time.deltaTime);

            if ((CartTr.position - Tr.position).sqrMagnitude < 25600.0f)
            {
                State = OctState.GoWay;

                float beforeTimeScale = CartMove.PathTweener.timeScale;

                gameObject.Tween("CartSleep", beforeTimeScale, 0.1f, 0.5f, TweenScaleFunctions.CubicEaseOut,
                    (t) =>
                    {
                        CartMove.PathTweener.timeScale = t.CurrentValue;
                    },
                    (t) =>
                    {
                        BlackMeteor.SetActive(true);
                        Invoke("PlaySound", 0.45f);
                        gameObject.Tween("CartSleep", 0.0f, 0.0f, 5.0f, TweenScaleFunctions.CubicEaseOut, null,
                            (t2) =>
                            {
                                gameObject.Tween("CartSleep", CartMove.PathTweener.timeScale, beforeTimeScale, 3.0f, TweenScaleFunctions.CubicEaseOut,
                                    (t3) =>
                                    {
                                        CartMove.PathTweener.timeScale = t3.CurrentValue;
                                    }, null);
                            }
                        );
                    }
                );
            }
        }
        else if (State == OctState.GoWay)
        {
            Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward + Vector3.up + (Vector3.down * 2.0f));
            Tr.rotation = Quaternion.Lerp(Tr.rotation, lookRotation, RotateSpeed * 2.0f * Time.deltaTime);
            Tr.Translate(Vector3.forward * MoveSpeed * 1.0f * Time.deltaTime);
        }
    }

    void PlaySound()
    {
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.Sounds.Ink);
    }
}