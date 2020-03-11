using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapCreater : MonoBehaviour
{
    private const string JSON_PATH = "MapData";
    private const string GROUND_PATH = "Prefabs/Ground";
    private const string HUDDLE_PATH = "Prefabs/Huddle";
    private const float GROUND_HEIGHT = .94f;
    private const float GROUND_WIDTH = .94f;

    [SerializeField]
    private string jsonFileName;

    [Header("Temporal Setting")]
    [SerializeField]
    private GameObject portal;

    private StageData stageData = null;
    private List<GameObject> groundTable = null;
    private List<GameObject> huddleTable = null;

    public Vector3 MapOffset { get; private set; }

    private void Start()
    {
        string jsonString = string.Empty;

        using (StreamReader reader = new StreamReader(Path.Combine(Application.streamingAssetsPath, JSON_PATH, jsonFileName) + ".json"))
            jsonString = reader.ReadToEnd();

        if (string.IsNullOrEmpty(jsonString) == true)
        {
            Debug.LogError("MapCreater.Start() : JSON string is empty.");
            return;
        }

        stageData = JsonUtility.FromJson<StageData>(jsonString);

        groundTable = new List<GameObject>(stageData.grounds.Count);
        huddleTable = new List<GameObject>(stageData.huddles.Count);

        for (int i = 0;i <stageData.grounds.Count; ++i)
        {
            string path = Path.Combine(GROUND_PATH, stageData.grounds[i]);
            var prefab = Resources.Load<GameObject>(path);
            groundTable.Add(prefab);
        }

        for (int i = 0; i < stageData.huddles.Count; ++i)
        {
            string path = Path.Combine(HUDDLE_PATH, stageData.huddles[i]);
            var prefab = Resources.Load<GameObject>(path);
            huddleTable.Add(prefab);
        }

        MapOffset = new Vector3(GROUND_WIDTH * stageData.map[0].row.Count - 1, GROUND_HEIGHT * stageData.map.Count - 1, 0) * 0.5f;

        for (int i = 0; i < stageData.map.Count; ++i)
        {
            for (int j = 0; j < stageData.map[i].row.Count; ++j)
            {
                if (stageData.map[i].row[j] == 0)
                    continue;

                // 바닥 생성.
                int index = stageData.map[i].row[j] - 1;
                float posX = GROUND_WIDTH * j - MapOffset.x;
                float posY = GROUND_HEIGHT * i;
                var platform = Instantiate(groundTable[index], new Vector3(posX, posY, 0) + transform.position, Quaternion.identity, transform);

                var currentPositon = new Vector2(j, i);

                // 장애물 생성.
                for(int k = 0; k < stageData.huddleInfos.Count; ++k)
                {
                    var huddleInfo = stageData.huddleInfos[k];
                    if (currentPositon == huddleInfo.position)
                        Instantiate(huddleTable[huddleInfo.huddleIndex], platform.transform);
                }

                // 포탈 생성.
                if (currentPositon == stageData.portalPosition)
                    Instantiate(portal, platform.transform);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (stageData == null)
            return;

        // 생성된 맵의 사이즈를 시각적으로 표현.
        Gizmos.color = Color.green;
        var position = transform.position + (new Vector3(0, GROUND_HEIGHT * stageData.map.Count - 1, 0) * 0.5f);
        var size = new Vector3(GROUND_WIDTH * stageData.map[0].row.Count - 1, GROUND_HEIGHT * stageData.map.Count - 1, 0);
        Gizmos.DrawWireCube(position, size);
    }
}
