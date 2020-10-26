using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashEvent : MonoBehaviour
{
    [SerializeField]
    AudioSource trashaudio;
    [SerializeField]
    GameObject rightArm, leftArm;
    [SerializeField]
    GameObject subMarine;

    Transform This;
    public float Range = 3f;

    public int DumpCount = 10;
    public int currentCount;

    public GameObject[] ListOfTrashes;
    Vector3 Pos;
    //Vector3 WorldPos ;
    int ObjectIndex;
    GameObject Clone;

    // Start is called before the first frame update
    void Start()
    {
        This = transform;
    }
    IEnumerator StartTrashEvent()
    {
        trashaudio.Play();
        if (trashaudio.isPlaying)
        {
            yield return null;
        }
        rightArm.SetActive(true);
        leftArm.SetActive(true);
        yield break;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //이벤트를 시작하고
            StartCoroutine(StartTrashEvent());
            subMarine = other.gameObject;
            //이동을 멈춘다!!
            subMarine.GetComponent<CartMove>().enabled = false;
            StartCoroutine(Instance());
        }
    }

    private void OnDisable()
    {
        rightArm.SetActive(false);
        leftArm.SetActive(false);
        //다시 움직이게 하기
        subMarine.GetComponent<CartMove>().enabled = true;
    }
	

	public IEnumerator Instance()
	{
		currentCount = 0;

		while (currentCount < DumpCount)
		{
			Pos = This.TransformPoint(new Vector3(Random.Range(0, Range), Random.Range(0,Range), 0));
			//WorldPos = This.TransformPoint (Pos);
			//Pos = Pos + This.position;
			float R;
			R = Random.Range(0f, ListOfTrashes.Length - 0.6f);
			ObjectIndex = Mathf.RoundToInt(R);

			Clone = Instantiate(ListOfTrashes[ObjectIndex], Pos, Random.rotation);
			Clone.GetComponentInChildren<Trashes>().FloatLevel();

			currentCount++;

			yield return new WaitForSeconds(0.1f);
		}
	}

	public void BringInMore()
	{
		StartCoroutine(Instance());
	}

}
