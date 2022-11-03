using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerSong : MonoBehaviour
{
    public AudioSource _song;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerAttribute>() != null)
        {
            _song.Play();
        }
    }
}
