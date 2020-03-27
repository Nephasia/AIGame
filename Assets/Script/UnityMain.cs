using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	public class UnityMain : MonoBehaviour
	{

		Game game;

		void Start()
		{
			game = new Game();
			game.CreateGame();

			SetWholeLevel();
		}

		void Update()
		{
			game.MakeIteration(100);

			// todo : updates position and rotation of all enemies
		}

		void SetWholeLevel()
		{
			SetUpGround();
			SetUp("Obstacles", game.LevelBuilder.ObstacleGenerator, "Materials/Gray");
			SetUp("Spawners", game.LevelBuilder.SpawnerGenerator, "Materials/Purple");

			SetUpUpdateable("Enemy", game.LevelBuilder.OpponentsCreator, "Materials/Red", Vector3.one);
			SetUpUpdateable("Bullet", game.LevelBuilder.BulletGenerator, "Materials/Yellow", Vector3.one * 0.4f);
		}

		void SetUpGround()
		{
			GameObject go = GameObject.CreatePrimitive(PrimitiveType.Plane);
			go.transform.localScale = new Vector3(game.LevelBuilder.Dimensions.x / 10, 1, game.LevelBuilder.Dimensions.y / 10);
			go.transform.position = new Vector3(game.LevelBuilder.Dimensions.x / 2, 0, game.LevelBuilder.Dimensions.y / 2);
		}

		void SetUp(string name, IObjectList objectList, string materialName)
		{
			GameObject container = new GameObject(name + "Container");
			int id = 0;
			foreach (var item in objectList.ObjectList)
			{
				PrepateGameObject(item, container, id, materialName);
				id++;
			}
		}

		void PrepateGameObject(IGameObject item, GameObject container, int id, string materialName)
		{
			item.GameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
			item.GameObject.transform.SetParent(container.transform);
			item.GameObject.name = item.ToString() + "_" + id;

			item.GameObject.transform.position = item.Position;
			item.GameObject.transform.localScale = item.Scale;

			item.GameObject.GetComponent<Renderer>().material = Resources.Load(materialName) as Material;
		}

		void SetUpUpdateable(string name, IObjectList generableObject, string materialName, Vector3 scale)
		{
			GameObject container = new GameObject(name + "Container");
			foreach (var item in generableObject.ObjectList)
			{
				PrepateUpdateableObjects(item, container, materialName, scale);
			}
		}

		void PrepateUpdateableObjects(IGameObject item, GameObject container, string materialName, Vector3 scale)
		{
			item.GameObject.AddComponent<MeshRenderer>();
			MeshFilter mf = item.GameObject.AddComponent<MeshFilter>();
			CreateCubeMesh(ref mf);

			item.GameObject.transform.SetParent(container.transform);
			item.GameObject.GetComponent<Renderer>().material = Resources.Load(materialName) as Material;

			item.GameObject.AddComponent<BoxCollider>();

			item.GameObject.transform.localScale = scale;

		}

		void CreateCubeMesh(ref MeshFilter mf)
		{
			GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);

			MeshFilter mf2 = go.GetComponent<MeshFilter>();
			Mesh mesh = Instantiate(mf2.mesh) as Mesh;

			mf.mesh = mesh;
			Destroy(go);
		}

	}
}
