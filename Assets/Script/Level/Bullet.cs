using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : IGameObject, IUpdateable
{
    public GameObject GameObject { get; set; }
    public Vector3 Position
    {
        get => GameObject.transform.position;
        set => GameObject.transform.position = value;
    }
    public Vector3 Scale
    {
        get => GameObject.transform.localScale;
        set => GameObject.transform.localScale = value;
    }

    static int BulletsCreated = 0;
    
    int Id { get; set; }
    int OpponentId { get; set; }
    int Damage { get; set; } = 50;
    float LifeTimeCD { get; set; }
    float CurrentSpeed { get; set; }

    const float lifeTime = 5;
    const float speed = 20;
    Movement movement;
    bool active = false;

    public Bullet()
    {
        Game.RegisterUpdateable(this);
        Id = BulletsCreated++;

        GameObject = new GameObject();
        GameObject.name = ToString() + Id;
        
        CurrentSpeed = speed;
        movement = new Movement(GameObject, speed, 0, 0);

    }

    public void Initialize()
    {
        LifeTimeCD = lifeTime;
        active = true;
    }

    public void Update()
    {
        if(active)
        {
            movement.HandleMovementInput(Inputs.MovementEnum.Forward);
            LifeTimeCD -= Game.IterationTime;
            if (LifeTimeCD <= 0)
            {
                Destroy();
            }
        }
        
    }

    public void Destroy()
    {
        active = false;
        int underBoard = -10;
        this.GameObject.transform.position = new Vector3(0, underBoard, 0);
        BulletGenerator.Instance.Push(this);
    }
}
