using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward  
{

    float expirience;
    float coins;
    bool result;

    public Reward(float expirience, float coins, bool result)
    {
        this.expirience = expirience;
        this.coins = coins;
        this.result = result;
    }

    public void AddExpirience(float value)
    {
        this.expirience = value;
    }

    public void AddCoins(float value)
    {
        this.coins = value;
    }

    public void SetResult(bool value)
    {
        this.result = value;
    }

    public bool GetResult()
    {
        return this.result;
    }
}
