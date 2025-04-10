using System.Collections;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float timer=1;
    public GameObject explosion;
    void Start()
    {
        StartCoroutine(DestroySelf());
    }

    IEnumerator DestroySelf(){
        yield return new WaitForSeconds(timer);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if(obj.tag == "Player" && this.tag != "PlayerProjectile"){
            if(explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);
            obj.GetComponent<PlayerScript>().TakeDamage();
            Destroy(this.gameObject);
        }
        if(obj.tag == "Enemy" && this.tag != "EnemyProjectile"){
            if(explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);
            obj.GetComponent<EnemyScript>().TakeDamage();
            Destroy(this.gameObject);
        }
    }
}
