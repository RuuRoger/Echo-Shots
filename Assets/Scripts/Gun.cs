using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Gun : MonoBehaviour
{
    //public properties
    public Camera camera;

    //Private attributes
    private Animator _gunAnimator;
    private SpriteRenderer _gunRenderer;

    //Methods
    private void Start()
    {
        _gunAnimator = GetComponent<Animator>();
        _gunRenderer = GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        // Shoot
        if (Input.GetMouseButtonDown(0)) _gunAnimator.SetBool("Shoot", true);
        else _gunAnimator.SetBool("Shoot", false);

        //Use mouse to move the gun (This is problem with 2d. I wanted to use LookAt but doesen't works in 2D...)
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; //Because we are in 2d
        Vector3 direction = mousePosition - transform.position; //This is the direction for the gun
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //We trnasform an angle to "º"
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); //Now we can evite use quaternions

        //Gun flip
        if (angle > 90) _gunRenderer.flipY = true;
        else _gunRenderer.flipY = false;
    }
}
