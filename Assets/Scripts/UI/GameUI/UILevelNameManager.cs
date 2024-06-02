using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UILevelNameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    [SerializeField] private TextMeshProUGUI _textMeshProRecord;
    
    void Awake()
    {
        EventSystem.ChangeUIName.AddListener(UpdateText);
    }

    private void UpdateText(string name, string record)
    {
        _textMeshPro.text = name;
        _textMeshProRecord.text = record;
    }
}
