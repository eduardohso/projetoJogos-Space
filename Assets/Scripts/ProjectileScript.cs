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
    void Update()
    {
        
    }
}
