using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopSongTrigger : MonoBehaviour
{
    //public AudioSource _song;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerAttribute>() != null)
        {
            AudioSource _song = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
            _song.Stop();
        }
    }
}
