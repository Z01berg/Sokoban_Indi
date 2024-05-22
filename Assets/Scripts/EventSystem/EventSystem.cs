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

    #region AddTarget*: LevelGenerator.cs -> WorldManager.cs

    public static UnityEvent AddTargetR = new UnityEvent();
    public static UnityEvent AddTargetB = new UnityEvent();
    public static UnityEvent AddTargetG = new UnityEvent();

    #endregion

    #region ChangeValueTargetRGB: BoxController.cs -> WorldManager.cs

    public static UnityEvent<int, string> ChangeValueTargetRGB = new UnityEvent<int, string>();

    #endregion
}

