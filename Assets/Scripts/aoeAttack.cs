using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aoeAttack : MonoBehaviour {

	public GameObject aoePrefab;
	public float projSpeed;
	public float projDestroTime;
	public float cooldown;

	private GameObject[] instantiatedObjects = new GameObject[8];
	private float attackTimer = 3.0f;
	private bool isAttacking = false;

	void Start() {
		aoePrefab.transform.localScale = new Vector3 (0.05f, 0.05f, 1f);
	}

	void Update() {
		attackTimer += Time.deltaTime;
		if (Input.GetMouseButtonDown(0) && attackTimer >= cooldown && !isAttacking) {
			isAttacking = true;
			StartCoroutine(attack());
		}
	}

	IEnumerator attack() {
		for (int count = 0; count < 8; count++) {
			instantiatedObjects[count] = Instantiate(aoePrefab, transform.position, Quaternion.identity);
			Vector3 forceVector = new Vector3(Mathf.Cos(count * 15), Mathf.Sin(count * 15), 0);
			Rigidbody2D shotRb = instantiatedObjects[count].GetComponent<Rigidbody2D>();
			shotRb.AddForce (forceVector.normalized * projSpeed);
		}
		yield return new WaitForSeconds(projDestroTime);
		for (int count = 0; count < 8; count++) {
			Destroy(instantiatedObjects[count]);
		}
		attackTimer = 0.0f;
		isAttacking = false;
	}
}
