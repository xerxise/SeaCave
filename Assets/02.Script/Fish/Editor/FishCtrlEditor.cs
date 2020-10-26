using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(FishCtrl))]
public class FishCtrlEditor : Editor {

	private FishCtrl fishCtrl;
	private ReorderableList RlMoveObjects;

	private bool isMoveObject = true;

	private void OnEnable()
	{
		fishCtrl = (FishCtrl)target;

		#region MoveObjectReorderableList
		RlMoveObjects = new ReorderableList(fishCtrl.MoveObj, typeof(Transform), true, true, true, true);
		RlMoveObjects.drawHeaderCallback = (rect) =>
		{
			GUI.Label(rect, "Move Object : " + fishCtrl.MoveObj.Count);
		};
		RlMoveObjects.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
		{
			for (int cnt = 0; cnt < fishCtrl.MoveObj.Count; cnt++)
			{
				if (fishCtrl.MoveObj[cnt] == null)
					fishCtrl.MoveObj.RemoveAt(cnt);
			}

			if (fishCtrl.MoveObj.Count > index)
				fishCtrl.MoveObj[index] = (Transform)EditorGUI.ObjectField(rect, fishCtrl.MoveObj[index], typeof(Transform), true);
		};
		RlMoveObjects.onAddDropdownCallback = (Rect rect, ReorderableList rl) => {
			var menu = new GenericMenu();
            //하나씩 생산
			foreach (var obj in fishCtrl.MovePrefabs)
			{
				menu.AddItem(new GUIContent("Object/" + obj.name), false, () => 
				{
					GameObject temp = Instantiate(obj, fishCtrl.transform);
					if (temp.CompareTag("Untagged"))
					{
						temp.tag = "FISH";
						temp.layer = 13;
						temp.AddComponent<Rigidbody>().useGravity = false;
					}
					temp.transform.localPosition = Vector3.zero;
					fishCtrl.MoveObj.Add(temp.transform);
				});
			}
            //열개씩 생산
            foreach (var obj in fishCtrl.MovePrefabs)
            {
                menu.AddItem(new GUIContent("Object*10/" + obj.name + "*10"), false, () =>
                {
                    for (int cnt = 0; cnt < 10; cnt++)
                    {
                        GameObject temp = Instantiate(obj, fishCtrl.transform);
                        if (temp.CompareTag("Untagged"))
                        {
                            temp.tag = "FISH";
							temp.layer = 13;
							temp.AddComponent<Rigidbody>().useGravity = false;
                        }
                        temp.transform.localPosition = Vector3.zero;
                        fishCtrl.MoveObj.Add(temp.transform);
                    }
                });
            }
			//삼십개씩 생산
			foreach (var obj in fishCtrl.MovePrefabs)
			{
				menu.AddItem(new GUIContent("Object*30/" + obj.name + "*30"), false, () =>
				{
					for (int cnt = 0; cnt < 30; cnt++)
					{
						GameObject temp = Instantiate(obj, fishCtrl.transform);
						if (temp.CompareTag("Untagged"))
						{
							temp.tag = "FISH";
							temp.layer = 13;
							temp.AddComponent<Rigidbody>().useGravity = false;
						}
						temp.transform.localPosition = Vector3.zero;
						fishCtrl.MoveObj.Add(temp.transform);
					}
				});
			}
			//랜덤 열개씩 생산
			menu.AddItem(new GUIContent("Random*10"), false, () =>
			{
				for (int cnt = 0; cnt < 10; cnt++)
				{
					int random = Random.Range(0, fishCtrl.MovePrefabs.Length);
					GameObject temp = Instantiate(fishCtrl.MovePrefabs[random], fishCtrl.transform);
					if (temp.CompareTag("Untagged"))
					{
						temp.tag = "FISH";
						temp.layer = 13;
						temp.AddComponent<Rigidbody>().useGravity = false;
						}
					temp.transform.localPosition = Vector3.zero;
					fishCtrl.MoveObj.Add(temp.transform);
				}
			});
			menu.ShowAsContext();
		};
		RlMoveObjects.onRemoveCallback = (list) =>
		{
			if (fishCtrl.MoveObj[list.index] != null)
				DestroyImmediate(fishCtrl.MoveObj[list.index].gameObject);
			fishCtrl.MoveObj.RemoveAt(list.index);
		};
		#endregion MORL
	}

	public override void OnInspectorGUI()
	{
		GUILayout.BeginVertical("box");

		GUILayout.Space(5f);

		GUILayout.BeginHorizontal ();
		EditorGUILayout.LabelField ("이동속도 : ", GUILayout.Width(60));
		fishCtrl.MinSpeed = Mathf.Clamp(EditorGUILayout.FloatField(fishCtrl.MinSpeed, GUILayout.Width(45)), 0, fishCtrl.MaxSpeed);
		EditorGUILayout.MinMaxSlider(ref fishCtrl.MinSpeed, ref fishCtrl.MaxSpeed, 0, 10);
		fishCtrl.MaxSpeed = Mathf.Clamp(EditorGUILayout.FloatField(fishCtrl.MaxSpeed, GUILayout.Width(45)), fishCtrl.MinSpeed, 10);
		GUILayout.EndHorizontal ();
		GUILayout.Space(5f);

		GUILayout.BeginHorizontal ();
		EditorGUILayout.LabelField ("회전지속시간 : ", GUILayout.Width(80));
		fishCtrl.MinDelayRotate = Mathf.Clamp(EditorGUILayout.FloatField(fishCtrl.MinDelayRotate, GUILayout.Width(45)), 0, fishCtrl.MaxDelayRotate);
		EditorGUILayout.MinMaxSlider(ref fishCtrl.MinDelayRotate, ref fishCtrl.MaxDelayRotate, 0, 10);
		fishCtrl.MaxDelayRotate = Mathf.Clamp(EditorGUILayout.FloatField(fishCtrl.MaxDelayRotate, GUILayout.Width(45)), fishCtrl.MinDelayRotate, 10);
		GUILayout.EndHorizontal ();

		GUILayout.Space(5f);

		GUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("이동범위 : ", GUILayout.Width(60));
		fishCtrl.Size = EditorGUILayout.Vector3Field("", fishCtrl.Size);
		GUILayout.EndHorizontal();

		GUILayout.Space(5f);

		GUILayout.EndVertical();

		/*==================================================================================*/

		GUILayout.BeginHorizontal("box");
		EditorGUILayout.LabelField("", GUILayout.Width(7));
		serializedObject.Update();
		EditorGUILayout.PropertyField(serializedObject.FindProperty("MovePrefabs"), true);
		serializedObject.ApplyModifiedProperties();
		GUILayout.EndHorizontal();

		GUILayout.BeginVertical("box");
		GUILayout.BeginHorizontal();
		GUILayout.Space(12f);
		isMoveObject = EditorGUILayout.Foldout(isMoveObject, "MoveObjects");
		GUILayout.EndHorizontal();
		if (isMoveObject)
		{
			RlMoveObjects.DoLayoutList();
		}
		GUILayout.EndVertical();
	}
}
