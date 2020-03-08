using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageData
{
    [System.Serializable]
    public struct Row
    {
        public List<int> row;
    }

    [System.Serializable]
    public struct HuddleInfo
    {
        public int huddleIndex;
        public Vector2 position;
    }

    public List<string> grounds;
    public List<Row> map;
    public Vector2 portalPosition;
    public List<string> huddles;
    public List<HuddleInfo> huddleInfos;
}
