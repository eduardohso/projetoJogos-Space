using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExplosionScript : MonoBehaviour
{
    
  public float timer = 0.2f;
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
