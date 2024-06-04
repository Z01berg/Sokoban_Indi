using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class History : MonoBehaviour
{
    public Stack<object> MoveHistory = new Stack<object>();
    public Stack<object> UndoHistory = new Stack<object>();
}

/*
 * Stack<(Vector3, bool)> vectorBoolStack = new Stack<(Vector3, bool)>();

// Dodawanie elementów do stosu
vectorBoolStack.Push((new Vector3(1, 2, 3), true));
vectorBoolStack.Push((new Vector3(4, 5, 6), false));

// Pobieranie elementów ze stosu
(Vector3 vector, bool flag) = vectorBoolStack.Pop();
 */
