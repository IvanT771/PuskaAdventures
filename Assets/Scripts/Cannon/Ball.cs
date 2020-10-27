using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public AudioSource audio;
    public AudioClip[] clips;
    public GameObject Spark;
    private bool isPopodanie = false;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isPopodanie)
        {
            var ef = Instantiate(Spark, collision.GetContact(0).point, Quaternion.identity);
            audio.clip = clips[0]; //Звук поподания в игрока
            audio.Play();
            isPopodanie = true;
            Destroy(ef, 5f);

            //Наносим урон игроку 
        }
    }
}
