using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed = 1;
    public Vector2 screenLimit;
    public GameObject Projectile;
    public Transform[] ShootPosition;
    public Vector2[] shootDirection;
    public float shootSpeed=20;
    public float shootCD=.5f;
    float shootTimer=0;

    void Start(){

    }

    void Update(){
      Movement();
      shootTimer += Time.deltaTime;
      Shooting();
    }

    void Shooting(){
      if(Input.GetAxisRaw("Jump")!= 0 && shootTimer>= shootCD){
        for(int i=0;i<ShootPosition.Length;i++){
          GameObject shoot = Instantiate(Projectile);
          shoot.transform.position= ShootPosition[i].position;
          shoot.transform.up=shootDirection[i].normalized;
          shoot.GetComponent<Rigidbody2D>().AddForce(shootDirection[i].normalized*shootSpeed);
          shootTimer=0;
        }
      }
    }

    void Movement(){
      float hMove = Input.GetAxisRaw("Horizontal");
      float vMove = Input.GetAxisRaw("Vertical");

      transform.Translate(new Vector3(hMove, vMove).normalized*speed*Time.deltaTime);

      if(transform.position.x > screenLimit.x)transform.position = new Vector3(-screenLimit.x + .2f, transform.position.y);

      if(transform.position.x < -screenLimit.x)transform.position = new Vector3(screenLimit.x - .2f, transform.position.y);

      if(transform.position.y > screenLimit.y)transform.position = new Vector3(transform.position.x, -screenLimit.y + .2f);

      if(transform.position.y < -screenLimit.y)transform.position = new Vector3(transform.position.x, screenLimit.y - +.2f);
    }
}
