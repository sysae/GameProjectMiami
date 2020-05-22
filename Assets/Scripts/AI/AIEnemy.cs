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

	[SerializeField] private Transform _playerTarget;

	void Start () {
		myAgent = GetComponent<NavMeshAgent>();
		_myRender = GetComponent<Renderer>();

		timeBtwShots = startTimeBtwShots;
	}
	
	void FixedUpdate () {
		UpdateAi();
		
	}

	private void UpdateAi()
	{
		myAgent.destination = _playerTarget.position;
		playerTargetDistance = Vector3.Distance(_playerTarget.position, transform.position);
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
		if (timeBtwShots <= 0)
		{
			Instantiate(prefabBullet, transform.position, Quaternion.identity);
			timeBtwShots = startTimeBtwShots;
		}
		else
		{
			timeBtwShots -= Time.deltaTime;
		}
	}
}
