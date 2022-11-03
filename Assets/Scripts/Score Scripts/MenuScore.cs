using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuScore : MonoBehaviour
{
    public static int LastScore = 0;
    private int HiScore = 0;

    public GameObject TMPLastScore;
    public GameObject TMPHiScore;
    // Start is called before the first frame update
    void Start()
    {
        TMPLastScore.GetComponent<TMP_Text>().text = LastScore.ToString();

        if (HiScore < LastScore)
        {
            HiScore = LastScore;
        }
        TMPHiScore.GetComponent<TMP_Text>().text = HiScore.ToString();
    }
}
