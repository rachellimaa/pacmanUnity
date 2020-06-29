using UnityEngine;

public class Points : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Start () 
    {
        Destroy(gameObject, 1.5f);
    }
	
    private void Update () 
    {
        transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
        speed -= 0.01f;
    }
}
