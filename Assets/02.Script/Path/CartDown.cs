using UnityEngine;
using DG.Tweening;

public class CartDown : MonoBehaviour
{
    public GameObject UnderWaterCameraEffects;
    public GameObject UnderwaterAudio;
    public Transform Path;
    public float DownSpeed = 50.0f;

    Transform Tr;
    CartMove CartMove;

    void Awake()
    {
        Tr = GetComponent<Transform>();
        CartMove = GetComponent<CartMove>();
    }

    void Update()
    {
        if (Tr.position.y > 930.0f)
        {
            Tr.Translate(Vector3.down * DownSpeed * Time.deltaTime);
        }
        else if (DownSpeed == 5.0f)
        {
            Tr.DOLookAt(Tr.position - Path.position, 2.0f).SetEase(Ease.InSine).OnComplete(
                () =>
                {
                    CartMove.enabled = true;
                    enabled = false;
                }
            );
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            DownSpeed = 5.0f;
            Tr.DOShakeRotation(0.5f, 10.0f, 1, 10.0f).OnComplete(
                () => Tr.DORotate(new Vector3(0.0f, 90.0f, 0.0f), 0.5f)
            );
            UnderWaterCameraEffects.SetActive(true);
            UnderwaterAudio.SetActive(true);
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.Sounds.CrasheByWater);
        }
    }
}