using UnityEngine;
using UnityEngine.SocialPlatforms;
using System.Collections;
using JetBrains.Annotations;

public class AIEnemy : MonoBehaviour {

	[Header("Настройка AI")]
	public float playerTargetDistance;
	public float enemyLookDistance;
	public float attackDistance;
	public float enemyMovementSpeed;
	public float damping;

	[Header("Пули бота")]
	[SerializeField] private GameObject bulletPrefabs;


	[Header("Объект игрока")]
	[SerializeField] private Transform _playerTarget;

	

	Rigidbody _theRigidbody;
	Renderer _myRender;



	// Use this for initialization
	void Start () {
		
		_myRender = GetComponent<Renderer>();
		_theRigidbody = GetComponent<Rigidbody>();
	}
	
	
	void FixedUpdate () {

		playerTargetDistance = Vector3.Distance(_playerTarget.position, transform.position);
		if (playerTargetDistance < enemyLookDistance)
		{
			lookAtPlayer();
		}
		if (playerTargetDistance < attackDistance)
		{
			_myRender.material.color = Color.red;
			attackPlease();
			
		}
		// оставновился и спокоен
		else
		{
			_myRender.material.color = Color.blue;
		}
	}
	
	// враг увидел
	void lookAtPlayer(){
		Quaternion rotation = Quaternion.LookRotation(_playerTarget.position - transform.position);
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime*damping);
	}

	// враг атакует
	void attackPlease(){
		_theRigidbody.AddForce(transform.forward * enemyMovementSpeed);
		//GetComponent<Rigidbody>().AddForce(transform.forward * 3500);

	}

}
