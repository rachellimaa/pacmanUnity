using UnityEngine;

public class GhostMove : MonoBehaviour {

	[SerializeField] private Rigidbody2D rigidbody2D;
	[SerializeField] private Animator animator;
	[SerializeField] private GameUINavigation uiNav;
	[SerializeField] private PlayerControllerPacman pacman;
	[SerializeField] private float scatterLength;
	[SerializeField] private float waitLength;
	
	private GameManager _gm;
	private float _toggleInterval;
	private float timeToEndWait;
	private float _timeToToggleWhite;
	private bool isWhite = false;
	private float timeToEndScatter;
	private Vector3 _startPos;
	private float _timeToWhite;
	private int cur = 0;
	
	public Transform[] waypoints;
	public float speed;

	enum State { Wait, Init, Scatter, Chase, Run };
	State state;
	
	public Vector3 _direction;
	public Vector3 direction 
	{
		get
		{
			return _direction;
		}

		set
		{
			_direction = value;
			Vector3 pos = new Vector3((int)transform.position.x, (int)transform.position.y, (int)transform.position.z);
		}
	}
	
	private void Start()
	{
		_gm = GameObject.Find("GameManager").GetComponent<GameManager>();
		_toggleInterval = _gm.scareLength * 0.33f * 0.20f;  
		InitializeGhost();
	}

	private void FixedUpdate () {
		
		if(GameManager.gameState == GameManager.GameState.Game){
			Animate();
			Move();
			switch(state)
			{
				case State.Wait:
					Wait();
					break;
				case State.Init:
					Init();
					break;
				case State.Scatter:
					Scatter();
					break;
				case State.Run:
					RunAway();
					break;
			}
		}
	}

	private void Move()
	{
		if (transform.position != waypoints[cur].position) {
			Vector2 p = Vector2.MoveTowards(transform.position,
				waypoints[cur].position,
				speed);
			rigidbody2D.MovePosition(p);
		}
		else cur = (cur + 1) % waypoints.Length;
	}
	
	private void Wait()
	{
		if(Time.time >= timeToEndWait)
		{
			state = State.Init;
		}
	}
	
	private void Init()
	{
		_timeToWhite = 0;
		if(waypoints.Length == 0)
		{
			state = State.Scatter;
			timeToEndScatter = Time.time + scatterLength;
		}
	}
	
	private void Scatter()
	{
		if(Time.time >= timeToEndScatter)
		{
			state = State.Chase;
		}
	}
	
	private void RunAway()
	{
		animator.SetBool("Run", true);

		if(Time.time >= _timeToWhite && Time.time >= _timeToToggleWhite)   ToggleBlueWhite();
	}

	private void Animate()
	{
		Vector2 dir = waypoints[cur].position - transform.position;
		animator.SetFloat("DirX", dir.x);
		animator.SetFloat("DirY", dir.y);
	}
	
	 private void OnTriggerEnter2D(Collider2D other) {
        if (other.name == "pacman"){
           if (state == State.Run)
           {
	           Calm();
	           InitializeGhost(_startPos);
	           pacman.UpdateScore();
           }		       
           else
           {
	           _gm.LoseLife();
           }
        }
           
	 }
	 
	 public void Calm()
	 {
		 if (state != State.Run) return;

		 state = State.Chase;
		 _timeToToggleWhite = 0;
		 _timeToWhite = 0;
		 animator.SetBool("Run_White", false);
		 animator.SetBool("Run", false);
	 }
	 
	 public void ToggleBlueWhite()
	 {
		 isWhite = !isWhite;
		 animator.SetBool("Run_White", isWhite);
		 _timeToToggleWhite = Time.time + _toggleInterval;
	 }
	 
	 public void Frighten()
	 {
		 state = State.Run;
		 _direction *= -1;
		 _timeToWhite = Time.time + _gm.scareLength * 0.66f;
		 _timeToToggleWhite = _timeToWhite;
		 animator.SetBool("Run_White", false);
	 }

	 public void InitializeGhost()
	 {
		 _startPos = getStartPosAccordingToName();
		 state = State.Wait;
		 timeToEndWait = Time.time + waitLength + uiNav.initialDelay;
	 }
	 
	 public void InitializeGhost(Vector3 pos)
	 {
		 transform.position = pos;
		 state = State.Wait;
		 timeToEndWait = Time.time + waitLength + uiNav.initialDelay;
	 }
	 
	 private Vector3 getStartPosAccordingToName()
	 {
		 switch (gameObject.name)
		 {
			 case "blinky":
				 return new Vector3(15f, 20f, 0f);
			 case "pinky":
				 return new Vector3(14.5f, 17f, 0f);
			 case "inky":
				 return new Vector3(16.5f, 17f, 0f);
			 case "clyde":
				 return new Vector3(12.5f, 17f, 0f);
		 }
		 return new Vector3();
	 }
}
