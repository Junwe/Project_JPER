using System.Collections;
using System.Collections.Generic;

public class StageData
{
    [System.Serializable]
    public struct Row
    {
        public List<int> row;
    }

    //public List<string> groundNames;
    public List<Row> map; //int[,] map;
}
