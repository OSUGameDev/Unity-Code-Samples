/*
Purpose: Shoot projectiles towards an input gameObject, outputs a sound, has a cooldown, activates animation
Also can detect if an object is set to be an "enhanced" variant
Requires: declared class variables burstTimer, fireTimer, curShots
You must declare the starting curshots you want for each script
Input: target, fireSound
Output: instantiated projectile
 */
void ShootProjectile(gameObject target,gameObject projectile,audioClip fireSound, float burstSpeed, float fireRate, int shots){
    if(fireRate - fireTimer < 0){
        burstTimer+= Time.deltaTime;
        if(curShots > 0 && burstSpeed-burstTimer < 0){
            gameObject.animator.SetTrigger("isFiring");
            audioPlayer.PlayOneShot(fireSound,0.04f);
            var shot = Instantiate(projectile,transform.position,transform.rotation);
            if(transform.parent.tag == "BigShooter")
                shot.transform.localScale = new Vector3(4,4,1);
            shots--;
            burstTimer = 0;
        }
        else if(shots <= 0){ //once all shots have been shooted, start the firing cooldown and # of shots
            fireTimer = 0; 
            curShots = shots;
        }
    }
    fireTimer += Time.deltaTime; 
}
/*
Purpose: Gun scripts that involve a player using it
Requires: local variables curAmmo, maxAmmo, reloading, shotTimer, the reloadGun function
Input: float fireRate, gameObject bullet,Vector3 offset
Output: instantiates a bullet at the transform of the gun+ offset, 
 */
void ShootGun(float fireRate, gameObject bullet, Vector3 offset){
    if(fireRate - shotTimer < 0 && curAmmo > 0 && !reloading){ 
        audioPlayer.PlayOneShot(gunShot,0.04f);
        Debug.Log("firing gun");
        Instantiate(bullet,transform.position+offset,tranform.rotation);
        curAmmo--;
        lastShot = 0;
    }
    else if(curAmmo == 0){
        //play empty mag sound and anything to indicate the ammo is empty/
    }
    shotTimer += Time.deltaTime;
}
//pairing function to shooting
void ReloadGun(float reloadTime){   
    if(reloading == false){
        //do initial reloading animations and such
        reloading = true;
    }

    timer += Time.deltaTime;
    Debug.Log("Reloading gun");
    if(reloadTime - timer < 0){
        curAmmo = maxAmmo;
        reloading = false;
        timer = 0;
    }
    
}