using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIHistoryManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    [SerializeField] private TextMeshProUGUI _textMeshProCount;

    private Stack<string> _historyStack = new Stack<string>();
    
    private Dictionary<Vector3, string> _directionSymbols = new Dictionary<Vector3, string>()
    {
        { Vector3.left, "◄" },
        { Vector3.right, "►" },
        { Vector3.up, "▲" },
        { Vector3.down, "▼" }
    };

    private void Awake()
    {
        EventSystem.ChangeUIHistory.AddListener(CheckWhatDo);
        EventSystem.ClearUIHistory.AddListener(ClearHistoryMoveUI);
        EventSystem.FlagUIHistory.AddListener(ColorIt);
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
            string moveSymbol = _directionSymbols[moveDirection];
            _historyStack.Push(moveSymbol);
        }

        UpdateText();
    }
    
    private void PushMove(Vector3 moveDirection, string flag)
    {
        if (_directionSymbols.ContainsKey(moveDirection))
        {
            string moveSymbol = _directionSymbols[moveDirection];
            _historyStack.Push(moveSymbol);
        }

        if (_historyStack.Count > 0)
        {
            string lastMove = _historyStack.Pop();

            switch (flag)
            {
                case "R":
                    _historyStack.Push("<color=red>" + lastMove + "</color>");
                    break;
                case "G":
                    _historyStack.Push("<color=green>" + lastMove + "</color>");
                    break;
                default:
                    _historyStack.Push("<color=blue>" + lastMove + "</color>");
                    break;
            }
        }

        UpdateText();
    }

    private void PopMove()
    {
        if (_historyStack.Count > 0)
        {
            _historyStack.Pop();
            UpdateText();
        }
    }

    private void ColorIt(string flag)
    {
        if (_historyStack.Count > 0)
        {
            string lastMove = _historyStack.Pop();
            
            switch (flag)
            {
                case "R":
                    _historyStack.Push("<color=red>" + lastMove + "</color>");
                    break;
                case "G":
                    _historyStack.Push("<color=green>" + lastMove + "</color>");
                    break;
                default:
                    _historyStack.Push("<color=blue>" + lastMove + "</color>");
                    break;
            }
            
            UpdateText();
        }
    }

    private void ClearHistoryMoveUI()
    {
        _historyStack.Clear();
        UpdateText();
    }

    private void UpdateText()
    {
        string[] historyArray = _historyStack.ToArray();
        System.Array.Reverse(historyArray);
        _textMeshPro.text = string.Join("", historyArray);
        _textMeshProCount.text = CountTurns().ToString();
    }

    private int CountTurns()//TODO: Save to File
    {
        return _historyStack.Count;
    }
}
