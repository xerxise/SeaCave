using UnityEngine;
using UnityEngine.UI;
using DigitalRuby.Tween;

interface IPointerFocusLongEnter { void OnFocusLongEnter(GameObject sender); }

public class FocusPointer : MonoBehaviour
{
    public float AmountFillTime = 3.0f;
	public LayerMask LayerMask;

    Transform Tr;
    Image Pointer;
    Image PointerFulling;

    bool CanLongEnter;
    GameObject CurrentTarget;
    FloatTween ResizeTween;

    void Awake()
    {
        Tr = GetComponent<Transform>();
        Pointer = GetComponent<Image>();
        PointerFulling = Tr.GetChild(0).GetComponent<Image>();

        AmountFillTime = 1.0f / AmountFillTime;
    }

    void PointerResize(float size)
    {
        if (ResizeTween != null) ResizeTween.Stop(TweenStopBehavior.DoNotModify);

        ResizeTween = gameObject.Tween("ReSizePointer", Pointer.rectTransform.localScale.x, size, 0.25f, TweenScaleFunctions.Linear,
            (t) =>
            {
                Pointer.rectTransform.localScale = new Vector3(t.CurrentValue, t.CurrentValue, 1.0f);
            }, null);
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Tr.position, Tr.forward, out hit, Mathf.Infinity, LayerMask))
        {
            if (CurrentTarget == null || !CurrentTarget.Equals(hit.transform.gameObject))
            {
                CurrentTarget = hit.transform.gameObject;
                PointerFulling.fillAmount = 0.0f;

                IPointerFocusLongEnter focusLongEnter = CurrentTarget.GetComponent<IPointerFocusLongEnter>();
                if (focusLongEnter != null)
                {
                    CanLongEnter = true;
                    PointerResize(3.0f);
                }
                else
                {
                    CanLongEnter = false;
                    PointerResize(1.0f);
                }
            }
        }
        else
        {
            CurrentTarget = null;
            PointerFulling.fillAmount = 0.0f;

            CanLongEnter = false;
            PointerResize(1.0f);
        }

        if (CanLongEnter)
        {
            if (PointerFulling.fillAmount < 1.0f)
            {
                PointerFulling.fillAmount += AmountFillTime * Time.deltaTime;
            }
            else
            {
                PointerFulling.fillAmount = 0.0f;
                IPointerFocusLongEnter focusLongEnter = CurrentTarget.GetComponent<IPointerFocusLongEnter>();
                if (focusLongEnter != null) focusLongEnter.OnFocusLongEnter(gameObject);
            }
        }
    }
}