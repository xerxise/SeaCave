using System.Collections;
using UnityEngine;
using DG.Tweening;

public class RandomColor : MonoBehaviour
{
    Color[] color = new Color[]
    {
        new Color(1, 0.5f, 1),
        new Color(1, 1, 0.5f),
        new Color(0.8f, 0.2f, 0.2f),
        new Color(1, 1, 1),
        new Color(0.5f, 0.5f, 1)
    };

    Material material;

    IEnumerator Start()
    {
        material = transform.GetChild(0).GetComponentInChildren<SkinnedMeshRenderer>().material;
        while (true)
        {
            yield return material.DOColor(color[Random.Range(0, color.Length)], 3.0f).WaitForCompletion();
        }
    }
}