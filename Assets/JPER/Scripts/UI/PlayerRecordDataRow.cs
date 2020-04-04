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

    public void UpdateDataValue()
    {
        dataValueText.text = GameManager.Instance.playerActionCounter.GetValueString(recordDataType);
    }

    private void Start()
    {
        dataNameText.text = recordDataType.ToString() + " : ";
    }
}
