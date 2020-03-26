﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : IGenerableObject {

	Vector2 gridSize;

	int amount = 4;

	int minBorderMargin = 10;
	int minMarginBetweenObstacles = 5;

	public List<Obstacle> Obstacles { get; private set; } = new List<Obstacle>();

	public List<IGameObject> ObjectList => Obstacles.ConvertAll(x => (IGameObject)x);

	public ObstacleGenerator(Vector2 gridSize) {
		this.gridSize = new Vector2(gridSize.x, gridSize.y);
	}

	public void CreateObstacles() {

		for (int i = 0; i < amount; i++) {
			Obstacle obstacle = new Obstacle(gridSize, minBorderMargin, minMarginBetweenObstacles, Obstacles);
			Obstacles.Add(obstacle);
		}

	}

}
