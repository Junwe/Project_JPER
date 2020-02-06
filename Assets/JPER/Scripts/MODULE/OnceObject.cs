using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnceObject : MonoBehaviour
{
    public GameObject[] DontDesotryObject;

    public static bool isOnce = false;   
    private void Awake() 
    {
        if(!OnceObject.isOnce)
        {
            for (int i = 0; i < DontDesotryObject.Length; ++i)
                Instantiate(DontDesotryObject[i]);
            OnceObject.isOnce = true;
        }
    }

}
