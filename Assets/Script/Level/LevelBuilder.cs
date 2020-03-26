﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder {

	public Vector2 Dimensions { get; private set; }

	public ObstacleGenerator ObstacleGenerator { get; private set; }
	public SpawnerGenerator SpawnerGenerator { get; private set; }

	public OpponentsCreator OpponentsCreator { get; private set; }

	public LevelBuilder() {
		Dimensions = new Vector2(100, 100);
		ObstacleGenerator = new ObstacleGenerator(Dimensions);
		SpawnerGenerator = new SpawnerGenerator(Dimensions);
		OpponentsCreator = new OpponentsCreator();
	}

	public void BuildLevel() {
		ObstacleGenerator.Create();
		SpawnerGenerator.Create(ObstacleGenerator.ObjectList);
	}

	public void CreateEnemies() {
		OpponentsCreator.CreateOpponents(20, SpawnerGenerator);
	}
	

}
