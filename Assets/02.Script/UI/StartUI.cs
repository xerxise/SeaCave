using System.Collections;
using UnityEngine;
using DG.Tweening;

public class StartUI : MonoBehaviour, IPointerFocusLongEnter
{
    public open Door;
    public Transform CartTr;
    public Transform CraneBone;
    public Animator CraneAnim;

    CartDown CartDown;

    void Awake()
    {
        CartDown = CartTr.GetComponent<CartDown>();
    }

    public void OnFocusLongEnter(GameObject sender)
    {
        Destroy(sender);
        Destroy(transform.GetChild(0).gameObject);

        StartCoroutine(CartMoveBefore());
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.Sounds.Start);
    }

    IEnumerator CartMoveBefore()
    {
        yield return YieldInstructionCache.WaitForSeconds(3.0f);
        Door.enabled = true;
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.Sounds.Dooropen);
        CraneAnim.SetBool("isStart", true);
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.Sounds.CraneUP);
        yield return YieldInstructionCache.WaitForSeconds(3.25f);
        CartTr.parent = CraneBone;
        CartTr.DOShakeRotation(4.0f, 3.0f, 2, 3.0f).OnComplete(
            () => StartCoroutine(CartMoveStart())
        );

        SoundManager.Instance.PlayOneShot(SoundManager.Instance.Sounds.CraneDown);
    }

    IEnumerator CartMoveStart()
    {
        yield return YieldInstructionCache.WaitForSeconds(0.5f);
        CartTr.parent = null;
        CartDown.enabled = true;
        Door.enabled = false;
        CraneAnim.SetBool("isStart", false);
        Destroy(gameObject);
    }
}