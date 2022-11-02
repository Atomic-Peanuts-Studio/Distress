using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarManager : MonoBehaviour
{
    public GameObject heartPrefab;
    public Health playerHealth;
    List<HeartManager> hearts = new List<HeartManager>();
    public void Start()
    {
        DrawHearts(0);
    }
    private void OnEnable()
    {
        playerHealth.healthChanged += DrawHearts;
    }
    private void OnDisable()
    {
        playerHealth.healthChanged -= DrawHearts;
    }
    public void ClearHearts()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        hearts = new List<HeartManager>();
    }
    public void CreateEmptyHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);
        HeartManager heartManager = newHeart.GetComponent<HeartManager>();
        heartManager.SetHeartImage(HeartManager.HeartStatus.Empty);
        hearts.Add(heartManager);
    }
    public void DrawHearts(float justToBeHere)
    {
        ClearHearts();
        float maxHRemainder = playerHealth.maxHealth % 2;
        int heartsToMake = (int)(playerHealth.maxHealth / 2 + maxHRemainder);
        for (int i = 0; i < heartsToMake; i++)
        {
            CreateEmptyHeart();
        }
        for (int i = 0; i < hearts.Count; i++)
        {
            int hearthStatusRemainder = (int)Mathf.Clamp(playerHealth.health - (i * 2), 0, 2);
            hearts[i].SetHeartImage((HeartManager.HeartStatus)hearthStatusRemainder);
        }
    }
}
