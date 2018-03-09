using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour {

	private NavMeshAgent agent;
	[HideInInspector]
	public Transform player;	// Assign in WaveSystem

	private void Start() {
		agent = GetComponent<NavMeshAgent>();
		StartCoroutine(Movement());
	}

	private IEnumerator Movement() {
		yield return new WaitForSeconds(1f);
		agent.destination = player.position;
	}
	
}
