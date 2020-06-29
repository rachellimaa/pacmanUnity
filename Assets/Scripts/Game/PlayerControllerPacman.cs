using System;
using System.Collections;
using UnityEngine;

public class PlayerControllerPacman : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rigidbodyPacman;
    [SerializeField] private Animator animatorPacman;
    private Vector2 _dest = Vector2.zero;  
    private Vector2 _dir = Vector2.zero;
    private Vector2 _nextDir = Vector2.zero;
    
    [Serializable]
    public class PointSprites
    {
        public GameObject[] pointSprites;
    }
    
    [SerializeField] private PointSprites points;
    
    public static int killstreak = 0;

    private GameUINavigation GUINav;
    private GameManager GM;

    private bool _deadPlaying = false;
    
    private void Start () {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        GUINav = GameObject.Find("UIManager").GetComponent<GameUINavigation>();
        _dest = transform.position;
    }
  
    private void FixedUpdate () {
        switch (GameManager.gameState)
        {
            case GameManager.GameState.Game:
                Move();
                Animate();
                break;
            case GameManager.GameState.Dead:
                if (!_deadPlaying)
                    StartCoroutine("PlayDeadAnimation");
                break;
        }
    }

    void Move()
    {
        // move closer to destination
        Vector2 p = Vector2.MoveTowards(transform.position, _dest, speed);
        GetComponent<Rigidbody2D>().MovePosition(p);

        // get the next direction from keyboard
        if (Input.GetAxis("Horizontal") > 0) _nextDir = Vector2.right;
        if (Input.GetAxis("Horizontal") < 0) _nextDir = -Vector2.right;
        if (Input.GetAxis("Vertical") > 0) _nextDir = Vector2.up;
        if (Input.GetAxis("Vertical") < 0) _nextDir = -Vector2.up;

            if (Valid(_nextDir))
            {
                _dest = (Vector2)transform.position + _nextDir;
                _dir = _nextDir;
            }
            else   // if next direction is not valid
            {
                if (Valid(_dir))  // and the prev. direction is valid
                    _dest = (Vector2)transform.position + _dir;   // continue on that direction
            }
    }
    
    private bool Valid(Vector2 direction)
    {
        // cast line from 'next to pacman' to pacman
        // not from directly the center of next tile but just a little further from center of next tile
        Vector2 pos = transform.position;
        direction += new Vector2(direction.x * 0.45f, direction.y * 0.45f);
        RaycastHit2D hit = Physics2D.Linecast(pos + direction, pos);
        return hit.collider.name == "pacdot" || (hit.collider == GetComponent<Collider2D>());
    }
    
    private void Animate()
    {
        Vector2 dir = _dest - (Vector2) transform.position;
        animatorPacman.SetFloat("DirX", dir.x);
        animatorPacman.SetFloat("DirY", dir.y);
    }

    public void UpdateScore()
    {
        killstreak++;
        if (killstreak > 4) 
            killstreak = 4;
        Instantiate(points.pointSprites[killstreak - 1], transform.position, Quaternion.identity);
        GameManager.score += (int)Mathf.Pow(2, killstreak) * 100;
    }
    
    IEnumerator PlayDeadAnimation()
    {
        _deadPlaying = true;
        animatorPacman.SetBool("Die", true);
        yield return new WaitForSeconds(1);
        animatorPacman.SetBool("Die", false);
        _deadPlaying = false;

        if (GameManager.lives <= 0)
        {
            GUINav.HandlerShowGameOverScreen();
        }else
            GM.ResetScene();
    }
    
    public void ResetDestination()
    {
        _dest = new Vector2(15f, 11f);
        GetComponent<Animator>().SetFloat("DirX", 1);
        GetComponent<Animator>().SetFloat("DirY", 0);
    }
 
}