using UnityEngine;
using EZCameraShake;


public class PlayerScript : MonoBehaviour {

    Light playerLight;
    public Color lerpedColor = Color.white;
    public GameObject destroyEffect;
    public GameObject playerDestroyEffect;
    public GameObject playerDieEffect;
    Animator playerAnimator;
    
    

    void Start () {
        playerLight = GetComponentInChildren<Light>();
        playerAnimator = this.GetComponent<Animator>();
	}
	
	void Update () {
        ChangeColorOverTime();
        CheckDeath();
    }

    void CheckDeath()
    {
        if(GameManager.playerHealth <= 0)
        {
            GameManager.playerHealth = 1;
            if (GameManager.elligibleForReward)
            {
                GameManager.giveRewardChance = true;
                GameManager.elligibleForReward = false;
            }
            else
            {
                
                GameManager.playerDead = true;
            }
            this.gameObject.SetActive(false);
            CameraShaker.Instance.ShakeOnce(8f, 8f, .1f, 1f);
        }
    }

    void ChangeColorOverTime()
    {
        lerpedColor = Color.Lerp(Color.gray, Color.white, Mathf.PingPong(Time.time, 1));
        playerLight.color = lerpedColor;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            FindObjectOfType<AudioManager>().Play("playerhit");
            playerAnimator.SetTrigger("Hit");
            CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
            GameManager.playerHealth--;
            if (GameManager.playerHealth > 0)
            {
                Instantiate(destroyEffect, collision.gameObject.transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(playerDestroyEffect, collision.gameObject.transform.position, Quaternion.identity);
                Instantiate(playerDieEffect, transform.position, Quaternion.identity);
            }
            collision.gameObject.SetActive(false);


        }    
    }

}
