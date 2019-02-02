using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Entity : MonoBehaviour
{
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public int Strength { get; set; }
    public int Speed { get; set; }
    // TODO add AttackPattern or Ability property
    // TODO potentially incorporate Movement-related code into this class

    public abstract void Attack();
    public abstract void Move();
    public abstract void ChangeHealth(int amount);
}
