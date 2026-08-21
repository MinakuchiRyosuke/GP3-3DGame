using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [SerializeField] private GameObject floorPrefab;
    [SerializeField] private Transform player;

    [SerializeField] private int floorCount;

    private Queue<GameObject> floorObjects = new Queue<GameObject>();// 後入れ先出しの配列

    private float floorLength;
    private float nextFloorZ;

    private void Awake()
    {
        // 床の長さを取得
        floorLength = floorPrefab.GetComponent<Renderer>().bounds.size.z;
        CreateFloor();
    }

    private void Update()
    {
        CheckFloor();
    }

    //最初に床を生成
    private void CreateFloor()
    {
        for (int i = 0; i < floorCount; i++)
        {
            GameObject floor = Instantiate(
                floorPrefab,
                new Vector3(0f, 0f, nextFloorZ),
                Quaternion.Euler(0f, 90f, 0f)
                );

            floorObjects.Enqueue( floor );

            nextFloorZ += floorLength;
        }
    }

    //プレイヤーの位置を判定
    private void CheckFloor()
    {
        if (player.position.z > nextFloorZ -  floorLength * (floorObjects.Count - 1))
        {
            GetFloor();
        }
    }

    //古い床をキューから出して移動させてキューに入れる
    private void GetFloor()
    {
        GameObject usedFloor = floorObjects.Dequeue();

        usedFloor.transform.position = new Vector3(0f, 0f, nextFloorZ);

        floorObjects.Enqueue(usedFloor);

        nextFloorZ += floorLength;
    }
}
