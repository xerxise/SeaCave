using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour
{
    public Transform CartTr;
    public float PlayDistance = 20.0f;
    public float PlayTime = 1.0f;

    Transform Tr;
    CartMove CartMove;
    AudioSource Audio;
    float PlayDistanceSqr;

    AudioState State = AudioState.Pause;
    enum AudioState
    {
        Pause,
        VolumeUp,
        VolumeDown
    }

    void Awake()
    {
        Tr = GetComponent<Transform>();

        CartMove = CartTr.GetComponent<CartMove>();
        CartMove.WaypointEnterListener += WaypointCallBack;

        Audio = GetComponent<AudioSource>();
        Audio.mute = true;
        Audio.loop = true;
        Audio.volume = 0.0f;

        PlayDistanceSqr = Mathf.Pow(PlayDistance, 2);
    }

    void WaypointCallBack(int waypoint)
    {
        float distance = (CartTr.position - Tr.position).sqrMagnitude;

        if (distance < PlayDistanceSqr)
        {
            CartMove.WaypointEnterListener -= WaypointCallBack;
            Audio.mute = false;
            State = AudioState.VolumeUp;
        }
    }

    void Update()
    {
        if (State == AudioState.VolumeUp)
        {
            Audio.volume += Time.deltaTime / PlayTime;
            if (Audio.volume >= 1.0f)
                State = AudioState.VolumeDown;
        }
        else if (State == AudioState.VolumeDown)

        {
            Audio.volume -= Time.deltaTime / PlayTime;
            if (Audio.volume <= 0.0f)
                State = AudioState.Pause;
        }
    }
}