using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Game
{
    class Weapon
    {
        float ShootTimeCD { get; set; }

        float shootTime = 0.6f;

        bool canShoot;

        public Weapon()
        {
            ShootTimeCD = shootTime;
        }

        public void Reload()
        {
            ShootTimeCD -= Game.IterationTime;
            if (ShootTimeCD <= 0)
            {
                canShoot = true;
                ShootTimeCD = shootTime;
            }
        }

        public void Shoot(GameObject opponent)
        {
            if (canShoot) {
                Bullet bullet = BulletGenerator.Instance.Pop();
                bullet.GameObject.transform.rotation = opponent.transform.rotation;
                bullet.GameObject.transform.position = opponent.transform.position;
                bullet.GameObject.transform.Translate(Vector3.forward);
                canShoot = false;
            }
        }
    }
}
