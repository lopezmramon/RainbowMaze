using System;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public int width, height, objectiveNumber, exitNumber;
    private Grid grid;
    public Cell[,] generatedCells;
    public Cell playerCell;
    public Transform gridParent;
    public Material[] floorMaterials;
    public Material enemyMaterial;
    public Material exitMaterial;
    private RainbowColor currentColor;
    public GameObject[] enemyPrefabs;

    private void Awake()
    {
        CodeControl.Message.AddListener<GenerateGridRequestEvent>(OnGenerateGridRequested);
        CodeControl.Message.AddListener<LevelCompleteEvent>(OnLevelCompleted);
        grid = new Grid();
    }

    private void OnLevelCompleted(LevelCompleteEvent obj)
    {
        foreach(Transform child in gridParent)
        {
            Destroy(child.gameObject);
        }
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
            floorTile.layer = LayerMask.NameToLayer("Floor");
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
                    wallForTile.layer = LayerMask.NameToLayer("Wall");
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
            cell.occupied = true;
            GameObject objective = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            objective.name = "Objective";
            objective.AddComponent<ObjectiveController>();
            objective.GetComponent<Renderer>().material = Instantiate(floorMaterials[(int)currentColor]);
            objective.transform.SetParent(gridParent);
            objective.transform.localPosition = cell.SpawnOverCellLocalPosition(0, 2, 0);
            objective.transform.localScale *= 2;
        }
    }

    private void GenerateExit(int exitAmount)
    {
        for (int i = 0; i < exitAmount; i++)
        {
            Cell cell = grid.RandomCell(playerCell, 5);
            GameObject exit = GameObject.CreatePrimitive(PrimitiveType.Cube);
            exit.name = "Exit";
            cell.occupied = true;
            exit.AddComponent<ExitController>();
            exit.GetComponent<Renderer>().material = Instantiate(exitMaterial);
            exit.transform.SetParent(gridParent);
            exit.transform.localPosition = cell.SpawnOverCellLocalPosition(0, 2, 0);
            exit.transform.localScale *= 2;
        }
    }

    private void GenerateEnemies(int enemyAmount)
    {
        for (int i = 0; i < enemyAmount; i++)
        {
            Cell cell = grid.RandomCell(playerCell, 3);
            cell.occupied = true;
            GameObject enemy = Instantiate(enemyPrefabs[UnityEngine.Random.Range(0, enemyPrefabs.Length)]);
            enemy.name = "Enemy";
            enemy.layer = LayerMask.NameToLayer("Obstacle");
            enemy.AddComponent<EnemyController>().Initialize(new Enemy(UnityEngine.Random.Range(1, 4), EnemyType.Normal));
            enemy.transform.SetParent(gridParent);
            enemy.transform.localPosition = cell.SpawnOverCellLocalPosition(0, 3, 4);

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
