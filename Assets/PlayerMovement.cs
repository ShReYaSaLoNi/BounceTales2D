using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

[RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float maxRollSpeed;
    public float accelerationSpeed;
    public float decelerationSpeed;
    public float rollDegreeMultiplier;
    public float jumpSpeed ;
    public LayerMask groundLayer;
    // Start is called before the first frame update

    Rigidbody2D rb;
    CircleCollider2D cc;

    float circumference;
    bool isGrounded;

    //public int coin = 0;

    //[SerializeField] private Text cointext;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CircleCollider2D>();
        circumference = 2* Mathf.PI * cc.radius;
    }

    // Update is called once per frame
    void Update()
    {
     isGrounded = cc.IsTouchingLayers(groundLayer);
     Move(Input.GetAxisRaw("Horizontal")); 
     AnimationUpdate();

     if(Input.GetKeyDown(KeyCode.Space)){
         Jump();
     }
    }


    void AnimationUpdate(){
        float speed = rb.velocity.magnitude;
        if(speed > 0){
            transform.Rotate(0,0, (speed * -Mathf.Sign(rb.velocity.x) * Time.deltaTime / circumference) * 360f * rollDegreeMultiplier);
        }
    }

    void Move(float h){
        if(h != 0){
            rb.velocity += h* accelerationSpeed * Vector2.right * Time.deltaTime;
            if(rb.velocity.x > maxRollSpeed || rb.velocity.x < -maxRollSpeed)
            {
                rb.velocity = new Vector2(h * maxRollSpeed, rb.velocity.y);
            }
            else if(rb.velocity.x != 0){
                float deceleratedSpeed = Mathf.Lerp(rb.velocity.x, 0, decelerationSpeed * Time.deltaTime);
                rb.velocity = new Vector2(deceleratedSpeed, rb.velocity.y);
            }
        }
    }

    void Jump(){
        if(!isGrounded) return;
        List<ContactPoint2D> contacts = new List<ContactPoint2D>();
        cc.GetContacts(contacts);
        rb.velocity += contacts[0].normal * jumpSpeed;
    }
}
