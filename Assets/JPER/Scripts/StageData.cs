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

    //public List<string> groundNames;
    public List<Row> map;
    public Vector2 portalPosition;
}
