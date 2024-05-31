using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, PlayerControlls.IMovementActions, PlayerControlls.IHistoryStackActions
{
    [SerializeField] private GameObject _player;
    private float _gridSize = 0.5f;
    
    private Stack<Vector3> _moveHistoryPlayer = new Stack<Vector3>();
    private Stack<Vector3> _undoHistoryPlayer = new Stack<Vector3>();
    
    private PlayerControlls _controls;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        PrepareInputSystem();
        EventSystem.DeletePlayer.AddListener(DeletePlayer);
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void PrepareInputSystem()
    {
        _controls = new PlayerControlls();
        
        _controls.Movement.SetCallbacks(this);
        _controls.Movement.Enable();
        
        _controls.HistoryStack.SetCallbacks(this);
        _controls.HistoryStack.Enable();
    }

    #region Init Player Controls

    #region History

    public void OnUndo(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            UndoMove();
        }
    }
    
    public void OnRedo(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            RedoMove();
        }
    }

    #endregion

    #region Movement

    public void OnUp(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Move(Vector2.up);
            RotateSprite(Vector2.up);
        }
    }
    
    public void OnDown(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Move(Vector2.down);
            RotateSprite(Vector2.down);
        }
    }
    
    public void OnLeft(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Move(Vector2.left);
            RotateSprite(Vector2.left);
        }
    }
    
    public void OnRight(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Move(Vector2.right);
            RotateSprite(Vector2.right);
        }
    }

    #endregion
    
    #endregion

    private void Move(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) == 1f || Mathf.Abs(direction.y) == 1f)//TODO: może Debug.Break
        {
            Vector3 targetPosition = transform.position + new Vector3(direction.x * _gridSize, direction.y * _gridSize, 0);
            Debug.Log($"PLAYER ▓ Current: {transform.position} ▓ Target:{targetPosition}");
        
            Collider2D[] colliders = Physics2D.OverlapBoxAll(targetPosition, new Vector2(_gridSize, _gridSize), 0);
        
            bool canMove = true;
        
            foreach(Collider2D collider in colliders)
            {
                if(collider.CompareTag("Collision"))
                {
                    canMove = false;
                    break;
                }

                if (collider.CompareTag("Box_B") || collider.CompareTag("Box_R") || collider.CompareTag("Box_G"))
                {
                    BoxController box = collider.GetComponent<BoxController>();
                    if (box != null)
                    {
                        canMove = box.TryMoveBox(direction);
                    }
                }
            }
        
            if (canMove)
            {
                _moveHistoryPlayer.Push(transform.position);
                transform.position += new Vector3(direction.x * _gridSize * 2, direction.y * _gridSize * 2, 0);;
                EventSystem.ChangeUIHistory.Invoke(new Vector3(direction.x, direction.y, 0), false);
                _undoHistoryPlayer.Clear();
            }
        }
    }
    
    private void RotateSprite(Vector2 direction)
    {
            if (direction == Vector2.up)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                _spriteRenderer.flipX = false;
                _spriteRenderer.flipY = false;
            }
            else if (direction == Vector2.down)
            {
                transform.rotation = Quaternion.Euler(0, 0, -360);
                _spriteRenderer.flipX = false;
                _spriteRenderer.flipY = true;
            }
            else if (direction == Vector2.left)
            {
                transform.rotation = Quaternion.Euler(0, 0, 90);
                _spriteRenderer.flipX = false;
                _spriteRenderer.flipY = false;
            }
            else if (direction == Vector2.right)
            {
                transform.rotation = Quaternion.Euler(0, 0, -90);
                _spriteRenderer.flipX = true;
                _spriteRenderer.flipY = false;
            }
    }
    
    private void UndoMove()
    {
        if (_moveHistoryPlayer.Count > 0)
        {
            Vector3 previousPosition = transform.position;
            _undoHistoryPlayer.Push(previousPosition);
            transform.position = _moveHistoryPlayer.Pop();
            
            EventSystem.ChangeUIHistory.Invoke(transform.position, true);
        
            Vector2 direction = (transform.position - previousPosition).normalized * -1;
            RotateSprite(direction);
        }
    }

    private void RedoMove()
    {
        if (_undoHistoryPlayer.Count > 0)
        {
            Vector3 previousPosition = transform.position;
            _moveHistoryPlayer.Push(previousPosition);
            transform.position = _undoHistoryPlayer.Pop();
            
            Vector2 direction = (transform.position - previousPosition).normalized;
            EventSystem.ChangeUIHistory.Invoke(direction, false);
        
            RotateSprite(direction);
        }
    }

    private void ClearHistoryStack()
    {
        _moveHistoryPlayer.Clear();
        _undoHistoryPlayer.Clear();
    }

    private void DisableInputSystem()
    {
        _controls.Movement.Disable();
        _controls.HistoryStack.Disable();
    }
    
    // World Logic
    private void DeletePlayer()
    {
        DisableInputSystem();
        
        ClearHistoryStack();
        
        Destroy(_player);
    }
}
