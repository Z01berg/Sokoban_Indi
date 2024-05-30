using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIHistoryManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    private string _historyString;
    private Dictionary<Vector3, char> _directionSymbols = new Dictionary<Vector3, char>()
    {
        { Vector3.left, '◄' },
        { Vector3.right, '►' },
        { Vector3.up, '▲' },
        { Vector3.down, '▼' }
    };
    
    void Awake()
    {
        EventSystem.ChangeUIHistory.AddListener(CheckWhatDo);    
        EventSystem.ClearUIHistory.AddListener(ClearHistoryMoveUI);
    }

    private void CheckWhatDo(Vector3 direction, bool remove)
    {
        if (remove)
        {
            PopMove();
        }
        else
        {
            PushMove(direction);
        }
    }


    private void PushMove(Vector3 moveDirection)
    {
        if (_directionSymbols.ContainsKey(moveDirection))
        {
            //_historyString.Append(_directionSymbols[moveDirection]);//TODO: WTF?
            _historyString += _directionSymbols[moveDirection];
        }

        UpdateText(_historyString);
    }
    
    private void PopMove()
    {
        
        _historyString = _historyString.Remove(_historyString.Length - 1);

        UpdateText(_historyString);
    }

    private void ClearHistoryMoveUI()
    {
        _historyString = "";
    }
    
    private void UpdateText(string name)
    {
        _textMeshPro.text = name;
    }
}
