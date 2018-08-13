using UnityEngine;

public class SlowPickUpScript : MonoBehaviour {

    private void OnMouseDown()
    {
        FindObjectOfType<AudioManager>().Play("gotpoint");
        Destroy(this.gameObject);
        GameManager.slowMotion++;
    }
}
