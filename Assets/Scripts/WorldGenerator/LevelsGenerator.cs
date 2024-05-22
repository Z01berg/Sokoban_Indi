using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Levels : MonoBehaviour
{
    [SerializeField] private Tile Floor;
    [SerializeField] private RuleTile Wall;
    [SerializeField] private Tile Target_R;
    [SerializeField] private Tile Target_B;
    [SerializeField] private Tile Target_G;

    [SerializeField] private Tilemap _floor;
    [SerializeField] private Tilemap _wall;
    [SerializeField] private Tilemap _tR;
    [SerializeField] private Tilemap _tB;
    [SerializeField] private Tilemap _tG;
    
    private Vector3Int _position = new Vector3Int(0,0);
    private int _finishedLine = 0;
    
    public TextAsset filePath;
    StringReader read;
    string[] lines;
    private string _nameLevel;

    // variables
    private int _redBox = 1;
    private int _blueBox = 2;
    private int _greenBox = 3;

    private bool build = false;

    private void Start()
    {
        filePath = (TextAsset)Resources.Load ("Data/Levels");
        
        if (filePath != null)
        {
            read = new StringReader(filePath.text);
            lines = filePath.text.Split('\n');
            BuildLevelFromFile();
        }
        else
        {
            Debug.LogError("Failed to load Levels.txt from Resources/Data folder.");
        }
        
        EventSystem.CreateNextLevel.AddListener(BuildLevelFromFile);
    }

    private void Update()
    {
        // if (build)
        // {
        //     BuildLevelFromFile(_finishedLine);
        // }
    }

    // Builder
    private void BuildLevelFromFile()
    {
        _position.y = lines.Length - 1 - _finishedLine;
        
        bool shouldExit = false; 

        for (int i = _finishedLine; i < lines.Length && !shouldExit; i++)
        {
            string line = lines[i];
        
            for (int j = 0; j < line.Length; j++)
            {
                char symbol = line[j];
            
                switch (symbol)
                {
                    case '$':
                        _nameLevel = line.Substring(2);
                        Debug.Log($"Level: {_nameLevel}"); //TODO: UI Stuff?
                        break;
                    case '-':
                        // Empty space
                        break;
                    case '*':
                        PaintFloor(_position);
                        break;
                    case '#':
                        PaintWall(_position);
                        break;
                    case '@':
                        PaintFloor(_position);
                        EventSystem.SpawnPlayer.Invoke(_position);
                        break;
                    case '5':
                        PaintFloor(_position);
                        EventSystem.SpawnBox.Invoke(_position, _redBox);
                        break;
                    case '%':
                        PaintTR(_position);
                        EventSystem.AddTargetR.Invoke();
                        break;
                    case '6':
                        PaintFloor(_position);
                        EventSystem.SpawnBox.Invoke(_position, _blueBox);
                        break;
                    case '^':
                        PaintTB(_position);
                        break;
                    case '7':
                        PaintFloor(_position);
                        EventSystem.SpawnBox.Invoke(_position, _greenBox);
                        break;
                    case '&':
                        PaintTG(_position);
                        break;
                    case '/':
                        _finishedLine = i + 1;
                        shouldExit = true;
                        break;
                }

                _position.x++;
            }

            _position.x = 0;
            _position.y--;
        }

        _position = new Vector3Int(0, 0);
        build = false;
    }
    
    // Build logic
    void PaintWall(Vector3Int pos)//TODO:Metoda
    {
        _wall.SetTile(pos, Wall);
    }

    void PaintFloor(Vector3Int pos)
    {
        _floor.SetTile(pos, Floor);
    }
    
    void PaintTR(Vector3Int pos)
    {
        _tR.SetTile(pos, Target_R);
    }
    
    void PaintTB(Vector3Int pos)
    {
        _tB.SetTile(pos, Target_B);
    }
    
    void PaintTG(Vector3Int pos)
    {
        _tG.SetTile(pos, Target_G);
    }
    
    // Event
    private void ChangeBool()
    {
        build = true;
    }
}
