using UnityEngine;
using UnityEngine.SocialPlatforms;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine.AI;

public class AIEnemy : MonoBehaviour {

	[Header("Настройка AI")]
	public float playerTargetDistance;
	public float enemyLookDistance;
	public float attackDistance;
	public float enemyMovementSpeed;
	public float damping;
	private NavMeshAgent myAgent;
	Renderer _myRender;

	public GameObject prefabBullet;
	private float timeBtwShots;
	public float startTimeBtwShots;

	public float shootDelay;
	public float fromLastShoot;


	[SerializeField] private Cube _playerTarget;

	void Start () {
		myAgent = GetComponent<NavMeshAgent>();
		_myRender = GetComponent<Renderer>();

		_playerTarget = FindObjectOfType<Cube>();
		timeBtwShots = startTimeBtwShots;
	}
	
	void FixedUpdate () 
	{
		if(_playerTarget != null)
			UpdateAi();	
	}

	private void UpdateAi()
	{
		fromLastShoot += Time.deltaTime;
		var playerPos = _playerTarget.transform.position;
		myAgent.destination = playerPos;
		playerTargetDistance = Vector3.Distance(playerPos, transform.position);
		if (playerTargetDistance > 40f)
		{
			myAgent.Stop();
		}
		else
		{
			if (playerTargetDistance < 20f)
			{

				myAgent.Stop();
			}
			else
			{
				AttackBot();
				myAgent.Resume();
			}
		}
	}
	/*private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Enemy"))
		{
			StartCoroutine(Die());
		}
	}

	private IEnumerator Die()
	{
		transform.Rotate(-75, 0, 0);

		yield return new WaitForSeconds(1.5f);
		Destroy(gameObject);
	}*/
	
	private void AttackBot()
	{
		if(fromLastShoot >= shootDelay)
		{
			Shoot();
			fromLastShoot = 0;
		}
	}

	private void Shoot()
	{
		Instantiate(prefabBullet, transform.position, Quaternion.identity);
	}
}
