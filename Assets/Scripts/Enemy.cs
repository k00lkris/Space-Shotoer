using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 4.0f;
    [SerializeField]
    private GameObject _enemyPrefab;

    private Player _player;
    // handle to aninimator component
    private Animator _anim;
    // Start is called before the first frame update
    void Start()
    { 
        _player = GameObject.Find("Player").GetComponent<Player>();
        if(_player == null)
        {
            Debug.LogError("The Player is NULL.");
        }
        // null check player
        // assign the component to Anim
        _anim = GetComponent<Animator>();

        if(_anim == null)
        {
            Debug.Log("The Anim is NULL.");
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);

        if(transform.position.y < -5.0f)
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 7, 0);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            
            if (player != null)
            {
                player.Damage();
            }
            _anim.SetTrigger("OnEnemyDeath");
            _enemySpeed = 0;
            // trigger anim
            Destroy(this.gameObject, 2.8f);
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);

            if(_player != null)
            {
                _player.AddScore(10);
            }
            _anim.SetTrigger("OnEnemyDeath");
            _enemySpeed = 0;
            // trig anim
            Destroy(this.gameObject,2.8f);
        }
    }
}
