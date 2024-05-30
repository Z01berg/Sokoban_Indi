using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[HideInInspector] public enum TypeBox
{
    R,
    B,
    G
}

public class BoxController : MonoBehaviour
{
    [SerializeField] private GameObject _box;
    [SerializeField] private TypeBox _type;
    
    private Stack<Vector3> _moveHistoryBox = new Stack<Vector3>();
    private Stack<Vector3> _undoHistoryBox = new Stack<Vector3>();
    
    private bool _adedTarget = false;
    private bool _foundMatchingTile = false;

    private float _gridSize = 0.5f;

    private void Awake()
    {
        EventSystem.DeleteBox.AddListener(DeleteBox);
        ResolveWhichBox();
    }

    private void ResolveWhichBox()
    {
        if (transform.CompareTag("Box_R"))
        {
            _type = TypeBox.R;
        }
        else if (transform.CompareTag("Box_B"))
        {
            _type = TypeBox.B;
        }
        else
        {
            _type = TypeBox.G;
        }
    }

    public bool TryMoveBox(Vector2 direction)
    {
        Vector3 targetPosition = transform.position + new Vector3(direction.x * _gridSize, direction.y * _gridSize, 0);

        Collider2D[] colliders = Physics2D.OverlapBoxAll(targetPosition, new Vector2(_gridSize, _gridSize), 0);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Collision") || (collider.tag.StartsWith("Box") && collider.gameObject != gameObject))
            {
                Debug.Log("Collision Box");
                return false;
            }
        }
        Debug.Log($"BoxMOVE ▓ Current: {transform.position} ▓ Target:{targetPosition}");
        transform.position += new Vector3(direction.x * _gridSize * 2, direction.y * _gridSize * 2, 0);
        Debug.Log($"BoxMOVE_AFTER ▓ Current: {transform.position}");
                
        CheckTileUnder(targetPosition);
        return true;
    }

    private void CheckTileUnder(Vector3 previousCheck)
    {
        Vector2 checkSize = new Vector2(_gridSize / 2, _gridSize / 2);
        Collider2D[] currentTileColliders = Physics2D.OverlapBoxAll(previousCheck, checkSize, 0);
        Debug.Log($"BoxUNDER_CHECK ▓ Current: {previousCheck} ▓ Target:{checkSize}");

        bool needAdditionalCheck = false; // Flag to indicate if an additional check is needed

        foreach (Collider2D collider in currentTileColliders)
        {
            Debug.Log("Checking tile: " + collider.tag);
            if (collider.tag.EndsWith(_type.ToString()))
            {
                _foundMatchingTile = true;
                if (!_adedTarget)
                {
                    Debug.Log("Box on matching tile: " + collider.tag);
                    EventSystem.ChangeValueTargetRGB.Invoke(1, _type.ToString());
                    _adedTarget = true;
                }
                else
                {
                    needAdditionalCheck = true;
                }
            }
        }

        if (!_foundMatchingTile && _adedTarget)
        {
            EventSystem.ChangeValueTargetRGB.Invoke(-1, _type.ToString());
            _adedTarget = false;
        }

        if (needAdditionalCheck)
        {
            CheckTileUnder(previousCheck);
        }
    }

        
        private void DeleteBox()
        {
            Destroy(_box);
        }
}


