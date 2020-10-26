using System.Collections;
using UnityEngine;
using DG.Tweening;
using DigitalRuby.Tween;

public class WaterSwirlEvent : MonoBehaviour
{
    public Transform CartTr;
    public Light Sun;
    public GameObject SwirlRock;
    public AudioSource UnderwaterAudio;
    public DepthUI Depth;
    public float ShakingTime = 15.0f;
    public float ToBeforeShakingTime = 1.0f;
    public float ShakingPower = 50.0f;
    public float RotateSpeed = 100.0f;
    public float TranslateDownSpeed = 10.0f;
    public float TranslateForwardSpeed = 3.0f;

    CartMove CartMove;
    Transform CartModelTr;
    Transform Tr;

    State IsStarted = State.Wait;
    enum State
    {
        Wait,
        Start,
        End
    }

    void Awake()
    {
        CartMove = CartTr.GetComponent<CartMove>();
        CartModelTr = CartTr.GetChild(0);
        Tr = GetComponent<Transform>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (IsStarted == State.Wait && other.gameObject.Equals(CartTr.gameObject))
        {
            IsStarted = State.Start;

            float beforeTimeScale = CartMove.PathTweener.timeScale;
            Vector3 beforePosition = CartTr.position;
            Quaternion beforeRotation = CartTr.rotation;

            CartMove.PathTweener.timeScale = 0.0f;
            CartMove.enabled = false;

            SwirlRock.SetActive(true);
            Tr.GetChild(0).gameObject.SetActive(true);

            Color beforeFogColor = RenderSettings.fogColor;
            CartMove.BeforeFogColor = beforeFogColor;
            gameObject.Tween("FogOff", beforeFogColor, Color.black, ShakingTime, TweenScaleFunctions.Linear,
                (t) =>
                {
                    RenderSettings.fogColor = t.CurrentValue;
                }, null);

            Color beforeSunColor = Sun.color;
            CartMove.BeforeSunColor = beforeSunColor;
            gameObject.Tween("LightOff", beforeSunColor, Color.black, ShakingTime, TweenScaleFunctions.Linear,
                (t) =>
                {
                    Sun.color = t.CurrentValue;
                }, null);

            gameObject.Tween("DepthUPWait", 0.0f, 0.0f, 4.0f, TweenScaleFunctions.Linear,
                (t) => { },
                (t) =>
                {
                    gameObject.Tween("DepthUP", Depth.FakeDepth, 400, 6.0f, TweenScaleFunctions.QuadraticEaseInOut,
                        (t2) =>
                        {
                            Depth.FakeDepth = (int)t2.CurrentValue;
                        }, null);
                });

            SoundManager.Instance.PlayOneShot(SoundManager.Instance.Sounds.CrasheBySharkEnginOff);
            CartModelTr.DOShakeRotation(ShakingTime, ShakingPower, 3, ShakingPower, true).OnComplete(
                () =>
                {
                    UnderwaterAudio.clip = SoundManager.Instance.Sounds.Deepsea;
                    UnderwaterAudio.Play();

                    SoundManager.Instance.PlayOneShot(SoundManager.Instance.Sounds.CrasheBySharkEnginOff);
                    CartModelTr.DOShakeRotation(ToBeforeShakingTime, ShakingPower, 10, ShakingPower, true);
                    CartTr.DORotate(beforeRotation.eulerAngles, ToBeforeShakingTime);
                    CartTr.DOMove(beforePosition, ToBeforeShakingTime).OnComplete(
                        () =>
                        {
                            IsStarted = State.End;

                            CartMove.PathTweener.timeScale = beforeTimeScale;
                            CartMove.enabled = true;

                            StartCoroutine(ToDestroy());
                        }
                    );
                }
            );

            SoundManager.Instance.PlayOneShot(SoundManager.Instance.Sounds.WhirlPool);
        }
    }

    void Update()
    {
        if (IsStarted == State.Start)
        {
            CartTr.RotateAround(Tr.position, Vector3.up, RotateSpeed * Time.deltaTime);
            CartTr.Translate(Vector3.down * TranslateDownSpeed * Time.deltaTime);
            CartTr.Translate(Vector3.forward * TranslateForwardSpeed * Time.deltaTime);
        }
    }

    IEnumerator ToDestroy()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(gameObject);
    }
}