﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class OpponentsCreator : IObjectList {

	public List<Opponent> Opponents = new List<Opponent>();
	public List<IGameObject> ObjectList => Opponents.ConvertAll(x => (IGameObject)x);

	public void CreateOpponents(int amount, SpawnerGenerator spawnerGenerator) {

		if (CheckIfEnoughSpawners(amount, spawnerGenerator)) {
			
			Stack<Vector3> spawnersPositions = new Stack<Vector3>(spawnerGenerator.Spawners.Select(x => x.Position));

			for (int i = 0; i < amount; i++) {
				Opponent opponent = new Opponent(spawnersPositions.Pop());
				Opponents.Add(opponent);
			}

		} else {
			throw new System.Exception(
				"Not enought spawners for enemies !\n" +
				"You have " + spawnerGenerator.SpawnersAmount + " spawners only."
			);
		}
	}

	private bool CheckIfEnoughSpawners(int amount, SpawnerGenerator spawnerGenerator) {
		return amount <= spawnerGenerator.SpawnersAmount;
	}

}
