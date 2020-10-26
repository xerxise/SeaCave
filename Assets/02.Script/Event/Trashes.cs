using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trashes : MonoBehaviour
{
	Transform This;
	float FLoatLevel;
	[SerializeField]
	Animator Ani;
	bool IsPicked;
	TrashEvent trashCon;
	
	// Use this for initialization

	Vector3 TempPos;

    private void Start()
    {
		trashCon = FindObjectOfType<TrashEvent>();
		FloatLevel();
    }
    // Update is called once per frame
    void LateUpdate()
	{
		if (!IsPicked)
		{
			This.position = Vector3.Lerp(This.position, TempPos, Time.deltaTime * 2);
		}
        else
        {
			This.position = Vector3.Lerp(This.position, Player.position, Time.deltaTime * 2);
			if((This.position-Player.position).magnitude<1)
            {
				Destroy(transform.root.gameObject);
            }
        }
	}


	//void Picked (float Level)
	public void Picked()
	{
		if (IsPicked)
			return;

		IsPicked = true;
		Ani.enabled = false;

	}

	Transform Player;

	public void FloatLevel()
	{
		Player = GameObject.FindWithTag("Player").transform;
		//print (Player.position);

		This = transform.root;
		Ani = GetComponent<Animator>();
		IsPicked = false;

		TempPos = This.position;
		TempPos.y = Player.position.y + Random.Range(-2f, 2f);
	}

    private void OnDestroy()
    {
		trashCon.currentCount--;
        if (trashCon.currentCount <= 0)
        {
			trashCon.gameObject.SetActive(false);
        }
    }
}
