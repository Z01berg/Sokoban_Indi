using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WorldManager : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    private GameObject _instantiatedPlayer;
    
    [SerializeField] private GameObject _box_R;
    private GameObject _instantiatedBoxR;
    
    [SerializeField] private GameObject _box_G;
    private GameObject _instantiatedBoxG;
    
    [SerializeField] private GameObject _box_B;
    private GameObject _instantiatedBoxB;
    
    private Vector2 _gridSize = new Vector2(1f, 1f);

    #region Debug World Info
    
    [Header("Max Count Box")]
    [SerializeField] private int _targetRedInWorld = 0;
    [SerializeField] private int _targetGreenInWorld = 0;
    [SerializeField] private int _targetBlueInWorld = 0;
    
    [Header("Current Box")]
    [SerializeField] private int _targetRed = 0;
    [SerializeField] private int _targetGreen = 0;
    [SerializeField] private int _targetBlue = 0;
    
    #endregion
        
    void Start()//TODO: Transform History Player && Box
    {
        InitEvents();
        DoBackupPrefabs();
    }

    private void InitEvents()
    {
        // Player
        EventSystem.SpawnPlayer.AddListener(SpawnPlayer);
        
        // Box
        EventSystem.SpawnBox.AddListener(SpawnBox);
        
        // Box Logic
        EventSystem.AddTargetR.AddListener(AddMaxRed);
        EventSystem.AddTargetB.AddListener(AddMaxBlue);
        EventSystem.AddTargetG.AddListener(AddMaxGreen);
        
        EventSystem.ChangeValueTargetRGB.AddListener(AddValueToColor);
    }

    private void DoBackupPrefabs()
    {
        _instantiatedPlayer = _player;
        _instantiatedBoxR = _box_R;
        _instantiatedBoxB = _box_B;
        _instantiatedBoxG = _box_G;
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.P))//TODO: Debug
        {
            Reset();
            EventSystem.CreateNextLevel.Invoke();
        }
        
        if (CheckTargets()) //TODO Why still deliting ref?
        {
            Reset();
            EventSystem.CreateNextLevel.Invoke();
        }
    }

    // Box Spawn
    private void SpawnBox(Vector3Int here, int kind)
    {
        Vector2 spawnPosition = new Vector2(here.x + 0.5f, here.y + 0.5f);
        
        spawnPosition.x *= _gridSize.x;
        spawnPosition.y *= _gridSize.y;

        if (kind == 1)
        {
            _box_R = Instantiate(_box_R, spawnPosition, Quaternion.identity);
        }
        else if (kind == 2)
        {
            _box_B = Instantiate(_box_B, spawnPosition, Quaternion.identity);
        }
        else
        {
            _box_G = Instantiate(_box_G, spawnPosition, Quaternion.identity);
        }
        
    }
    
    // Player Spawn
    private void SpawnPlayer(Vector3Int here)
    {
        Vector2 spawnPosition = new Vector2(here.x + 0.5f, here.y + 0.5f);
        
        spawnPosition.x *= _gridSize.x;
        spawnPosition.y *= _gridSize.y;

        _player = Instantiate(_player, spawnPosition, Quaternion.identity);
    }

    // Box logic
    private void AddMaxRed()
    {
        _targetRedInWorld++;
    }
    
    private void AddMaxGreen()
    {
        _targetGreenInWorld++;
    }
    
    private void AddMaxBlue()
    {
        _targetBlueInWorld++;
    }

    private void AddValueToColor(int change, string flag)
    {
        if (flag == "R")
        {
            _targetRed += change;
        }
        else if (flag == "G")
        {
            _targetGreen += change;
        }
        else
        {
            _targetBlue += change;
        }
    }

    // Reset level
    private bool CheckTargets()
    {
        if (_targetBlueInWorld == _targetBlue && _targetRedInWorld == _targetRed && _targetGreenInWorld == _targetGreen)
        {
            return true;
        }

        return false;
    }

    private void Reset()
    {
        EventSystem.DeletePlayer.Invoke();
        _player = _instantiatedPlayer;
        
        EventSystem.DeleteBox.Invoke();
        _box_R = _instantiatedBoxR;
        _box_G = _instantiatedBoxG;
        _box_B = _instantiatedBoxB;
        
        _targetGreen = 0;
        _targetBlue = 0;
        _targetRed = 0;

        _targetBlueInWorld = 0;
        _targetGreenInWorld = 0;
        _targetRedInWorld = 0;
    }
}
