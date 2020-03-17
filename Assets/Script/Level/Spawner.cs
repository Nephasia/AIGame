using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class Spawner
{
    public List<Vector3> SpawnPoints { get; set; }

    int numberSpawnPoints = 4;

    public Spawner(
        Vector2 gridSize, int minBorderMargin, int minMarginBetween, List<Obstacle> obstacles)
    {
        SpawnPoints = new List<Vector3>();
        for (int i = 0; i < numberSpawnPoints; i++)
        {
            SpawnPoints.Add(CreateClearPosition(gridSize, minBorderMargin, minMarginBetween, obstacles));
        }
    }

    Vector3 CreateClearPosition(
        Vector2 gridSize, int minBorderMargin, int minMarginBetween, List<Obstacle> obstacles)
    {
        Vector3 randPosition;
        bool isClear;

        do
        {
            isClear = true;

            randPosition = new Vector3(
                (int)Random.Range(0 + minBorderMargin, gridSize.x - minBorderMargin),
                0,
                (int)Random.Range(0 + minBorderMargin, gridSize.y - minBorderMargin)
            );

            for (int i = 0; i < obstacles.Count; i++)
            {
                if (DistanceBetween(randPosition.x, obstacles[i].Position.x, obstacles[i].Scale.x) < minMarginBetween
                    || DistanceBetween(randPosition.y, obstacles[i].Position.y, obstacles[i].Scale.y) < minMarginBetween
                )
                {
                    isClear = false;
                }
            }
        } while (!isClear);

        return randPosition;
    }

    float DistanceBetween(float pointPos, float obstaclePos, float obstacleSize)
    {
        return Mathf.Abs(pointPos - obstaclePos) - obstacleSize / 2;
    }
}