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
        dataNameText.text = GameManager.Instance.playerActionCounter.GetNameString(recordDataType) + " : ";
    }

    public void SetType(PlayerActionCounter.RecordDataType type)
    {
        recordDataType = type;
    }
}
