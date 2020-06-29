using UnityEngine;

public class PacdotSpawned : MonoBehaviour
{
    [SerializeField] private GameObject pacdot;
    [SerializeField] private float interval;
    [SerializeField] private float startOffset;

    private float startTime;

    private void Start () 
    {
        startTime = Time.time + startOffset;
    }
	
    private void Update () 
    {
        if(Time.time > startTime + interval)
        {
            GameObject obj = (GameObject)Instantiate(pacdot, transform.position, Quaternion.identity);
            obj.transform.parent = transform;

            startTime = Time.time;
        }
    }
}
