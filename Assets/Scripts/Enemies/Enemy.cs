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
    float spreadX = Random.Range(-spread, spread);
    float spreadY = Random.Range(-spread, spread);
    Vector3 spreadDirection = shootPoint.transform.forward + new Vector3(spreadX, spreadY, 0f);
    Debug.DrawLine(shootPoint.transform.forward, spreadDirection, Color.red, 40);
    Debug.Log("Dispara?");
    if (Physics.Raycast(shootPoint.transform.forward, spreadDirection, out hit, gunRange)) {
      Debug.Log(hit.transform.tag);
      if (hit.transform.tag == "Player") {
        DealDamage(damage, hit.transform);
      }
    }
  }

}
