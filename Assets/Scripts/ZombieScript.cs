using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour {

    public Transform target;
    public float speed = .01f;
    private Animator _animator;

    //private CircleCollider2D _col;

    void Awake()
    {
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player").transform;
        _animator = GetComponentInChildren<Animator>();
        //_col = GetComponent<CircleCollider2D>();
    }

    void FixedUpdate()
    {
        //rotate to look at the player
        transform.right = (target.position - transform.position).normalized;
        //transform.LookAt(target.position);
        //transform.Rotate(new Vector3(0,0, 180), Space.Self);//correcting the original rotation

        //move towards the player
        if (Vector3.Distance(transform.position, target.position) > 1.5f)
        {//move if distance from target is greater than 1.5
            transform.Translate(new Vector3(speed * Time.deltaTime, 0));
            if (_animator.GetBool("Attack")) {
                _animator.SetBool("Attack", false);
            }
        }
        else
        {
            _animator.SetBool("Attack", true);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
       /* if (col.transform.tag == "Limite")
        {
            Physics2D.IgnoreCollision(col.collider, GetComponent<Collider2D>(), true);
        }*/
        if (col.transform.tag == "Bullet")
        {
            //Life -= 50;
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
        /*if (col.transform.tag == "Player")
        {
            LifeEvent.SumEnergyPlayer(-1f);
            if (_animPlayer.GetInteger("State") == 4)
                if (col.collider.GetType() == typeof(BoxCollider2D))
                {
                    Life -= 25;
                }
        }*/

    }

}
