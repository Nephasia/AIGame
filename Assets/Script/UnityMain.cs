using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityMain : MonoBehaviour {

	Game game;

	void Start()
    {
		game = new Game();
		game.CreateGame();

		SetWholeLevel();

	}

    void Update()
    {
        
    }

	void SetWholeLevel() {
		SetUpGround();
		SetUpObstacles();
		SetUpSpawners();
	}

	void SetUpGround() {

		GameObject go = GameObject.CreatePrimitive(PrimitiveType.Plane);
		go.transform.localScale = new Vector3(game.LevelBuilder.Dimensions.x / 10, 1, game.LevelBuilder.Dimensions.y / 10);
		go.transform.position = new Vector3(game.LevelBuilder.Dimensions.x / 2, 0, game.LevelBuilder.Dimensions.y / 2);
	}

	void SetUpObstacles() {

		GameObject obstaclesContainer = new GameObject("ObstaclesContainer");

		int id = 0;

		foreach (var item in game.LevelBuilder.ObstacleGenerator.Obstacles) {

			item.GameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
			item.GameObject.transform.SetParent(obstaclesContainer.transform);
			item.GameObject.name = item.ToString() + id++;

			item.GameObject.transform.position = item.Position;
			item.GameObject.transform.localScale = item.Scale;

			item.GameObject.GetComponent<Renderer>().material = Resources.Load("Materials/Gray") as Material;
		}
	}

	void SetUpSpawners() {

		GameObject spawnersContainer = new GameObject("SpawnersContainer");

		int id = 0;

		foreach (var item in game.LevelBuilder.SpawnerGenerator.Spawners) {

			item.GameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
			item.GameObject.transform.SetParent(spawnersContainer.transform);
			item.GameObject.name = item.ToString() + id++;

			item.GameObject.transform.position = item.Position;
			item.GameObject.transform.localScale = item.Scale;

			item.GameObject.GetComponent<Renderer>().material = Resources.Load("Materials/Purple") as Material;
		}
	}

}
