using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using DigitalRuby.Tween;
using UnityEngine;

public class CartMove : MonoBehaviour
{
    public Transform target;
    public DOTweenPath path;
    public float speed = 0.0f;
    public float rotSpeed = 0.0f;
    public float playTime = 250.0f;

    public TweenerCore<Vector3, Path, PathOptions> PathTweener;
    public Action<int> WaypointEnterListener;
    [SerializeField]
    GameObject Spotlights;
    [SerializeField]
    GameObject WaterSwirlEvent;
    [SerializeField]
    public GameObject JellyfishEvent;
    [SerializeField]
    public GameObject EndingEvent;
    [SerializeField]
    public MeshRenderer AirLens;
    [SerializeField]
    public GameObject UnderWaterCameraEffects;
    [SerializeField]
    //쓰레기 치우기 이벤트 활성화 될 장소
    GameObject TrashEvent;

    public Color BeforeFogColor;
    public Color BeforeSunColor;

    Transform Tr;
    Rigidbody Rd;

    void Awake()
    {
        Tr = GetComponent<Transform>();
        Rd = GetComponent<Rigidbody>();
    }

    void Start()
    {
        path.wps.Insert(0, Tr.position);

        Vector3[] waypoints = path.wps.ToArray();
        PathTweener = target.DOPath(waypoints, playTime, PathType.CatmullRom, PathMode.Full3D, 10).SetUpdate(UpdateType.Fixed).SetEase(Ease.Linear).SetLookAt(1.0f).OnWaypointChange(MyCallback);
    }

    void Update()
    {
        Vector3 relativePosition = target.position - Tr.position;
        float dist = relativePosition.sqrMagnitude;

        Rd.MovePosition(Vector3.MoveTowards(Tr.position, target.position, speed * Time.deltaTime));

        Quaternion rot = Quaternion.LookRotation(relativePosition);
        Rd.MoveRotation(Quaternion.Lerp(Tr.rotation, rot, rotSpeed * Time.deltaTime));
    }

    void SetSpeed(float speedToScale, float maxSpeed, float changeTime)
    {
        gameObject.Tween("SetSpeed", speed, maxSpeed, changeTime, TweenScaleFunctions.Linear,
            (t) =>
            {
                speed = t.CurrentValue;
                PathTweener.timeScale = speedToScale * (speed / maxSpeed);
            }, null);
    }

    void MyCallback(int waypointIndex)
    {
        if (WaypointEnterListener != null) WaypointEnterListener(waypointIndex);

        float speedToScale = 1.0f;
        float maxSpeed = 200.0f;
        float changeTime = 2.0f;

        switch (waypointIndex)
        {
            case 1:
                SetSpeed(speedToScale, maxSpeed, changeTime);
                break;

            case 3:
                Spotlights.SetActive(true);
                break;

            case 5:
                UnderWaterCameraEffects.SetActive(false);
                break;

            case 10:
                SetSpeed(speedToScale, maxSpeed / 2, changeTime);
                rotSpeed = 100.0f;
                break;

            case 15:
                SetSpeed(speedToScale, maxSpeed, changeTime);
                break;

            case 28:
                SetSpeed(speedToScale, maxSpeed / 3, changeTime);
                break;

            case 36:
                UnderWaterCameraEffects.SetActive(true);
                SetSpeed(speedToScale, maxSpeed, changeTime);
                break;

            case 38:
                UnderWaterCameraEffects.SetActive(false);
                SetSpeed(speedToScale, maxSpeed, changeTime);
                break;

            case 39:
                OTwoUI.Instance.HPDecrease();
                SetSpeed(speedToScale, maxSpeed * 2, changeTime);
                WaterSwirlEvent.SetActive(true);
                break;

            case 43:
                OTwoUI.Instance.HPDecrease();
                break;

            case 45:
                OTwoUI.Instance.HPDecrease();
                break;

            case 48:
                gameObject.Tween("TimeScaleDown", PathTweener.timeScale, 0.0f, 4.0f, TweenScaleFunctions.Linear,
                    (t) =>
                    {
                        SoundManager.Instance.PauseLoop();
                        PathTweener.timeScale = t.CurrentValue;
                    },
                    (t) =>
                    {
                        UnderWaterCameraEffects.SetActive(true);
                        JellyfishEvent.SetActive(true);
                        enabled = false;
                    });
                rotSpeed = 1.0f;
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.Sounds.Crashed);
                break;

            case 49:
                AirLens.enabled = true;
                break;

            case 50:
                EndingEvent.SetActive(true);
                break;
        }
    }
}