using UnityEngine;

public class DepthUI : MonoBehaviour
{
    public Transform BasePostion;
    public int FakeDepth;

    Transform TR;
    TextMesh TextComp;
    string Format;
    float BasePostionY;

    void Awake()
    {
        TR = GetComponent<Transform>();
        TextComp = GetComponent<TextMesh>();
        Format = TextComp.text;
    }

    void Start()
    {
        BasePostionY = BasePostion.position.y;
    }

    void Update()
    {
        int Pos = FakeDepth + Mathf.Max((int)(BasePostionY - TR.position.y) / 9, 0);
        TextComp.text = string.Format(Format, Pos);
    }
}