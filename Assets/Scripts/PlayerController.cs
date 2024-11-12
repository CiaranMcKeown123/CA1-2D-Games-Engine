using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // [SerializeField] GameObject projectilePrefab;
    [SerializeField] private float speed;
    [SerializeField] private float JumpHeight;

    private Rigidbody2D _rigidbody;
    private bool isJumping = false;

    int coins = 0;
    public int state = 0;
    public int playerLives=3;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
        float move = Input.GetAxis("Horizontal");
        position.x = position.x + speed * move *Time.deltaTime;
        transform.position=position; 

        // if(Input.GetKeyDown(KeyCode.F))
        // {
        //     GameObject projectileObject = Instantiate(projectilePrefab, GetComponent<Rigidbody2D>().position, Quaternion.identity);
        //     Projectile projectile = projectileObject.GetComponent<Projectile>();
        //     state = move <0 ? -1 : 1;
        //     Projectile.Launch(new Vector2(position.x, 0), 300);
        // }

        if (!isJumping && Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.velocity=Vector3.zero;
            _rigidbody.AddForce( new Vector2(0, Mathf.Sqrt(-2 * Physics2D.gravity.y * JumpHeight)),ForceMode2D.Impulse);
            isJumping=true;
        }  

        if (move != 0 && !isJumping)
        {
            state = move <0 ? -1 : 1;
            animator.SetFloat("Move X", state);
        }

        else if(move !=0 && isJumping)
        {
            state = move <0 ? -1 : 1;
            animator.SetFloat("Move X", state);
            animator.SetFloat("Move Y", 1);
        }

        else 
        {
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", 0);
        }
    }  

    private void OnCollisionEnter2D()
    {
        isJumping=false;
        animator.SetFloat("Move Y", 0);
    }    

    public void AddCoin()
    {
        coins++;
    }

    public void LifeLoss()
    {
        if(playerLives!=0)
        playerLives--;

        else
        PlayerDeath();
    }

    public void PlayerDeath()
    {
        Destroy(this.gameObject);
    }
}
