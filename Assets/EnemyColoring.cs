using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColoring : MonoBehaviour
{
    public enum ColorType
    {
        Red,
        Green,
        Blue,
        Random
    }
    public ColorType colorType = ColorType.Random;
    public GameObject go;
    public Color color = Color.black;
    public Material material;
    public bool isRainbow = false;
    public bool enteredRainbow = false;
    public Color32[] colors;

    // Start is called before the first frame update
    void Start()
    {
        go = this.gameObject;
        material = go.GetComponent<SpriteRenderer>().material;
        if (color == Color.black)
        {
            switch (colorType)
            {
                case ColorType.Red:
                    color = new Color(
                        Random.Range(0.6f, 1f),
                        Random.Range(0f, 0.2f),
                        Random.Range(0f, 0.2f));
                    break;
                case ColorType.Green:
                    color = new Color(
                        Random.Range(0f, 0.2f),
                        Random.Range(0.6f, 1f),
                        Random.Range(0f, 0.2f));
                    break;
                case ColorType.Blue:
                    color = new Color(
                        Random.Range(0f, 0.2f),
                        Random.Range(0f, 0.2f),
                        Random.Range(0.6f, 1f)
                        );
                    break;
                case ColorType.Random:
                    color = new Color(
                        Random.Range(0f, 1f),
                        Random.Range(0f, 1f),
                        Random.Range(0f, 1f));
                    break;
            }

        }
        colors = new Color32[7]
      {
            new Color32(255, 0, 0, 255), //red
            new Color32(255, 165, 0, 255), //orange
            new Color32(255, 255, 0, 255), //yellow
            new Color32(0, 255, 0, 255), //green
            new Color32(0, 0, 255, 255), //blue
            new Color32(75, 0, 130, 255), //indigo
            new Color32(238, 130, 238, 255), //violet
      };
        material.SetColor("_BodyColor", color);

    }
    private void Update()
    {
        if (isRainbow && !enteredRainbow)
        {
            enteredRainbow = true;
            StartCoroutine(Cycle());
        }
    }
    public IEnumerator Cycle()
    {
        int i = 0;
        while (true)
        {
            for (float interpolant = 0f; interpolant < 1f; interpolant += 0.01f)
            {
                material.SetColor("_BodyColor", Color.Lerp(colors[i % 7], colors[(i + 1) % 7], interpolant)); 
                yield return null;
            }
            i++;
        }
    }



}
