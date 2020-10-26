using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;

public class TrashClean : MonoBehaviour
{
	[SerializeField]
	AirVRCameraRig _rig;
	[SerializeField]
	Transform rightHand, leftHand;
	[SerializeField]
	GameObject ClawFinger;
	[SerializeField]
	CCDIK ik;
	[SerializeField]
	Transform Target;
	[SerializeField]
	Transform PickOBJ;
	float weight;
	Vector3 Pos;
	Transform This;
	Vector3 PocketPos;
	bool IsPicking = false;
	public bool DeletePicked = false;
	[SerializeField]
	GameObject successIcon;
	

	bool CanPick;

	Ray ray;
	RaycastHit hit;
	[SerializeField]
	AudioSource ReachOutFX;
	public Camera FunctionCam;
	Animator Ani;
	[SerializeField]
	LayerMask layer;

	//Animator CrawFinggerAnimator ;
	//AnimatorStateInfo stateInfo;
	// Use this for initialization
	void Start()
	{
		This = transform;
		
		weight = 0f;
		PocketPos = This.localPosition;
		
		IsPicking = false;
		CanPick = true;
		//CrawFinggerAnimator = ClawFinger.gameObject.GetComponent<Animator> ();
	}

	void Update()
	{

		if (Input.GetKeyDown(KeyCode.C))
		{
			if (FunctionCam)
			{
				ray = FunctionCam.ScreenPointToRay(Input.mousePosition);
			}
			else
			{

				ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			}

			if (Physics.Raycast(ray, out hit, 300) && !IsPicking)
			{
				if (hit.collider)
				{
					if (hit.collider.gameObject.tag == "Linkable")
					{
						Ani = hit.collider.GetComponent<Animator>();
						StartCoroutine(Pick(hit.transform.root, Ani));
					}


					if (hit.collider.gameObject.layer == 5)
					{
						hit.collider.gameObject.BroadcastMessage("Do");
					}
				}
			}
		}
		if(AirVRInput.GetDown(_rig, AirVRInput.Button.RIndexTrigger))
        {
			ray = new Ray(rightHand.position, rightHand.forward);
			Physics.Raycast(ray, out hit, 50, layer);
            switch (hit.collider?.tag)
            {
				case "":
					StartCoroutine(Pick(hit.transform, Ani));
					break;
				default:

					break;
            }
        }
	}

	public void FoundPickTarget(Transform M, Animator A)
	{
		if (!IsPicking && CanPick)
		{
			M.gameObject.tag = "Finish";
			if (M != null)
				StartCoroutine(Pick(M, A));
		}
	}

	IEnumerator NoTaret()
	{
		while (weight > 0.01f)
		{
			weight = Mathf.Lerp(weight, 0f, Time.deltaTime * 3f);
			ik.solver.IKPositionWeight = weight;
			This.localPosition = Vector3.Lerp(This.localPosition, PocketPos, Time.deltaTime * 3f);
			yield return new WaitForSeconds(0.02f);
		}

		ik.solver.IKPositionWeight = 0f;
		StartCoroutine(Restore());

	}

	public IEnumerator Pick(Transform PickTarget, Animator anima)
	{
		if (CanPick && !IsPicking)
		{
			IsPicking = true;

			PickOBJ = PickTarget;
			ik.solver.target = PickTarget;
			ReachOutFX.Play();

			while (weight < 0.7f)
			{
				if (!PickTarget)
				{
					StartCoroutine(NoTaret());
				}

				weight = Mathf.Lerp(weight, 1.3f, Time.deltaTime * 3f);
				ik.solver.IKPositionWeight = weight;

				This.localPosition = Vector3.Lerp(This.localPosition, Vector3.zero, Time.deltaTime * 3f);

				yield return new WaitForSeconds(0.02f);
			}

			ik.solver.IKPositionWeight = 0.8f;
			

			yield return new WaitForSeconds(0.25f);

			if (!PickTarget)
			{
				StartCoroutine(NoTaret());
			}

			if (anima)
			{
				anima.enabled = false;

			}
			else
			{
				CanPick = false;
			}

			PickTarget.parent = ClawFinger.gameObject.transform;
			PickTarget.BroadcastMessage("Picked", SendMessageOptions.DontRequireReceiver);
			PickTarget.localPosition = new Vector3(0f, 0f, 0.7f);
			ik.solver.target = null;

			ReachOutFX.Play();

			while (weight > 0.01f)
			{
				if (!PickTarget)
				{
					StartCoroutine(NoTaret());
				}

				weight = Mathf.Lerp(weight, -0.1f, Time.deltaTime * 3f);
				ik.solver.IKPositionWeight = weight;
				This.localPosition = Vector3.Lerp(This.localPosition, PocketPos, Time.deltaTime * 3f);
				yield return new WaitForSeconds(0.02f);
			}

			ik.solver.IKPositionWeight = 0f;
			StartCoroutine(Restore());
		}
	}


	void EndMission()
	{
		CanPick = false;
	}

	public int PlayerInex = 1;
	IEnumerator Restore()
	{
		
		yield return new WaitForSeconds(0.25f);

		if (DeletePicked)
		{
			if (PickOBJ)
			{
				Destroy(PickOBJ.gameObject);

			}

		}
		else
		{
			PickOBJ.parent = null;
			Target.position = Pos;
		}

		IsPicking = false;
		
	}

}
