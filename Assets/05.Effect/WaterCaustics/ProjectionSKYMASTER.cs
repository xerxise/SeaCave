using UnityEngine;

public class ProjectionSKYMASTER : MonoBehaviour
{
    public Texture2D[] Caustics;
    public float fps = 30.0f;

    Material CausticProjector;
    float start_time;
    int currentFrame = 0;

    void Start()
    {
        CausticProjector = GetComponent<Projector>().material;
        start_time = Time.fixedTime;
    }

    void Update()
    {
        if (Time.fixedTime - start_time > (1.0f / fps))
        {
            CausticProjector.SetTexture("_CausticTexture", Caustics[currentFrame]);
            currentFrame++;
            if (currentFrame > 255)
            {
                currentFrame = 0;
            }
            start_time = Time.fixedTime;
        }
    }
}