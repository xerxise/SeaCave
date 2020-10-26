using System.Collections.Generic;
using UnityEngine;

public class GroupFishCtrl : MonoBehaviour
{
    //움직임에 관한 모드
    public enum MoveMode { Lump, Circle, Wave }
    public MoveMode FishMoveMode = MoveMode.Lump;

    //"타겟 오브젝트"
    public Transform Target;

    //"생성된 오브젝트의 속도"
    public float FishSpeed;
    public float FishDrag;
    public float MinDistance;

    //"생성 설정"
    public GameObject[] SpawnPrefabs;
    public Vector3 SpawnSize;
    public int SpawnInt;

    public List<Transform> SpawnObjects;
    public FishCtrl fishCtrl;

    public int SpawnDir = 2;

    public void DestoryGroupFish()
    {
        for (int cnt = 0; cnt < SpawnObjects.Count; cnt++)
        {
            SpawnObjects[cnt].parent = fishCtrl.transform;
            Destroy(SpawnObjects[cnt].GetComponent<GroupFishWave>());
        }

        fishCtrl.MoveObj.AddRange(SpawnObjects);
        fishCtrl.MoveObj.Remove(transform);
        fishCtrl.ReStart();

        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        if (!FishMoveMode.Equals(MoveMode.Wave))
        {
            Gizmos.DrawWireCube(transform.position, SpawnSize); Vector3 dir;
            for (int cnt = 0; cnt < SpawnObjects.Count; cnt++)
            {
                dir = Target.position - SpawnObjects[cnt].transform.position;
                Debug.DrawRay(SpawnObjects[cnt].transform.position, dir, new Color(0.3f, 0.8f, 0.5f, 0.2f));
            }
        }
        else
        {
            if (SpawnDir.Equals(0))
                Gizmos.DrawWireCube(Target.position - Vector3.right * SpawnSize.x / 2, SpawnSize);
            else if (SpawnDir.Equals(1))
                Gizmos.DrawWireCube(Target.position - Vector3.up * SpawnSize.y / 2, SpawnSize);
            else
                Gizmos.DrawWireCube(Target.position - Vector3.forward * SpawnSize.z / 2, SpawnSize);
        }
    }
}
