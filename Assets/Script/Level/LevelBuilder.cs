using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder {

	public Vector2 Dimensions { get; private set; }

	public ObstacleGenerator ObstacleGenerator { get; private set; }
	public SpawnerGenerator SpawnerGenerator { get; private set; }

	public LevelBuilder() {
		Dimensions = new Vector2(100, 100);
		ObstacleGenerator = new ObstacleGenerator(Dimensions);
		SpawnerGenerator = new SpawnerGenerator(Dimensions);
	}

	public void BuildLevel() {
		ObstacleGenerator.Create();
		SpawnerGenerator.Create(ObstacleGenerator.ObjectList);
	}

}
