using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class CopyStage : EditorWindow
{
    public GameObject rootObject;

    public GameObject[] mapObjectList;


    [MenuItem("test/test")]
    public static void CreateWindow()
    {
        CopyStage window = (CopyStage)GetWindow(typeof(CopyStage));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.BeginVertical();
        GUILayout.Label("스테이지 프리팹 만들기", EditorStyles.boldLabel);

        rootObject = (GameObject)EditorGUILayout.ObjectField("root Object", rootObject, typeof(GameObject));

        ScriptableObject scriptableObj = this;
        SerializedObject serialObj = new SerializedObject(scriptableObj);
        SerializedProperty serialProp = serialObj.FindProperty("mapObjectList");

        EditorGUILayout.PropertyField(serialProp, true);
        serialObj.ApplyModifiedProperties();

        if (GUILayout.Button("만들기"))
        {
            CreateNewPrefab();
        }

        GUILayout.EndVertical();
    }

    private void CreateNewPrefab()
    {
        Transform[] childs = rootObject.GetComponentsInChildren<Transform>();
        List<Transform> childList = new List<Transform>();
        childList.AddRange(childs);
        childList.Remove(rootObject.transform); // 본인은 삭제

        List<Transform> deleteList = new List<Transform>();

        foreach (var child in childList)
        {
            if (!child.parent.Equals(rootObject.transform))
            {
                deleteList.Add(child);
            }
        }
        foreach (var child in deleteList)
        {
            childList.Remove(child);
        }
        // 부모가 rootobject가아니면 맵 오브젝트의 하위 자식. 필요없음

        foreach (var orgObject in childList)
        {
            GameObject createObj = null;
            for (int i = 0; i < mapObjectList.Length; ++i)
            {
                if (orgObject.name.Contains(mapObjectList[i].name))
                {
                    createObj = mapObjectList[i];
                    break;
                }
            }
            if (createObj == null)
            {
                Debug.Log("createObj is null");
            }
            GameObject createedObject = (GameObject)PrefabUtility.InstantiatePrefab(createObj);
            createedObject.transform.parent = rootObject.transform;
            createedObject.transform.position = orgObject.transform.position;

            Spike spike = createedObject.GetComponent<Spike>();
            MovingTrap moving = createedObject.GetComponent<MovingTrap>();

            if (spike != null)
            {
                spike.ValueCopy(orgObject.GetComponent<Spike>());
            }
            if (moving != null)
            {
                moving.ValueCopy(orgObject.GetComponent<MovingTrap>());
            }
        }

    }
}
