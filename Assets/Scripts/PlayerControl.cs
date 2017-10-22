using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public float Speed;
    public GameObject PointForward;
    public GameObject PointBackward;

    public GameObject BulletPrefab;
    public float FireRate = 0.3f;
    float _timeElapsed;

    // Update is called once per frame
    void Update()
    {
        Shoot();

        if (Input.GetKey(KeyCode.UpArrow))
            Walk(PointForward);
        if (Input.GetKey(KeyCode.DownArrow))
            Walk(PointBackward);
        if (Input.GetKey(KeyCode.LeftArrow))
            RotateLeft();
        if (Input.GetKey(KeyCode.RightArrow))
            RotateRight();
    }

    void Walk(GameObject point)
    {
        if (transform.position.x != 16f && transform.position.x != -16f && transform.position.y != 10f && transform.position.y != -10f)
            transform.position = Vector3.MoveTowards(transform.position, point.transform.position, Speed * Time.deltaTime);
    }

    void RotateLeft()
    {
        transform.Rotate(Vector3.forward * 1f * Speed);
    }

    void RotateRight()
    {
        transform.Rotate(Vector3.forward * -1f * Speed);
    }

    public void Shoot()
    {
        //Si podemos disparar
        if (_timeElapsed <= 0)
        {
            //Si el usuario apreta el boton Fire1
            if (Input.GetButton("Fire1"))
            {
                //Reseteamos el valor que tenemos que esperar
                _timeElapsed = FireRate;
                //Instanciamos una bala en la escena, en la posicion del jugador

                GameObject bullet = Instantiate(BulletPrefab, transform.position, transform.rotation);
                bullet.transform.parent = gameObject.transform;
                bullet.transform.localPosition = new Vector3(0.0776f, 0.5f);
                bullet.transform.parent = null;
            }
        }
        else
        {
            //Sino, restamos tiempo al _timeElapsed
            _timeElapsed -= Time.deltaTime;
        }
    }


}
