using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    private float Speed = 18.0f;
    private float Range = 1f;
    public AudioClip ShootSound = null;
    private bool canMove = true;

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (canMove)
        {
            transform.Translate(Speed * Time.deltaTime, 0, 0);
            Destroy(gameObject, Range);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Bullet" || other.name!="MachineGun")
        {
            canMove = false;
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;

            //Destroy(gameObject, ShootSound.length);
        }
    }
}
