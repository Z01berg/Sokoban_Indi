using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeBox
{
    R,
    B,
    G
}

public class BoxController : MonoBehaviour
{
    [SerializeField] private GameObject _box;
    [SerializeField] private TypeBox _type;
    private bool _adedTarget = false;

    private float _gridSize = 0.5f;

    private void Start()
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

                transform.position += new Vector3(direction.x * _gridSize * 2, direction.y * _gridSize * 2, 0);
                
                CheckTileUnder();
                return true;
    }

    private void CheckTileUnder()
    {
        Collider2D[] currentTileColliders = Physics2D.OverlapBoxAll(new Vector2(transform.position.x - _gridSize / 2, transform.position.y - _gridSize / 2), new Vector2(_gridSize, _gridSize), 0);
        bool foundMatchingTile = false;
        foreach (Collider2D collider in currentTileColliders)
        {
            Debug.Log("Checking tile: " + collider.tag);
            if (collider.tag.EndsWith(_type.ToString()))
            {
                if (!_adedTarget && !foundMatchingTile)
                {
                    Debug.Log("Box on matching tile: " + collider.tag);
                    EventSystem.ChangeValueTargetRGB.Invoke(1, _type.ToString());
                    _adedTarget = true;
                    foundMatchingTile = true;
                    break;
                }
            }
        }

        if (!foundMatchingTile && _adedTarget)
        {
            EventSystem.ChangeValueTargetRGB.Invoke(-1, _type.ToString());
            _adedTarget = false;
        }
    }
        
        
        
        private void DeleteBox()
        {
            Destroy(_box);
        }
}


