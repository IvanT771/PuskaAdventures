using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotCannon : MonoBehaviour
{
   // public Animator animPushka;
    public Transform SpawnTransform;
    public Transform targetTransform;
    public Transform spawnSmoke;
    [Space(5)]
    [Header("Стреляет? Да/Нет")]
    public bool isActive = true;
    [Space(5)]

    public float AngleInDegrees;

    float g = Physics.gravity.y;

    public GameObject Bullet;
    public GameObject Smoke;
    public float TimeShot = 5f;

    private void Start()
    {
        StartCoroutine(CannonShot());
        if (targetTransform == null)
        {
            GameObject targetPlayer = GameObject.FindGameObjectWithTag("Player");
            targetTransform = targetPlayer.transform;
        }
    }
    IEnumerator CannonShot()
    {
        while (isActive)
        {
            yield return new WaitForSeconds(TimeShot + 1);
            Shot();
         //   animPushka.Play("ShotAnim");
            
        }
    }
    void Update()
    {
        if (!isActive || targetTransform == null) { return; }
        SpawnTransform.localEulerAngles = new Vector3(-AngleInDegrees, 0f, 0f);
        Vector3 fromTo = targetTransform.position - transform.position;
        Vector3 fromToXZ = new Vector3(fromTo.x, 0f, fromTo.z);

        transform.rotation = Quaternion.LookRotation(fromToXZ, Vector3.up);

       
    }

    public void Shot()
    {
        Vector3 fromTo = targetTransform.position - transform.position;
        Vector3 fromToXZ = new Vector3(fromTo.x, 0f, fromTo.z);

        transform.rotation = Quaternion.LookRotation(fromToXZ, Vector3.up);


        float x = fromToXZ.magnitude;
        float y = fromTo.y;

        float AngleInRadians = AngleInDegrees * Mathf.PI / 180;

        float v2 = (g * x * x) / (2 * (y - Mathf.Tan(AngleInRadians) * x) * Mathf.Pow(Mathf.Cos(AngleInRadians), 2));
        float v = Mathf.Sqrt(Mathf.Abs(v2));

        GameObject newBullet = Instantiate(Bullet, SpawnTransform.position, Quaternion.identity);
        var ef = Instantiate(Smoke, spawnSmoke.position, transform.rotation);
        ef.transform.parent = transform;
        Destroy(ef, 10f);
        Physics.IgnoreCollision(newBullet.GetComponent<Collider>(), GetComponent<Collider>());
        newBullet.GetComponent<Rigidbody>().velocity = SpawnTransform.forward * v;
        Destroy(newBullet, 20f);
    }
}
