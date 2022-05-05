using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
  protected int health;
  protected int damage;
  protected float speed;
  protected float gunRange;
  protected float lookRange;
  protected float spread;
  
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
    float spreadX = Random.Range(-spread, spread);
    float spreadY = Random.Range(-spread, spread);
    Vector3 spreadDirection = shootPoint.transform.forward + new Vector3(spreadX, spreadY, 0f);
    if (Physics.Raycast(spreadDirection, shootPoint.transform.forward, out hit, gunRange)) {
      if (hit.transform.tag == "Player") {
        DealDamage(damage, hit.transform);
      }
    }
  }

}
