using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder {

	public Vector2 Dimensions { get; private set; }

	public ObstacleGenerator ObstacleGenerator { get; private set; }
	//public Spawner Spawner { get; private set; }

	public LevelBuilder() {
		Dimensions = new Vector2(100, 100);
		ObstacleGenerator = new ObstacleGenerator(Dimensions);
		//Spawner = new Spawner(Dimensions, ObstacleGenerator., , ObstacleGenerator.Obstacles);
	}

	public void BuildLevel() {
		ObstacleGenerator.CreateObstacles();
		//Spawner.
	}

}
