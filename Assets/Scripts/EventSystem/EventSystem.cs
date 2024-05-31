using System;
using UnityEngine;
using UnityEngine.Events;

public static class EventSystem
{
    #region CreateNextLevel: WorldManager.cs -> LevelGenerator.cs

    public static UnityEvent CreateNextLevel = new UnityEvent();

    #endregion

    #region SpawnPlayer: LevelGenerator.cs -> WorldManager.cs

    public static UnityEvent<Vector3Int> SpawnPlayer = new UnityEvent<Vector3Int>();

    #endregion
    
    #region SpawnBox: LevelGenerator.cs -> WorldManager.cs

    public static UnityEvent<Vector3Int, int> SpawnBox = new UnityEvent<Vector3Int, int>();

    #endregion

    #region DeletePlayer: WorldManager.cs -> PlayerController.cs

    public static UnityEvent DeletePlayer = new UnityEvent();

    #endregion
    
    #region DeleteBox: WorldManager.cs -> BoxController.cs

    public static UnityEvent DeleteBox = new UnityEvent();

    #endregion

    #region AddTargetMax: LevelGenerator.cs -> WorldManager.cs

    public static UnityEvent<String> AddTargetMax = new UnityEvent<String>();

    #endregion

    #region ChangeValueTargetRGB: BoxController.cs -> WorldManager.cs

    public static UnityEvent<int, string> ChangeValueTargetRGB = new UnityEvent<int, string>();

    #endregion
    
    #region ChangeUITarget: WorldManager.cs -> UITargetManager.cs

    public static UnityEvent<string, string, string> ChangeUITarget = new UnityEvent<string, string, string>();

    #endregion
    
    #region ChangeUIName: WorldManager.cs -> UILevelNameManager.cs

    public static UnityEvent<string, string> ChangeUIName = new UnityEvent<string, string>();

    #endregion
    
    #region ChangeUIHistory: WorldManager.cs -> UITargetManager

    public static UnityEvent<Vector3, bool> ChangeUIHistory = new UnityEvent<Vector3, bool>();

    #endregion
    
    #region ClearUIHistory: PlayerController.cs -> UIHistoryManager.cs

    public static UnityEvent ClearUIHistory = new UnityEvent();

    #endregion
    
    #region FlagHistory: BoxController.cs -> UIHistoryManager.cs

    public static UnityEvent<string> FlagUIHistory = new UnityEvent<string>();

    #endregion
    
}

