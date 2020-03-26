using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : IGameObject
{	
	public GameObject GameObject { get; set; }
    public Vector3 SpawnPoint { get; set; }
	public Vector3 Position { get; set; }
	public Vector3 Scale { get; set; }

	float size = 0.1f;

	public Spawner(Vector2 gridSize, int minBorderMargin, int minMarginBetween, List<IGameObject> otherObjects)
    {
		Scale = Vector3.one * size;
        SpawnPoint = CreateClearPosition(gridSize, minBorderMargin, minMarginBetween, otherObjects);
    }

    Vector3 CreateClearPosition(Vector2 gridSize, int minBorderMargin, int minMarginBetween, List<IGameObject> otherObjects)
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

            for (int i = 0; i < otherObjects.Count; i++)
            {
                if (DistanceBetween(randPosition.x, otherObjects[i].Position.x, otherObjects[i].Scale.x) < minMarginBetween
                    && DistanceBetween(randPosition.z, otherObjects[i].Position.z, otherObjects[i].Scale.z) < minMarginBetween
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