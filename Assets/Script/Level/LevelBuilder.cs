using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder {

	Vector2 dimensions;

	public LevelBuilder() {
		dimensions = new Vector2(100, 100);

	}

	public void BuildLevel() {
		ObstacleGenerator obstacleGenerator = new ObstacleGenerator(dimensions);
		obstacleGenerator.CreateObstacles();
	}

}
