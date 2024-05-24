using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UILevelNameMAnager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    
    void Start()
    {
    EventSystem.ChangeUIName.AddListener(UpdateText);    
    }

    private void UpdateText(string name)
    {
        _textMeshPro.text = name;
    }
}
