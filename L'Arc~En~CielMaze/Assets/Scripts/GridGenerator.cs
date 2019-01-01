using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public int width, height, objectiveNumber, exitNumber;
    private Grid grid;
    public GameObject tilePrefab, wallPrefab;
    public Cell[,] generatedCells;
    public Cell playerCell;
    public Transform gridParent;
    public Material[] floorMaterials;
    private RainbowColors currentColor;

    private void Awake()
    {
        CodeControl.Message.AddListener<GenerateGridRequestEvent>(OnGenerateGridRequested);
        grid = new Grid();
    }

    private void OnGenerateGridRequested(GenerateGridRequestEvent obj)
    {
        currentColor = obj.level.color;
        generatedCells = grid.Generate(obj.level.width, obj.level.height);
        GenerateVisualGrid(obj.level.enemyAmount, obj.level.exitAmount, obj.level.objectiveAmount);
    }

    private void GenerateVisualGrid(int enemyAmount, int exitAmount, int objectiveAmount)
    {
        foreach (Cell cell in generatedCells)
        {
            GameObject floorTile = GameObject.CreatePrimitive(PrimitiveType.Plane);
            floorTile.transform.SetParent(gridParent);
            floorTile.transform.localPosition = new Vector3(cell.coordinates.x * 10, 0, cell.coordinates.y * 10);
            floorTile.name = string.Format("Tile {0}, {1}", cell.coordinates.x, cell.coordinates.y);
            floorTile.GetComponent<Renderer>().material = Instantiate(floorMaterials[(int)currentColor]);
            foreach (KeyValuePair<Direction, bool> wall in cell.walls)
            {
                if (wall.Value)
                {
                    GameObject wallForTile = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    wallForTile.transform.SetParent(floorTile.transform);
                    Vector3 position = wallForTile.transform.localPosition;
                    position.x = DirectionRelations.DirectionX[wall.Key] * 5;
                    position.y = 2;
                    position.z = DirectionRelations.DirectionY[wall.Key] * 5;
                    wallForTile.transform.localPosition = position;
                    Vector3 scale = wallForTile.transform.localScale;
                    scale.x = position.x == 0 ? 10 : 1;
                    scale.y = 4;
                    scale.z = position.z == 0 ? 10 : 1;
                    wallForTile.transform.localScale = scale;
                    wallForTile.name = string.Format("{0} Wall for Tile {1}, {2}", wall.Key.ToString(), cell.coordinates.x, cell.coordinates.y);
                }
            }
        }
        ChoosePlayerCell();
        GenerateEnemies(enemyAmount);
        GenerateExit(exitAmount);
        GenerateObjectives(objectiveAmount);
        DispatchGridGeneratedEvent();
        DispatchPlayerPlacementRequestEvent();
    }

    private void ChoosePlayerCell()
    {
        playerCell = grid.RandomCell(null, 0);
    }

    private void GenerateObjectives(int objectiveAmount)
    {
        for (int i = 0; i < objectiveAmount; i++)
        {
            Cell cell = grid.RandomCell(playerCell, 3);
            GameObject objective = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            objective.AddComponent<ObjectiveController>();
            objective.GetComponent<Renderer>().material = Instantiate(floorMaterials[(int)currentColor + 3]);
            objective.transform.SetParent(gridParent);
            objective.transform.localPosition = cell.SpawnOverCellLocalPosition(2);
        }
    }

    private void GenerateExit(int exitAmount)
    {
        for (int i = 0; i < exitAmount; i++)
        {
            Cell cell = grid.RandomCell(playerCell, 5);
            GameObject exit = GameObject.CreatePrimitive(PrimitiveType.Cube);
            exit.AddComponent<ExitController>();
            exit.GetComponent<Renderer>().material = Instantiate(floorMaterials[(int)currentColor + 3]);
            exit.transform.SetParent(gridParent);
            exit.transform.localPosition = cell.SpawnOverCellLocalPosition(2);
        }
    }

    private void GenerateEnemies(int enemyAmount)
    {
        for (int i = 0; i < enemyAmount; i++)
        {
            Cell cell = grid.RandomCell(playerCell, 3);
            GameObject enemy = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            enemy.AddComponent<EnemyController>().Initialize(new Enemy(1, EnemyType.Normal));
            enemy.GetComponent<Renderer>().material = Instantiate(floorMaterials[(int)currentColor + 3]);
            enemy.transform.SetParent(gridParent);
            enemy.transform.localPosition = cell.SpawnOverCellLocalPosition(2);
        }
    }

    private void DispatchPlayerPlacementRequestEvent()
    {
        CodeControl.Message.Send(new GeneratePlayerCharacterRequestEvent(playerCell, gridParent));
    }

    private void DispatchGridGeneratedEvent()
    {
        CodeControl.Message.Send(new GridGeneratedEvent(generatedCells, playerCell));
    }
}
