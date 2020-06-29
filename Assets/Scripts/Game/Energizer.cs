using UnityEngine;

public class Energizer : MonoBehaviour {
    private GameManager _gm;

    private void Start ()
    {
        _gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        if( _gm == null )    Debug.Log("Energizer did not find Game Manager!");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "pacman")
        {
            _gm.ScareGhosts();
            Destroy(gameObject);
        }
    }
}
