﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Game
{
    public class Weapon
    {
        float ShootTimeCD { get; set; }

        float minShootTime = 0.4f;
        float maxShootTime = 1.5f;

        bool canShoot;

        public int ShootedBullets { get; private set; } = 0;

        public Weapon()
        {
            ShootTimeCD = Random.Range(minShootTime, maxShootTime); 
        }

        public void Reload()
        {
            ShootTimeCD -= Game.deltaTime;
            if (ShootTimeCD <= 0)
            {
                canShoot = true;
                ShootTimeCD = Random.Range(minShootTime, maxShootTime);
            }
        }

        public void Shoot(GameObject opponent, int OpponentId)
        {
            if (canShoot) {
                Bullet bullet = BulletGenerator.Instance.Pop(OpponentId);
                bullet.GameObject.transform.rotation = opponent.transform.rotation;
                bullet.GameObject.transform.position = opponent.transform.position;
                bullet.GameObject.transform.Translate(Vector3.forward);
                canShoot = false;
                ShootedBullets++;
            }
        }
    }
}
