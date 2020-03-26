using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BulletGenerator : IObjectList
{
	public static BulletGenerator Instance { get; private set; }

	public List<Bullet> Bullets = new List<Bullet>();
    public List<IGameObject> ObjectList => Bullets.ConvertAll(x => (IGameObject)x);

    int underBoard = -10;

    public BulletGenerator()
    {
        Instance = this;
    }
    public void CreateBullets(int amount)
    {
        Vector3 vector3 = new Vector3(0, underBoard, 0);

        for (int i = 0; i < amount; i++)
        {
            Bullet bullet = new Bullet();
            Bullets.Add(bullet);
        }
    }

    public void Push(Bullet bullet)
    {
        Bullets.Add(bullet);
    }

    public Bullet Pop()
    {
        Bullet bullet = Bullets[0];
        Bullets.RemoveAt(0);
        bullet.Initialize();
        return bullet;
    }
}

