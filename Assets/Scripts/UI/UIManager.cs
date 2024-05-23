using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class UIManager : MonoBehaviour
{
    [SerializeField] private string _flag;
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    
    private string _max = "0";
    private string _current = "0";
    
    
    void Start()
    {
        EventSystem.ChangeUI.AddListener(ChangeUI);
    }

    private void ChangeUI(string current, string max, string flag)
    {
        if (_flag == flag && current != "x")
        {
            _current = current;
        }
        
        if (_flag == flag && max != "x")
        {
            _max = max;
        }

        UpdateText();
    }

    private void UpdateText()
    {
            _textMeshPro.text = $"{_current} / {_max}";
    }
    
}
