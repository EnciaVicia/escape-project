using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
  [SerializeField]protected int health;
  [SerializeField]protected int damage;
  [SerializeField]protected float speed;
  [SerializeField]protected float gunRange;
  [SerializeField]protected float lookRange;
  [SerializeField]protected float spread;
  
  public enum RebelActivity {
    Idle,
    Patrol,
    Chase,
    Attack,
    Dead
  }

  public Transform shootPoint;

  protected virtual void TakeDamage(int amount)
  {
    health -= amount;
  }
  
  protected virtual void DealDamage(int amount, Transform player)
  {
    player.transform.gameObject.GetComponent<Player>().TakeDamage(amount);
  }

  protected virtual void Shoot()
  {
    RaycastHit hit;
    Debug.DrawRay(shootPoint.transform.position, shootPoint.transform.forward, Color.red, 40);
    if (Physics.Raycast(shootPoint.transform.position, shootPoint.transform.forward, out hit, gunRange)) {
      Debug.Log(hit.transform.tag);
      if (hit.transform.tag == "Player") {
        DealDamage(damage, hit.transform);
      }
  }
  }
}
