//using UnityEngine;

//public class Projectile : MonoBehaviour {
//	private Rigidbody2D rigidBody;

//	private float timeAlive = 0;

//    //private Animator animator;

//    void Start ()
//	{
//		rigidBody = GetComponent<Rigidbody2D>();
//		//animator = GetComponent<Animator>();
//	}
	
//	void Update ()
//	{
//		timeAlive += Time.deltaTime;
//		if (timeAlive > 2)
//		    Destroy(this.gameObject);
//	}

//	private void Explode() 
//	{
//		rigidBody.velocity = Vector2.zero;
//		GetComponent<PolygonCollider2D>().isTrigger = false;
//		//animator.setTrigger("Explode");
//		Destroy(this.gameObject, 0.1f);
//	}

//	private void OnTriggerEnter2D(Collider2D other)
//	{
//		var tag = other.gameObject.tag;
//		switch (tag)
//		{
//			case "Wall":
//				Explode();
//				break;
//			case "Player":
//				var enemy = other.gameObject.GetComponent<Player>();
//				if (enemy.Invulnerable) return;
//			    var playerId = GetComponentInParent<Player>().Id;
//			    if (enemy.Id != playerId)
//			    {
//			        Explode();
//			        if (!enemy.Dead)
//			        {
//			            enemy.GotHit();
//			            GameController.Instance.UpdateScore(playerId, 1);
//			        }
//			    }
//			    break;
//		}
//	}
//}
