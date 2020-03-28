using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	public class ObstacleGenerator : GenerableObject
	{

		Vector2 gridSize;

		int amount = 4;

		int minBorderMargin = 10;
		int minMarginBetweenObstacles = 5;

		public List<Obstacle> Obstacles { get; } = new List<Obstacle>();
		public override List<IGameObject> ObjectList => Obstacles.ConvertAll(x => (IGameObject)x);

		public ObstacleGenerator(Vector2 gridSize)
		{
			this.gridSize = new Vector2(gridSize.x, gridSize.y);
		}

		public override void Create(List<IGameObject> avoidObjects, bool avoidSelf = true)
		{

			for (int i = 0; i < amount; i++)
			{
				Obstacle obstacle = new Obstacle(
					gridSize, minBorderMargin, minMarginBetweenObstacles,
					avoidObjects
				);
				Obstacles.Add(obstacle);
				if (avoidSelf)
				{
					avoidObjects.Add(obstacle);
				}
			}
            CreateBoarders();
		}

        private void CreateBoarders()
        {
            int boarderWidth = 1;
            Obstacle obstacle = new Obstacle(new Vector3(Game.LevelBuilder.Dimensions.x / 2, 1,0), new Vector3(Game.LevelBuilder.Dimensions.x, boarderWidth, boarderWidth));
            Obstacles.Add(obstacle);
            obstacle = new Obstacle(new Vector3(Game.LevelBuilder.Dimensions.x / 2, 1, Game.LevelBuilder.Dimensions.y), new Vector3(Game.LevelBuilder.Dimensions.x, boarderWidth, boarderWidth));
            Obstacles.Add(obstacle);
            obstacle = new Obstacle(new Vector3(0, 1, Game.LevelBuilder.Dimensions.y / 2), new Vector3(boarderWidth, boarderWidth, Game.LevelBuilder.Dimensions.y));
            Obstacles.Add(obstacle);
            obstacle = new Obstacle(new Vector3(Game.LevelBuilder.Dimensions.x, 1, Game.LevelBuilder.Dimensions.y / 2), new Vector3(boarderWidth, boarderWidth, Game.LevelBuilder.Dimensions.y));
            Obstacles.Add(obstacle);
        }

	}
}
