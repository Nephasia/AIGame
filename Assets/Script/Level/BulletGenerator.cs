using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace Game
{
    public class BulletGenerator : IObjectList
    {
        public static BulletGenerator Instance { get; private set; }

        public List<Bullet> Bullets = new List<Bullet>();
        public List<IGameObject> ObjectList => Bullets.ConvertAll(x => (IGameObject)x);

        int position = 20;

        public BulletGenerator()
        {
            Instance = null;
            Instance = this;
        }
        public void CreateBullets(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Bullet bullet = new Bullet();
                bullet.Position = new Vector3(-position, position, -position);
                Bullets.Add(bullet);
            }
        }

        public void Push(Bullet bullet)
        {
            Bullets.Add(bullet);
        }

        public Bullet Pop(int OpponentId)
        {
            Bullet bullet = Bullets[0];
            Bullets.RemoveAt(0);
            bullet.Initialize(OpponentId);
            return bullet;
        }
    }
}

