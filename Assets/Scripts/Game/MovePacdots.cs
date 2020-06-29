using UnityEngine;

public class MovePacdots : MonoBehaviour
{
    [SerializeField] private float speed;
	
    private void Update () 
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(gameObject);
    }
}
