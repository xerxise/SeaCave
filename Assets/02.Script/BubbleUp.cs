using UnityEngine;

public class BubbleUp : MonoBehaviour
{
    public float MinScale = 2.0f;
    public float MaxScale = 3.0f;
    public float MinusScale = 0.001f;
    public float averageUpdrift;

    public GameObject smallBubble;
    int smallBubbleCount;
    int maxSmallBubbleCount;

    float randomScale;
    float saveScale;
    Vector3 savePos;

    AQUAS_SmallBubbleBehaviour smallBubbleBehaviour;

    Transform Tr;

    void Awake()
    {
        Tr = GetComponent<Transform>();
    }

    void Start()
    {
        maxSmallBubbleCount = (int)Random.Range(10, 15);
        smallBubbleCount = 0;
        smallBubbleBehaviour = smallBubble.GetComponent<AQUAS_SmallBubbleBehaviour>();

        randomScale = Random.Range(MinScale, MaxScale);
        saveScale = randomScale;
        savePos = new Vector3(Tr.position.x, Tr.position.y, Tr.position.z);
    }

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * averageUpdrift, Space.World);

        SmallBubbleSpawner();

        Tr.localScale = new Vector3(randomScale, randomScale, randomScale);
        randomScale -= MinusScale;

        if (randomScale < 0.05f)
        {
            Tr.position = savePos;
            randomScale = saveScale;
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        Tr.position = savePos;
        randomScale = saveScale;
    }

    void SmallBubbleSpawner()
    {
        if (smallBubbleCount <= maxSmallBubbleCount)
        {
            smallBubble.transform.localScale = transform.localScale * Random.Range(0.6f, 0.8f);

            smallBubbleBehaviour.averageUpdrift = averageUpdrift * 0.5f;
           // smallBubbleBehaviour.waterLevel = waterLevel;
            //smallBubbleBehaviour.mainCamera = mainCamera;

            Instantiate(smallBubble, new Vector3(transform.position.x + Random.Range(-5f, 5f), transform.position.y - Random.Range(0.01f, 2), transform.position.z + Random.Range(-5f, 5f)), Quaternion.identity);

            smallBubbleCount += 1;
        }
    }
}