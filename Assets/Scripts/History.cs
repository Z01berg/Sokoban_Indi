using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class History : MonoBehaviour
{
    public Stack<object> MoveHistory = new Stack<object>();
    public Stack<object> UndoHistory = new Stack<object>();
}
