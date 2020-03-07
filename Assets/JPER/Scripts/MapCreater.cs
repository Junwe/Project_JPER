using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapCreater : MonoBehaviour
{
    private const string JSON_PATH = "MapData";
    private const float GROUND_HEIGHT = .94f;
    private const float GROUND_WIDTH = .94f;

    [SerializeField]
    private string jsonFileName;

    [Header("Temporal Setting")]
    [SerializeField]
    private List<GameObject> groundPrefabs;
    [SerializeField]
    private GameObject portal;

    private StageData stageData = null;

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
        MapOffset = new Vector3(GROUND_WIDTH * stageData.map[0].row.Count - 1, GROUND_HEIGHT * stageData.map.Count - 1, 0) * 0.5f;

        for (int i = 0; i < stageData.map.Count; ++i)
        {
            for (int j = 0; j < stageData.map[i].row.Count; ++j)
            {
                if (stageData.map[i].row[j] == 0)
                    continue;

                float posX = GROUND_WIDTH * j - MapOffset.x;
                float posY = GROUND_HEIGHT * i;
                var temp = Instantiate(groundPrefabs[0], new Vector3(posX, posY, 0) + transform.position, Quaternion.identity, transform);

                var portalPos = new Vector2(j, i);
                if (portalPos == stageData.portalPosition)
                    Instantiate(portal, temp.transform);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (stageData == null)
            return;

        Gizmos.color = Color.green;
        var position = transform.position + (new Vector3(0, GROUND_HEIGHT * stageData.map.Count - 1, 0) * 0.5f);
        var size = new Vector3(GROUND_WIDTH * stageData.map[0].row.Count - 1, GROUND_HEIGHT * stageData.map.Count - 1, 0);
        Gizmos.DrawWireCube(position, size);
    }
}
