using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GroupFishCtrl))]
public class GroupFishCtrlEditor : Editor
{

	private GroupFishCtrl groupFishCtrl;

	private SerializedProperty Mode;
	private SerializedProperty Target;
	private SerializedProperty FishSpeed;
	private SerializedProperty FishDrag;
	private SerializedProperty MinDistance;
	private SerializedProperty SpawnPrefabs;
	private SerializedProperty SpawnSize;
	private SerializedProperty SpawnInt;
	private SerializedProperty FishCtrl;

	private void OnEnable()
	{
		groupFishCtrl = (GroupFishCtrl)target;

		Mode = serializedObject.FindProperty("FishMoveMode");
		Target = serializedObject.FindProperty("Target");
		FishSpeed = serializedObject.FindProperty("FishSpeed");
		FishDrag = serializedObject.FindProperty("FishDrag");
		MinDistance = serializedObject.FindProperty("MinDistance");
		SpawnPrefabs = serializedObject.FindProperty("SpawnPrefabs");
		SpawnSize = serializedObject.FindProperty("SpawnSize");
		SpawnInt = serializedObject.FindProperty("SpawnInt");
		FishCtrl = serializedObject.FindProperty("fishCtrl");
	}

	public override void OnInspectorGUI()
	{
		GUILayout.BeginVertical();
		serializedObject.Update();
		GUILayout.BeginVertical("box");
		EditorGUILayout.LabelField("움직임 모드");
		EditorGUILayout.PropertyField(Mode);
		GUILayout.EndVertical();

		GUILayout.Space(5f);

		GUILayout.BeginVertical("box");
		EditorGUILayout.LabelField("타겟 오브젝트");
		EditorGUILayout.PropertyField(Target);
		GUILayout.EndVertical();


		GUILayout.Space(5f);

		GUILayout.BeginVertical("box");
		EditorGUILayout.LabelField("생성된 오브젝트의 속도");
		EditorGUILayout.PropertyField(FishSpeed);
		if (!groupFishCtrl.FishMoveMode.Equals(GroupFishCtrl.MoveMode.Wave))
		{
			EditorGUILayout.PropertyField(FishDrag);
			EditorGUILayout.PropertyField(MinDistance);
		}
		GUILayout.EndVertical();

		GUILayout.Space(5f);

		GUILayout.BeginVertical("box");
		EditorGUILayout.LabelField("생성 설정");
		GUILayout.BeginHorizontal();
		GUILayout.Space(11f);
		EditorGUILayout.PropertyField(SpawnPrefabs, true);
		GUILayout.EndHorizontal();
		EditorGUILayout.PropertyField(SpawnSize);
		EditorGUILayout.PropertyField(SpawnInt);
		GUILayout.EndVertical();

		GUILayout.Space(5f);

		GUILayout.BeginHorizontal("box");
		EditorGUILayout.PropertyField(FishCtrl);
		if (GUILayout.Button("그룹해제"))
		{
			groupFishCtrl.DestoryGroupFish();
		}
		GUILayout.EndHorizontal();

		serializedObject.ApplyModifiedProperties();

		GUILayout.Space(5f);

		groupFishCtrl.SpawnDir = GUILayout.Toolbar(groupFishCtrl.SpawnDir, new string[] { "X축", "Y축", "Z축" });

		GUILayout.BeginHorizontal();
		if (GUILayout.Button("생성"))
		{
			if (groupFishCtrl.FishMoveMode.Equals(GroupFishCtrl.MoveMode.Wave))
			{
				Transform[] obj = groupFishCtrl.Target.GetComponentsInChildren<Transform>();
				for (int cnt = 1; cnt < obj.Length; cnt++)
				{
					DestroyImmediate(obj[cnt].gameObject);
				}
			}

            for (int cnt = 0; cnt < groupFishCtrl.SpawnObjects.Count; cnt++)
			{
				DestroyImmediate(groupFishCtrl.SpawnObjects[cnt].gameObject);
			}
			groupFishCtrl.SpawnObjects.Clear();
			
			//오브젝트 생성
			for (int cnt = 0; cnt < groupFishCtrl.SpawnInt; cnt++)
			{
				Vector3 point = new Vector3(Random.Range(-groupFishCtrl.SpawnSize.x / 2, groupFishCtrl.SpawnSize.x / 2),
											Random.Range(-groupFishCtrl.SpawnSize.y / 2, groupFishCtrl.SpawnSize.y / 2),
											Random.Range(-groupFishCtrl.SpawnSize.z / 2, groupFishCtrl.SpawnSize.z / 2));
				if (!groupFishCtrl.FishMoveMode.Equals(GroupFishCtrl.MoveMode.Wave))
				{
					groupFishCtrl.SpawnObjects.Add(Instantiate(groupFishCtrl.SpawnPrefabs[Random.Range(0, groupFishCtrl.SpawnPrefabs.Length)], groupFishCtrl.transform).transform);
					groupFishCtrl.SpawnObjects[cnt].gameObject.AddComponent<GroupFishMove>().Target = groupFishCtrl.Target;
					GroupFishMove gfm = groupFishCtrl.SpawnObjects[cnt].GetComponent<GroupFishMove>();
					gfm.isLump = groupFishCtrl.FishMoveMode.Equals(GroupFishCtrl.MoveMode.Lump) ? true : false;
					gfm.Speed = groupFishCtrl.FishSpeed;
					gfm.MinDistance = groupFishCtrl.MinDistance;
					groupFishCtrl.SpawnObjects[cnt].gameObject.AddComponent<Rigidbody>().useGravity = false;
					groupFishCtrl.SpawnObjects[cnt].GetComponent<Rigidbody>().drag = groupFishCtrl.FishDrag;
				}
				else
				{
					groupFishCtrl.SpawnObjects.Add(Instantiate(groupFishCtrl.SpawnPrefabs[Random.Range(0, groupFishCtrl.SpawnPrefabs.Length)], groupFishCtrl.transform).transform);
					if (groupFishCtrl.SpawnDir.Equals(0))
						point += new Vector3(groupFishCtrl.Target.position.x - (groupFishCtrl.SpawnSize.x / 2), groupFishCtrl.Target.position.y, groupFishCtrl.Target.position.z);
					else if (groupFishCtrl.SpawnDir.Equals(1))
						point += new Vector3(groupFishCtrl.Target.position.x, groupFishCtrl.Target.position.y - (groupFishCtrl.SpawnSize.y / 2), groupFishCtrl.Target.position.z);
					else
						point += new Vector3(groupFishCtrl.Target.position.x, groupFishCtrl.Target.position.y, groupFishCtrl.Target.position.z - (groupFishCtrl.SpawnSize.z / 2));
					GameObject obj = new GameObject("target[" + cnt + "]");
					obj.transform.parent = groupFishCtrl.Target;
					obj.transform.position = point;
					groupFishCtrl.SpawnObjects[cnt].gameObject.AddComponent<GroupFishWave>().Target = obj.transform;
					groupFishCtrl.SpawnObjects[cnt].GetComponent<GroupFishWave>().Speed = groupFishCtrl.FishSpeed;
					groupFishCtrl.SpawnObjects[cnt].gameObject.AddComponent<Rigidbody>().useGravity = false;
				}
				groupFishCtrl.SpawnObjects[cnt].tag = "FISH";
				groupFishCtrl.SpawnObjects[cnt].gameObject.layer = 13;
				groupFishCtrl.SpawnObjects[cnt].transform.position = point;
			}
		}

		if (GUILayout.Button("리셋"))
		{
			if (groupFishCtrl.FishMoveMode.Equals(GroupFishCtrl.MoveMode.Wave))
			{
				Transform[] obj = groupFishCtrl.Target.GetComponentsInChildren<Transform>();
				for (int cnt = 1; cnt < obj.Length; cnt++)
				{
					DestroyImmediate(obj[cnt].gameObject);
				}
			}

			for (int cnt = 0; cnt < groupFishCtrl.SpawnObjects.Count; cnt++)
			{
				DestroyImmediate(groupFishCtrl.SpawnObjects[cnt].gameObject);
			}
			groupFishCtrl.SpawnObjects.Clear();
		}
		GUILayout.EndHorizontal();

		GUILayout.EndVertical();
	}
}
