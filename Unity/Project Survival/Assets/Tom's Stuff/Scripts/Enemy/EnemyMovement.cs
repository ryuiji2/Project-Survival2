using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour {

	private NavMeshAgent agent;
	[HideInInspector]
	public Transform player;	// Assign in WaveSystem
	private Wave wave;

	private void Start() {

		wave = GameObject.Find("WaveManager").GetComponent<Wave>();
		player = wave.player;
		agent = GetComponent<NavMeshAgent>();
		StartCoroutine(Movement());
	}
	public void FixedUpdate () {

		agent.destination = player.position;
	}
	private IEnumerator Movement() {  //werkt niet

		yield return new WaitForSeconds(1f);
		//agent.destination = player.position;
	}
	
}
