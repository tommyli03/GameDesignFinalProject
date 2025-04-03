using UnityEngine;
using System.Collections;

public class DWGDestroyer : MonoBehaviour {

	public float radius = 2;
	public float force = 50f;
	public bool canBreakWalls = true;
	
	void OnCollisionEnter(Collision col){
			if (canBreakWalls) {
				ExplodeForce();
			}
			Destroy(GetComponent<DWGDestroyer>());
	}
	
	// Explode force by radius only if a destructible tag is found
	void ExplodeForce(){	
		/*Vector3 explodePos = transform.position;
		Collider[] colliders = Physics.OverlapSphere(explodePos, radius); 		
		foreach (Collider hit in colliders){
			Rigidbody rb = hit.GetComponent<Rigidbody>();
			if (rb == null) continue;
			GameObject obj = rb.gameObject;

			if (obj.CompareTag("Destructible"))
			{
				Durability durability = obj.GetComponent<Durability>();
				if (durability != null) {
					durability.TakeDamage(damage, explodePos, force, radius);
				} else {
					// fallback — break immediately if no durability component
					rb.isKinematic = false;
					rb.AddExplosionForce(force, explodePos, radius);
				}
			}
		}*/
	}
}
