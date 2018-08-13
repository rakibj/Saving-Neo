using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    Transform player;
    public float speed = 5f;
    Light enemyLight;
    public Color lerpedColor;
    public List<Color> colors;
    Color color1, color2;
    public GameObject touchDestroyEffect;

    void Start () {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        enemyLight = GetComponentInChildren<Light>();
        //int ran1, ran2;
        color1 = Random.ColorHSV();
        color2 = colors[Random.Range(0,3)];
    }
	
    
	void Update () {
        MoveTowardsPlayer();
        ChangeColorOverTime();
	}

    void MoveTowardsPlayer()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    private void OnMouseDown()
    {
        if (!GameManager.playerDead)
        {
            FindObjectOfType<AudioManager>().Play("whack");
            Instantiate(touchDestroyEffect, gameObject.transform.position, Quaternion.identity);
            GameManager.score++;
            gameObject.SetActive(false);
        }
    }

    void ChangeColorOverTime()
    {
        lerpedColor = Color.Lerp(color1, color2, Mathf.PingPong(Time.time, 1));
        enemyLight.color = lerpedColor;
    }
}
