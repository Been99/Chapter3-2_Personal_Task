using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    public UICondition uICondition;

    Condition health { get { return uICondition.health; } }
    Condition hunger { get { return uICondition.hunger; } }
    Condition stemina { get { return uICondition.stemina; } }

    public float noHungerHealthDecay;

    void Update()
    {
        hunger.Subtract(hunger.passiveValue * Time.deltaTime);
        stemina.Add(stemina.passiveValue * Time.deltaTime);

        if (hunger.curValue <= 0.0f)
        {
            health.Subtract(noHungerHealthDecay * Time.deltaTime);
        }

        if (health.curValue <= 0.0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Eat(float amount)
    {
        hunger.Add(amount);
    }
    

    public void Die()
    {
        Debug.Log("죽었다");

    }
}
