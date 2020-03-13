using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MapData", menuName = "MapData", order = 2)]
public class MapGridScriptbleObject : ScriptableObject
{
    public float width = 32f;
    public float height = 32f;

    public Color color = Color.white;

    public GameObject[] prefabsList;

}
