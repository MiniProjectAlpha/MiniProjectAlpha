using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
	public Transform[] shotGunMuzzle; //여러 개의 탄환 생성기 
	public Projectile projectile;

	public float msBetweenShots = 100;
	public float muzzleVelocity = 50; //통상탄 보다는 빠르게

	float nextShotTime;
	public void SgShot()
	{
		if (Time.time > nextShotTime)
		{
			for (int i = 0; i < shotGunMuzzle.Length; i++)
			{
				nextShotTime = Time.time + msBetweenShots / 1000;
				Projectile newProjectile = Instantiate(projectile, shotGunMuzzle[i].position, shotGunMuzzle[i].rotation) as Projectile;
				newProjectile.SetSpeed(muzzleVelocity);
			}
		}
	}

}