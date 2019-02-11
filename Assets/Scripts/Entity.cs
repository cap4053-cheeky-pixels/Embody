using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Entity : MonoBehaviour
{
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public int Strength { get; set; }
    public int Speed { get; set; }
    protected float tickRate = 1.8f;

    public abstract void Attack();
    public abstract void Move();
    public abstract void ChangeMaxHealth(int amount);
    public abstract void ChangeHealth(int amount);
}
