using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScoreManager : MonoBehaviour
{
    public float CurrentScore { get; private set; }
    private void Start()
    {
        CurrentScore = 0f;
    }
    public void AddScore(float addAmount)
    {
        if (addAmount > 0) CurrentScore += addAmount;
    }
    public void RemoveScore(float removeAmount)
    {
        if (removeAmount > 0) CurrentScore -= removeAmount;
    }
    public void ResetScore()
    {
        CurrentScore = 0;
    }
}
