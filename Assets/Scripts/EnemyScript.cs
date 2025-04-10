using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    public float speed = 1;
    public Vector2 screenLimit;
    int direction = 1;   
    public GameObject projectile;
    public float shootDistance=1;
    public float shootSpeed=300;
    public float shootCD=.8f;
    public Vector3 shootDirection = Vector3.left;
    float shootTimer=0;

    int hp=5;
    public int maxHP=10;
    public GameObject explosion;
		public int scoreBonus = 20;
    
    void Start()
    {
        hp=maxHP;
    }

    void Update()
    {
        shootTimer += Time.deltaTime;
        Move();
        Shoot();
    }

    void Move(){
        transform.Translate(new Vector2(-speed * Time.deltaTime, direction * speed * 2 * Time.deltaTime)); 
        if(transform.position.y > screenLimit.y || transform.position.y < -screenLimit.y){
            direction *= -1;
            transform.position = new Vector2(transform.position.x, Mathf.Sign(transform.position.y)*screenLimit.y);
        }
        if(transform.position.x < -screenLimit.x) transform.position = new Vector2(screenLimit.x, transform.position.y);
    }

    void Shoot(){
        if(shootTimer > shootCD){
            GameObject shoot = Instantiate(projectile);
            shoot.transform.position = transform.position + Vector3.left * shootDistance;
            shoot.transform.up = shootDirection.normalized;
            shoot.GetComponent<Rigidbody2D>().AddForce(shootDirection.normalized * shootSpeed);
            shootTimer=0;
        }
    }
    public void TakeDamage(int damage=1){
      if(damage<0) return;
      if(hp-damage>0) hp -= damage;
      else{
        hp=0;
        Die();
      }
    }
    void Die(){
      if(explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);

			try{
				FindFirstObjectByType<PlayerScript>().AddScore(scoreBonus);
			}
			catch{

			}

      hp=maxHP;
      transform.position = Vector2.zero;
      transform.position = new Vector2(screenLimit.x, transform.position.y);
    }
}
