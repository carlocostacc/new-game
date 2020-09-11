using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{

    public float move_speed;
    private Vector2 move_input;
    public Rigidbody2D theRB;
    public Transform stafarm;
    private Camera theCam;
    public Animator anim;
    public GameObject bulletTofire;
    public Transform firepoint;
    public float timebetweenshots;
    private float shotcounter;


    // Start is called before the first frame update
    void Start()
    {
        theCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //getaxis does acceleration
        move_input.x = Input.GetAxisRaw("Horizontal");
        move_input.y = Input.GetAxisRaw("Vertical");


        move_input.Normalize();
        //transform.position += new Vector3(move_input.x * Time.deltaTime * move_speed, move_input.y * Time.deltaTime * move_speed, 0f);
        theRB.velocity = move_input * move_speed;

        Vector3 mousePos = Input.mousePosition;
        Vector3 screenPoint = theCam.WorldToScreenPoint(transform.localPosition);

        if (mousePos.x < screenPoint.x){
            transform.localScale = new Vector3(-1f, 1f,1f);
            stafarm.localScale = new Vector3(-1F,1F,1F);
        }
        else {
            transform.localScale = Vector3.one;
            stafarm.localScale = Vector3.one;
        }



        //unstable rotation


        /*
        Vector2 offset = theCam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(offset.y ,offset.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        stafarm.rotation = Quaternion.Slerp(transform.rotation, rotation, rotation_speed * Time.deltaTime);
        */


        // fix rotation of weapon to follow mouse position 
        // rotation gun arm
        Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y ,offset.x) * Mathf.Rad2Deg;
        stafarm.rotation = Quaternion.Euler(0, 0, angle - 18);

        if (Input.GetMouseButtonDown(0)){
            Instantiate(bulletTofire, firepoint.position, firepoint.rotation);
            shotcounter = timebetweenshots;
        }


        // print finction to console **Debug.Log("Text: " + myText.text); **
        Debug.Log("angle: " + -1*angle);

        if (move_input != Vector2.zero){
            anim.SetBool("isMoving",true);
        }
        else{
            anim.SetBool("isMoving", false);
        }
        if(Input.GetMouseButton(0)){
            shotcounter -= Time.deltaTime; 

            if (shotcounter <= 0){
                 Instantiate(bulletTofire, firepoint.position, firepoint.rotation);
                 shotcounter = timebetweenshots;
            }
        }
    }
}
