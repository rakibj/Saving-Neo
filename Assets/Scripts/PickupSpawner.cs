using UnityEngine;

public class PickupSpawner : MonoBehaviour {

    public int minX, maxX, y;
    public int chanceOfBlast = 5;
    public GameObject pickupBlast;
    public float waitTimeMinBlast, waitTimeMaxBlast, waitTimeMinSlow, waitTimeMaxSlow;
    public GameObject pickupSlow;
    private int spawnX, spawnY;
    private int direction;
    private float timerBlast, timerSlow;
    // Use this for initialization
    void Start () {
        timerBlast = Random.Range(waitTimeMinBlast, waitTimeMaxBlast);
        timerSlow = Random.Range(waitTimeMinSlow, waitTimeMaxSlow);
	}
	
	// Update is called once per frame
	void Update () {
        if (!GameManager.gameStarted) return;
        timerBlast -= Time.deltaTime;
        timerSlow -= Time.deltaTime;
        if(timerBlast <= 0)
        {
            int dirran;
            if (Random.Range(0, 11) % 2 == 0)
            {
                dirran = 1;
            }
            else
            {
                dirran = -1;
            }
            timerBlast = Random.Range(waitTimeMinBlast, waitTimeMaxBlast);
            spawnX = Random.Range(minX, maxX + 1);
            spawnY = Random.Range(-y, y);
            Vector3 spawnPosition = new Vector3(spawnX * dirran, spawnY, -1f);
            FindObjectOfType<AudioManager>().Play("pickup");
            GameObject go = Instantiate(pickupBlast, spawnPosition, Quaternion.identity);
            Debug.Log("Blast Pickup instantiated");
            Destroy(go, 2f);
        }

        if(timerSlow <= 0)
        {
            int dirran;
            if (Random.Range(0, 11) % 2 == 0)
            {
                dirran = 1;
            }
            else
            {
                dirran = -1;
            }
            timerSlow = Random.Range(waitTimeMinSlow, waitTimeMaxSlow);
            spawnX = Random.Range(minX, maxX + 1);
            spawnY = Random.Range(-y, y);
            Vector3 spawnPosition = new Vector3(spawnX * dirran, spawnY, -1f);
            FindObjectOfType<AudioManager>().Play("pickup");
            GameObject go = Instantiate(pickupSlow, spawnPosition, Quaternion.identity);
            Debug.Log("Slow Pickup instantiated");
            Destroy(go, 2f);
        }
	}

}
