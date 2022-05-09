using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
  public int damage;
  public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
  public int bulletsPerTap;
  public List<AudioClip> weaponSFX;
  public AudioSource shotSource;
  public int currentMagazine = 0;
  public List<int> ammo = new List<int>();
  public bool allowHoldButton;
  int ammoLeft, ammoShot, ammoToDisplay;
  bool isShooting, isReadyToShoot, isReloading;
  public Camera fpsCam;
  public Transform attackPoint;
  public RaycastHit rayHit;
  public LayerMask enemyMask;
  
  private void Start() {
    ammoLeft = ammo[currentMagazine];
    ammoToDisplay = ammoLeft;
    isReadyToShoot = true;
    shotSource = gameObject.AddComponent<AudioSource>();
  }
  
  private void Update() {
    PlayerInput();
  }
  private void PlayerInput() {
    if (allowHoldButton) isShooting = Input.GetKey(KeyCode.Mouse0);
    else isShooting = Input.GetKeyDown(KeyCode.Mouse0);

    if (Input.GetKeyDown(KeyCode.R) && ammoLeft < ammo[currentMagazine] && !isReloading && ammo.Count > 1) Reload();

    if (isReadyToShoot && isShooting && !isReloading && ammoLeft > 0) { 
      ammoShot = bulletsPerTap;
      Shoot();
    }
  }

  private void Shoot() {
    isReadyToShoot = false;
    float spreadX = Random.Range(-spread, spread);
    float spreadY = Random.Range(-spread, spread);

    Vector3 spreadDirection = fpsCam.transform.forward + new Vector3(spreadX, spreadY, 0f);
    Debug.DrawRay(fpsCam.transform.position, spreadDirection, Color.blue, 30);
    if (Physics.Raycast(fpsCam.transform.position, spreadDirection, out rayHit, range)) {
      if (rayHit.collider.CompareTag("Enemy")) {
        rayHit.collider.GetComponent<Rebel_Recruit>().TakeDamage(damage);
      }
    }
    ammoLeft--;
    ammoShot--;
    ammoToDisplay = ammoLeft;
    PlaySound(weaponSFX, 0);
    Invoke("ResetShot", timeBetweenShooting);
    // IF para disparar en rafagas
    if (ammoShot > 0 && ammoLeft > 0) {
      Invoke("Shoot", timeBetweenShots);
    }
  }

  private void ResetShot() {
    isReadyToShoot = true;
  }

  private void Reload() {
    isReloading = true;
    PlaySound(weaponSFX, 1);
      Invoke("ReloadFinished", reloadTime);
  }

  private void ReloadFinished() {
    ammo[currentMagazine] = ammoLeft;
    if (ammo[currentMagazine] <= 0) {
      ammo.RemoveAt(currentMagazine);
    }
    if (currentMagazine >= ammo.Count) {
      currentMagazine++;
    } else {
      currentMagazine = 0;
    }
    ammoLeft = ammo[currentMagazine];
    ammoToDisplay = ammoLeft;
    isReloading = false;
  }
  void PlaySound(List<AudioClip> sound, int index) {
      shotSource.clip = sound[index];
      shotSource.Play();
  }
  public void RechargeAmmo(int amount)
  {
      ammo.Add(amount);
  }
  public string AmmoToDisplay()
  {
    var acc = 0;
    for (int i = 1; i < ammo.Count; i++)
    {
      acc += ammo[i];
    }
    return "" + ammoToDisplay + " / " + acc;
  }
}
