using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Ending : MonoBehaviour
{
    //public Transform Crane;
    public Transform CraneBone;
    public Animator CraneAnim;
    public Transform CartTr;

    CartMove CartMove;

    void Awake()
    {
        CartMove = CartTr.GetComponent<CartMove>();
    }

    IEnumerator Start()
    {
        yield return YieldInstructionCache.WaitForSeconds(15.0f);
        CartMove.enabled = false;
        yield return CartTr.DOLocalMove(new Vector3(CartTr.position.x, 960.0f, CartTr.position.z), 4.0f).WaitForCompletion();
        OTwoUI.Instance.HPReset();
        CraneAnim.SetBool("isEnding", true);
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.Sounds.CraneUP);
        yield return YieldInstructionCache.WaitForSeconds(2.15f);
        CartTr.parent = CraneBone;
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.Sounds.CraneDown);
        yield return YieldInstructionCache.WaitForSeconds(1.45f);
        CartTr.parent = null;
        yield return YieldInstructionCache.WaitForSeconds(2.45f);
        CraneAnim.SetBool("isEnding", false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}