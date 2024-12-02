using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Gun : MonoBehaviour
{
    //Public properties
    public GameObject bulletPrefab;
    public GameObject amunationPrefab;
    public GameObject player;
    public Transform gunBarrel;
    public float speedBullet;
    
    //Private attributes
    private Animator _gunAnimator;
    private SpriteRenderer _gunRenderer;
    private SpriteRenderer _playerSpriteRenderer;
    private Renderer _bulletRender;
    private Player _player;
    private byte _bulletNumbers;
    private float _randomX;
    private float _randomY;
    private bool _amunationFlag;

    //Methods
    private void Start()
    {
        _gunAnimator = GetComponent<Animator>();
        _gunRenderer = GetComponent<SpriteRenderer>();
        _player = FindObjectOfType<Player>();
        _playerSpriteRenderer = _player.GetComponent<SpriteRenderer>();
        _bulletNumbers = 12;
        _amunationFlag = false;

        //Position enable with amunation
    }

    private void Update()
    {
        // Shoot animation
        if (Input.GetMouseButtonDown(0))
        {
            if (_bulletNumbers > 0) 
            {
                _gunAnimator.SetBool("Shoot", true);
                _bulletNumbers--;
            }
        }
        else _gunAnimator.SetBool("Shoot", false);

        //Enable shoot animation if player got 0 bullets    
        if (Input.GetMouseButtonDown(0) && _bulletNumbers <=0) _gunAnimator.SetBool("Shoot", false);

        //Without flip
        if (_playerSpriteRenderer.flipX == false)
        {
            //Use mouse to move the gun (This is problem with 2d. I wanted to use LookAt but doesen't works in 2D...)
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f; //Because we are in 2d
            Vector3 direction = mousePosition - transform.position; //This is the direction for the gun
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //transform to "º"
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); //Now we can evite use quaternions
            
            //Shoot
            if (Input.GetMouseButtonDown(0) && _bulletNumbers > 0) Shoot();
            
            //Gun position
            Vector3 newPosition = new Vector3(_player.transform.position.x + 0.7f, _player.transform.position.y - 0.3f, transform.position.z);
            transform.position = newPosition;
            _gunRenderer.flipX = false;
            gunBarrel.localPosition = new Vector3(0.407f, 0.141f, 0);

        }
        //With flip
        if (_playerSpriteRenderer.flipX)
        {
            //Use mouse to move the gun (This is problem with 2d. I wanted to use LookAt but doesen't works in 2D...)
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f; //Because we are in 2d
            Vector3 direction = (mousePosition - transform.position) * (-1); //This is the direction for the gun
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //transform to "º"
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); //Now we can evite use quaternions
           
            //Shoot
            if (Input.GetMouseButtonDown(0) && _bulletNumbers > 0) ShootWithFlip();

            //Gun position
            Vector3 newPosition = new Vector3(_player.transform.position.x - 0.7f, _player.transform.position.y - 0.3f , transform.position.z);
            transform.position = newPosition;
            _gunRenderer.flipX = true;
            gunBarrel.transform.localPosition = new Vector3(-0.407f, 0.141f, 0);
        }

        //Show amunation
        if (_bulletNumbers == 0 && _amunationFlag == false)
        {
            ShowAmunation();
        }

        //Auxiliary intermediate step to sho amunation
        if (_bulletNumbers == 5) _amunationFlag = false;

        
        //Prints
        Debug.Log("Balas: " + _bulletNumbers);
        Debug.Log(_amunationFlag);


        //"Clean Print". Only show 0 bullets in console
        if (_bulletNumbers <=0) _bulletNumbers = 0;

    }

    private void Shoot ()
    {
        //Make cube
        GameObject bullet = GameObject.Instantiate(bulletPrefab,gunBarrel.position, gunBarrel.rotation);
        bullet.transform.position += new Vector3(0.2f, 0f, 0f);
        bullet.GetComponent<Rigidbody2D>().AddForce(gunBarrel.right * speedBullet,ForceMode2D.Impulse);
        Destroy(bullet, 10f);
    }

    private void ShootWithFlip()
    {
        GameObject bullet = GameObject.Instantiate(bulletPrefab, gunBarrel.position, gunBarrel.rotation);
        bullet.transform.position += new Vector3( - 0.2f, 0f, 0f);
        bullet.GetComponent<Rigidbody2D>().AddForce( - gunBarrel.right * speedBullet, ForceMode2D.Impulse);
        Destroy(bullet, 10f);
    }

    public void ShowAmunation() 
    {
        //Create random position
        _randomX = UnityEngine.Random.Range(-7.5f, 7.5f); //Unity is confusing libraru UnityEngine with system
        _randomY = UnityEngine.Random.Range(-3.5f, 3.5f);

        //Make a new position
        Vector3 amunationPosition = new Vector3(_randomX, _randomY, 0f);

        //Instantiate
        GameObject amunationObject = GameObject.Instantiate(amunationPrefab, amunationPosition, Quaternion.identity);

        //Make flag
        _amunationFlag = true;
    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Amunation")
        {
            _bulletNumbers = 12;
            Destroy(collision.gameObject);
        }
    }


}
