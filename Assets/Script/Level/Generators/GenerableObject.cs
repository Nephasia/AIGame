using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	public abstract class GenerableObject : IGenerableObject
	{

		public abstract List<IGameObject> ObjectList { get; }

		public virtual void Create(bool avoidSelf = true)
		{
			List<IGameObject> objects = new List<IGameObject>();
			Create(objects, avoidSelf);
		}

		public abstract void Create(List<IGameObject> avoidObjects, bool avoidSelf = true);

	}

}
