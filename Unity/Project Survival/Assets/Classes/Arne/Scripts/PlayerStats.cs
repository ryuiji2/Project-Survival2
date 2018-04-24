using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

	//weapons
	public List<GameObject> weapons = new List<GameObject>();
	public GameObject currentWeapon;

	//health 
	public float maxHP;
	public float health;
	public float healthPercentage;
	public UIManager uim;

	public Vector3 startPos;
    
    private HashSet<Collider> attackedBy = new HashSet<Collider>();

	private void Awake () {

		//uim = GameObject.FindWithTag("Canvas").GetComponent<UIManager>(); //gives error needs to be manually put in
		maxHP = 100;
		health = maxHP;
		startPos = transform.position;
	}
	public void PlayerHealth (float dmg) {

		health -= dmg;
		healthPercentage = health / maxHP;
		if(health <= 0f) {
            
			uim.SetState(UIManager.UIState.GameOver);
		}
		uim.CheckHealth();
	}
	public void PlayerReset () {

		transform.position = startPos;
		health = maxHP;
		PlayerHealth(0f);
		//playeranimator disable
	}

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Damage" /*&& !attackedBy.Contains(other)*/)
        {
            //attackedBy.Add(other);
            GetComponent<Regeneration>().EnemyAttack();
            PlayerHealth(other.GetComponent<Attack>().damage);
        }
    }

    public IEnumerator DamageSpam (Collider col)
    {
        yield return new WaitForSeconds(1);
        if (attackedBy.Contains(col))
        {
            attackedBy.Remove(col);
        }
        else print("Doesn't work!");
    }
}
