using UnityEngine;
using DG.Tweening;
using DigitalRuby.Tween;

public class SharkEvent : MonoBehaviour
{
    public Transform CartTr;
    public Transform Target;
    public int EventWaypoint = -1;
    public float MoveSpeed = 100.0f;
    public float RotateSpeed = 100.0f;
    public float ShakingPower = 40.0f;
    public GameObject Sparks;

    [Space]
    public float SharkFloat = 0;

    [Space]
    public Texture BreakGlass;

    bool IsStarted, IsEnding;
    CartMove CartMove;
    Transform Tr;
    Rigidbody Rb;
    Animator At;

    float BeforeTimeScale;

    void Awake()
    {
        CartMove = CartTr.GetComponent<CartMove>();
        CartMove.WaypointEnterListener += WaypointCallBack;
        Tr = GetComponent<Transform>();
        Rb = GetComponent<Rigidbody>();
        At = GetComponent<Animator>();
    }

    public void WaypointCallBack(int waypoint)
    {
        if (EventWaypoint == waypoint)
        {
            CartMove.WaypointEnterListener -= WaypointCallBack;
            IsStarted = true;
            IsEnding = false;

            BeforeTimeScale = CartMove.PathTweener.timeScale;

            gameObject.Tween("TimeScaleDown", CartMove.PathTweener.timeScale, 0.0f, 4.0f, TweenScaleFunctions.Linear,
                (t) =>
                {
                    CartMove.PathTweener.timeScale = t.CurrentValue;
                },
                (t) =>
                {
                    CartMove.enabled = false;
                }
            );
        }
    }

    void Update()
    {
        if (!IsEnding)
        {
            Quaternion lookRotation = Quaternion.LookRotation(Target.position - Tr.position);
            Tr.rotation = Quaternion.Lerp(Tr.rotation, lookRotation, RotateSpeed * Time.deltaTime);
        }
        if (IsStarted)
        {
            At.SetBool("Swiming", true);
            Tr.Translate(Vector3.forward * MoveSpeed * Time.deltaTime);
            SharkFloat += Time.deltaTime;
            At.SetFloat("AttackNum", SharkFloat);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (IsStarted && collision.gameObject.Equals(CartTr.gameObject))
        {
            IsStarted = false;

            Rb.velocity = Vector3.zero;
            Rb.angularVelocity = Vector3.zero;
            Rb.isKinematic = true;

            Shaking();

            Tr.DOMove(Tr.position + Vector3.forward * 40.0f, 2.5f).OnComplete(
                () =>
                {
                    Tr.DOMove(Tr.position + (Vector3.back * 100.0f) + (Vector3.left * 65.0f), 12.5f);
                }
            );

            At.SetBool("Swiming", false);
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.Sounds.CrasheByShark);
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.Sounds.Electronic1);
        }
    }

    void Shaking()
    {
        Quaternion beforeRotation = CartTr.rotation;

		SoundManager.Instance.PlayOneShot(SoundManager.Instance.Sounds.Glassbreak);
		CartTr.GetChild(0).GetChild(0).gameObject.SetActive(true);

		CartTr.DOShakeRotation(2.0f, ShakingPower, 10, ShakingPower, true).OnComplete(
            () =>
            {
                CartTr.DORotate(beforeRotation.eulerAngles, 4.0f).OnComplete(
                    () =>
                    {
                        SoundManager.Instance.PlayOneShot(SoundManager.Instance.Sounds.Shark);
                        CartTr.GetChild(0).GetChild(0).gameObject.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", BreakGlass);
                        IsEnding = true;
                        CartTr.DOShakeRotation(2.0f, ShakingPower, 10, ShakingPower, true).OnComplete(
                            () =>
                            {
                                SoundManager.Instance.PlayOneShot(SoundManager.Instance.Sounds.SubmarineTurnon);
                                SoundManager.Instance.GetComponent<AudioSource>().volume = 0.3f;
                                CartTr.DORotate(beforeRotation.eulerAngles, 4.0f).OnComplete(
                                    () =>
                                    {
                                        Sparks.SetActive(true);
                                        SoundManager.Instance.PlayLoop(SoundManager.Instance.Sounds.BrokenMachine);
                                        CartMove.PathTweener.timeScale = BeforeTimeScale;
                                        CartMove.enabled = true;
                                    }
                                );
                            }
                        );
                    }
                );
            }
        );
    }
}