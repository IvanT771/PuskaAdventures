using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slap : MonoBehaviour
{
    public Animator animator;
    public GameObject sparkEf;
    public Transform spawnSpark;
    public AudioClip[] clips; //0 - slap, 1- popodanie 
    public AudioSource audio;
    private bool isAttack = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.Play("Slap");
        }else
        if (Input.GetMouseButtonDown(1))
        {
            animator.Play("Tyti");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cannon") && isAttack)
        {

            other.gameObject.GetComponent<DamageCannon>().Damage_cannon(1, -transform.right);
            var ef = Instantiate(sparkEf, spawnSpark.position, Quaternion.identity);
            audio.clip = clips[0]; //Звук удара
            audio.Play();
            Destroy(ef, 10f);
            isAttack = false; //Исключаем двойной удар 
        }
    }

    //как только мы замохнулись рукой, то разрешаем наносить удар 
    public void ChangeAttackTrue()
    {
        isAttack = true;

    }
    //как только рука вернулась в исходное положение, то запрещаем наносить удар 
    public void ChangeAttackFalse()
    {
        isAttack = false;

    }
}
