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

		foreach (var item in game.LevelBuilder.ObstacleGenerator.Obstacles) {

			GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);

			go.transform.position = item.Position;
			go.transform.localScale = item.Scale;

			go.GetComponent<Renderer>().material = Resources.Load("Materials/Gray") as Material;
		}
	}

	void SetUpSpawners() {
		//foreach (var item in game.LevelBuilder.Obstacles) {

		//	GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);

		//	go.transform.position = item.Position;
		//	go.transform.localScale = item.Scale;

		//	go.GetComponent<Renderer>().material = Resources.Load("Materials/Gray") as Material;
		//}
	}

}
