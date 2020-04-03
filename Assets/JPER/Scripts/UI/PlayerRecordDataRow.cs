using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRecordDataRow : MonoBehaviour
{
    [SerializeField]
    private PlayerActionCounter.RecordDataType recordDataType;
    [SerializeField]
    private Text dataNameText;
    [SerializeField]
    private Text dataValueText;

    private PlayerActionCounter playerActionCounter = null;

    public void UpdateDataValue()
    {
        dataValueText.text = playerActionCounter.GetValueString(recordDataType);
    }

    private void Start()
    {
        dataNameText.text = recordDataType.ToString() + " : ";
        playerActionCounter = GameManager.Instance.playerActionCounter;
    }
}
