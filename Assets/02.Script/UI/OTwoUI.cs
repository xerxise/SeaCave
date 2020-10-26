using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MeshRenderer))]
public class OTwoUI : MonoBehaviour
{
    public static OTwoUI Instance;

    public Sprite[] OTwoImage;
    public GameObject DangerGlass;
    public Texture[] DangerTexture;
    public CameraFilterPack_Atmosphere_Fog Filter;

    public bool Flag = true;
    public int WarningHP = 2;

    int HP;
    Image ImageComp;
    Material MaterialComp;

    void Awake()
    {
        Instance = this;

        ImageComp = GetComponent<Image>();
        MaterialComp = DangerGlass.GetComponent<MeshRenderer>().material;

        HP = OTwoImage.Length - 1;
        ImageComp.sprite = OTwoImage[HP];
    }

    public void HPReset()
    {
        HP = OTwoImage.Length - 1;
        ImageComp.sprite = OTwoImage[HP];
        Filter.enabled = false;
        MaterialComp.SetTexture("_MainTex", DangerTexture[0]);
        StopAllCoroutines();
    }

    public void HPDecrease()
    {
        HP = Mathf.Max(HP - 1, 0);
        ImageComp.sprite = OTwoImage[HP];

        if (HP <= WarningHP)
        {
            StopAllCoroutines();
            StartCoroutine(Warning());
        }
    }

    IEnumerator Warning()
    {
        if (HP > 0)
        {
            while (true)
            {
                yield return YieldInstructionCache.WaitForSeconds(0.7f);
                ImageComp.sprite = OTwoImage[HP - 1];
                MaterialComp.SetTexture("_MainTex", DangerTexture[1]);
                if (HP == 1 && Flag) Filter.enabled = true;
                yield return YieldInstructionCache.WaitForSeconds(0.7f);
                ImageComp.sprite = OTwoImage[HP];
                MaterialComp.SetTexture("_MainTex", DangerTexture[0]);
                if (HP == 1 && Flag) Filter.enabled = false;
            }
        }
    }
}