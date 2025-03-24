using System.Collections;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float timer=1;
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
            obj.GetComponent<PlayerScript>().TakeDamage();
            Destroy(this.gameObject);
        }
        if(obj.tag == "Enemy" && this.tag != "EnemyProjectile"){
            obj.GetComponent<EnemyScript>().TakeDamage();
            Destroy(this.gameObject);
        }
    }
}
