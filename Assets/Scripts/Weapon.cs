using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
  public int damage;
  public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
  public int magazineSize, bulletsPerTap, availableMagazines;
  public int currentMagazine = 0;
  public int[] ammo = new int[3];
  public bool allowHoldButton;
  int ammoLeft, ammoShot;
  bool isShooting, isReadyToShoot, isReloading;
  public Camera fpsCam;
  public Transform attackPoint;
  public RaycastHit rayHit;
  public LayerMask enemyMask;
  
  private void Start() {
    ammoLeft = ammo[currentMagazine];
    isReadyToShoot = true;
  }
  
  private void Update() {
    PlayerInput();
  }
  private void PlayerInput() {
    if (allowHoldButton) isShooting = Input.GetKey(KeyCode.Mouse0);
    else isShooting = Input.GetKeyDown(KeyCode.Mouse0);

    if (Input.GetKeyDown(KeyCode.R) && ammoLeft < ammo[currentMagazine] && !isReloading) Reload();
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
    
    if (Physics.Raycast(fpsCam.transform.position, spreadDirection, out rayHit, range, enemyMask)) {
      if (rayHit.collider.CompareTag("Enemy")) rayHit.collider.GetComponent<Rebel_Recruit>().TakeDamage(damage);
    }
    ammoLeft--;
    ammoShot--;
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
    Invoke("ReloadFinished", reloadTime);
  }

  private void ReloadFinished() {
    if (currentMagazine < availableMagazines) {
      currentMagazine++;
    } else {
      currentMagazine = 0;
    }
    ammoLeft = ammo[currentMagazine];
    isReloading = false;
  }

}
