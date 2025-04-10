// using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

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

    public int hp=10;
    public int maxHP=10;

    int score;
    float gameTimer;
    public Image lifebar;
    public TextMeshProUGUI lifeText, scoreText;
    public TextMeshProUGUI newScoreText;

    public GameObject explosion;
    public GameObject menu;
    public GameObject pauseMenu;
    bool pause = false, dead = false;

    void Start(){
        updateUI();
        score=0;
    }

    void Update(){
      updateUI();
      gameTimer += Time.deltaTime;
      Movement();
      shootTimer += Time.deltaTime;
      Shooting();
      if(Input.GetButtonDown("Cancel")){
        pause = !pause;
        if(pause && !dead){
          pauseMenu.SetActive(true);
          Time.timeScale = 0;
        }else if(!dead){
          pauseMenu.SetActive(false);
          Time.timeScale = 1;
        }
      }
    }

    public void AddScore(int value = 20){
      score += value;
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

      if(transform.position.x > screenLimit.x)transform.position = new Vector3(screenLimit.x, transform.position.y);

      if(transform.position.x < -screenLimit.x)transform.position = new Vector3(-screenLimit.x, transform.position.y);

      if(transform.position.y > screenLimit.y)transform.position = new Vector3(transform.position.x, -screenLimit.y + .2f);

      if(transform.position.y < -screenLimit.y)transform.position = new Vector3(transform.position.x, screenLimit.y - +.2f);
    }

    void updateUI(){
      lifebar.fillAmount = (float)hp / maxHP;
      lifeText.text = hp + "/" + maxHP;
      scoreText.text = "Score: " + ((int)gameTimer + score);
    }

    public void TakeDamage(int damage=1){
      if(damage < 0) return;
      if(hp - damage>0) hp -= damage;
      else{
        hp=0;
        Die();
      }
      updateUI();
    }
    void Die(){
      dead = true;
      menu.SetActive(true);
      if(explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);
      hp=maxHP;
      transform.position = Vector2.zero;

      int oldScore = PlayerPrefs.GetInt("Score");
      int newScore = (int)gameTimer + score;
      if(newScore >= oldScore){
        PlayerPrefs.SetInt("Score",newScore);
      }
      if(newScoreText != null) newScoreText.text = "Sua Pontuação: " + newScore.ToString() + "\nPontuação Máxima: " + PlayerPrefs.GetInt("Score");

      Time.timeScale = 0;
    }
}
