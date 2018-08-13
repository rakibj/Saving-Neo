using UnityEngine;

public class BombPickUpScript : MonoBehaviour {

    private void OnMouseDown()
    {
        FindObjectOfType<AudioManager>().Play("gotpoint");
        Destroy(this.gameObject);
        GameManager.blast++;
    }
}
