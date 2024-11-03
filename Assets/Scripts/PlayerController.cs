using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float JumpHeight;

    private Rigidbody2D _rigidbody;

    private bool isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
        float move = Input.GetAxis("Horizontal");
        position.x = position.x + speed * move *Time.deltaTime;
        transform.position=position;        

        if (!isJumping && Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.velocity=Vector3.zero;
            _rigidbody.AddForce( new Vector2(0, Mathf.Sqrt(-2 * Physics2D.gravity.y * JumpHeight)),ForceMode2D.Impulse);
            isJumping=true;
        }  
    }  

    private void OnCollisionEnter2D()
    {
        isJumping=false;
    }    
}
