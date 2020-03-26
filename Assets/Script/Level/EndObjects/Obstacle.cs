using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : IGameObject {

	public GameObject GameObject { get; set; }

	public Vector3 Position { get; set; }
	public Vector3 Scale { get; set; }

	int minSize = 5;
	int maxSize = 15;

	int height = 2;

	public Obstacle(
		Vector2 gridSize, int minBorderMargin, int minMarginBetweenObstacles,
		List<IGameObject> otherObjects
	) {
		Scale = RandScale();
		Position = CreateClearPosition(gridSize, minBorderMargin, minMarginBetweenObstacles, otherObjects);
	}

	Vector3 CreateClearPosition(
		Vector2 gridSize, int minBorderMargin, int minMarginBetweenObstacles,
		List<IGameObject> otherObjects
	) {
		Vector3 randPosition;

		bool isClear;

		do {
			isClear = true;

			randPosition = new Vector3(
				(int)Random.Range(0 + minBorderMargin, gridSize.x - minBorderMargin),
				height/2,
				(int)Random.Range(0 + minBorderMargin, gridSize.y - minBorderMargin)
			);

			for (int i = 0; i < otherObjects.Count; i++) {
				if (DistanceBetween(randPosition.x, Scale.x, otherObjects[i].Position.x, otherObjects[i].Scale.x) < minMarginBetweenObstacles
					&& DistanceBetween(randPosition.z, Scale.z, otherObjects[i].Position.z, otherObjects[i].Scale.z) < minMarginBetweenObstacles
				) {
					isClear = false;
				}
			}
		} while (!isClear);

		return randPosition;
	}

	Vector3 RandScale() {
		return new Vector3(
			Random.Range(minSize, (float)maxSize),
			height,
			Random.Range(minSize, (float)maxSize)
		);
	}

	float DistanceBetween(float firstPos, float firstSize, float secondPos, float secondSize) {
		return Mathf.Abs(firstPos - secondPos) - firstSize/2 - secondSize/2;
	}

}
