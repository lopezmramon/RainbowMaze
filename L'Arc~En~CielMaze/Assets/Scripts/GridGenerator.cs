using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public int width, height, objectiveNumber, exitNumber;
    private Grid grid;
    public GameObject tilePrefab, wallPrefab;
    public Cell[,] generatedCells;
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
        currentColor = obj.color;
        generatedCells = grid.Generate(obj.width, obj.height);
        GenerateVisualGrid(obj.enemyAmount, obj.exitAmount, obj.objectiveAmount);
    }

    private void GenerateVisualGrid(int enemyAmount, int exitAmount, int objectiveAmount)
    {
        foreach (Cell cell in generatedCells)
        {
            GameObject floorTile = GameObject.CreatePrimitive(PrimitiveType.Plane);
            floorTile.transform.SetParent(gridParent);
            floorTile.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            floorTile.transform.localPosition = new Vector3(cell.coordinates.x, 0, cell.coordinates.y);
            floorTile.name = string.Format("Tile {0}, {1}", cell.coordinates.x, cell.coordinates.y);
            floorTile.GetComponent<Renderer>().material = Instantiate(floorMaterials[(int)currentColor]);
            foreach (KeyValuePair<Directions, bool> wall in cell.walls)
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
        GenerateEnemies(enemyAmount);
        GenerateExit(exitAmount);
        GenerateObjectives(objectiveAmount);
    }

    private void GenerateObjectives(int objectiveAmount)
    {
        for (int i = 0; i < objectiveAmount; i++)
        {

        }
    }

    private void GenerateExit(int exitAmount)
    {
        for (int i = 0; i < exitAmount; i++)
        {

        }
    }

    private void GenerateEnemies(int enemyAmount)
    {
        for (int i = 0; i < enemyAmount; i++)
        {

        }
    }


}
