using UnityEngine;
using DG.Tweening;
using DigitalRuby.Tween;

public class JellyfishUp : MonoBehaviour
{
    public Transform CartTr;
    public Transform Target;
    public Light Sun;
    public GameObject DangerGlass;
    public OTwoUI OTwoUI;
    public DepthUI Depth;

    CartMove CartMove;
    float enabledTime;

    bool AudioFlag;

    void Awake()
    {
        CartMove = CartTr.GetComponent<CartMove>();
    }

    void Update()
    {
        Target.Translate(Vector3.up * 4.0f * Time.deltaTime);
        CartTr.Translate(Vector3.back * 0.7f * Time.deltaTime);

        enabledTime += Time.deltaTime;

        if (enabledTime > 14.0f && !AudioFlag)
        {
            DangerGlass.SetActive(true);
            OTwoUI.Instance.HPDecrease();
            SoundManager.Instance.PlayLoop(SoundManager.Instance.Sounds.WarningSlow);
            SoundManager.Instance.GetComponent<AudioSource>().volume = 1.0f;
            AudioFlag = true;
        }

        if (enabledTime > 20.0f)
        {
            SoundManager.Instance.PauseLoop();
            CartTr.DOShakeRotation(2.0f, 40.0f, 10, 40.0f, true);

            DangerGlass.SetActive(false);
            OTwoUI.Flag = false;
            OTwoUI.Filter.enabled = false;

            CartMove.PathTweener.timeScale = 2.0f;
            CartMove.speed = 200.0f;
            CartMove.enabled = true;

            gameObject.Tween("FogOn", RenderSettings.fogColor, CartMove.BeforeFogColor, 15.0f, TweenScaleFunctions.Linear,
                (t) =>
                {
                    RenderSettings.fogColor = t.CurrentValue;
                }, null);

            gameObject.Tween("LightOn", Sun.color, CartMove.BeforeSunColor, 15.0f, TweenScaleFunctions.Linear,
                (t) =>
                {
                    Sun.color = t.CurrentValue;
                }, null);

            gameObject.Tween("DepthDown", Depth.FakeDepth, 0, 6.0f, TweenScaleFunctions.QuadraticEaseInOut,
                (t) =>
                {
                    Depth.FakeDepth = (int)t.CurrentValue;
                }, null);

            enabled = false;

            SoundManager.Instance.PlayOneShot(SoundManager.Instance.Sounds.CrasheByShark);
        }
    }
}