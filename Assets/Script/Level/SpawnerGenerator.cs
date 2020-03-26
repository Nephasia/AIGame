using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SpawnerGenerator
{
    Vector2 gridSize;
    int amount = 4;
    int minBorderMargin = 1;
    int minMarginBetween = 1;

    public List<Spawner> SpawnPoints { get; private set; } = new List<Spawner>();
    public List<Obstacle> Obstacles { get; set; } = new List<Obstacle>();

    public SpawnerGenerator(Vector2 gridSize, List<Obstacle> obstacles)
    {
        this.gridSize = new Vector2(gridSize.x, gridSize.y);
        Obstacles = obstacles;
    }

    public void CreateObstacles()
    {

        for (int i = 0; i < amount; i++)
        {
            Spawner spawner = new Spawner(gridSize, minBorderMargin, minMarginBetween, Obstacles);
            SpawnPoints.Add(spawner);
        }

    }
}
