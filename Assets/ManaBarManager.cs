using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaBarManager : MonoBehaviour
{
    public GameObject cookiePrefab;
    public PlayerAttribute playerMana;
    List<HeartManager> hearts = new List<HeartManager>();
    public void Start()
    {
        DrawCookies();
    }
    private void OnEnable()
    {
        playerMana.manaChanged += DrawCookies;
    }
    private void OnDisable()
    {
        playerMana.manaChanged -= DrawCookies;
    }
    public void ClearCookies()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        hearts = new List<HeartManager>();
    }
    public void CreateEmptyCookie()
    {
        GameObject newHeart = Instantiate(cookiePrefab);
        newHeart.transform.SetParent(transform);
        HeartManager heartManager = newHeart.GetComponent<HeartManager>();
        heartManager.SetHeartImage(HeartManager.HeartStatus.Empty);
        hearts.Add(heartManager);
    }
    public void DrawCookies()
    {
        ClearCookies();
        float cookieReminder = playerMana.maxMana % 2;
        int heartsToMake = (int)(playerMana.maxMana / 2 + cookieReminder);
        for (int i = 0; i < heartsToMake; i++)
        {
            CreateEmptyCookie();
        }
        for (int i = 0; i < hearts.Count; i++)
        {
            int hearthStatusRemainder = (int)Mathf.Clamp(playerMana.mana - (i * 2), 0, 2);
            hearts[i].SetHeartImage((HeartManager.HeartStatus)hearthStatusRemainder);
        }
    }
}
