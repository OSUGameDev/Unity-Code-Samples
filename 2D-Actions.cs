/*
Purpose: Call this function when you want a character to attack, and do guarenteed damage to something
Requires: attackAnim animator declared, character have that animation, rigidbodies on target.
    You should only call this function on a prior prerequisite such as a character entering a trigger
Input: float attackStrength bool isDead float attackCooldown, gameObject target
Output: target loses health and an animation on the user of this function is triggered
 */
void attack(gameObject target, float attackStrength, bool isDead, float attackCooldown,audioClip attackClip,float attackVolume){
        if(attackCooldown - attackTimer < 0 && !isDead){
            target.GetComponent<targetScript>().curHealth -= attackStrength;
            attackTimer = 0;
            audioSource.PlayOneShot(attackClip,attackVolume);
            attackAnim.SetTrigger("Attacking");
            target.GetComponent<Rigidbody2D>().AddForce(transform.up*knockback);
        }
}
/*
Purpose: Upon a character dying, this function will run
Requires: local variable despawn timer.  die() is called,despawnTimer isDead,deathAction,animator,rb, deathVolume, deathSound are all local variables
Input: die() is called, bool isDead, bool deathAction, animator animator, Rigidbody2D rb, float deathVolume, audioClip deathSound
Output: Sound is played, animation triggered, and character will destroy itself after a given time.
 */
void Die(float despawnTime){
        if(!deathAction) //if deathrattle not already called, call it
            DeathRattle(isDead,deathAction,animator,rb, deathVolume, deathSound);
        despawnTimer += Time.deltaTime;
        if(despawnTime - despawnTimer < 0)//after despawn timer hits despawn time, destroy the object
            Destroy(this.gameObject);
}
//... continued: This function is tied to die() and will run it's code once before flipping the deathAction bool
void DeathRattle(bool isDead, bool deathAction, animator animator, Rigidbody2D rb, float deathVolume, audioClip deathSound){
        audioSource.PlayOneShot(deathSound,deathVolume);
        isDead = true; //let all the other functions and scripts that interact with this one know that this is dead
        animator.SetBool("isDead",true);
        rb.mass = 10f; //it's good to make the mass lower on a dead enemy because it helps indicate that it has lost all ability for action
        rb.gravityScale = 2f; //this is optional, I like to do this with flying enemies because it's more satisfying when they fall quickly.
        deathAction = true;
}
/*
Purpose:Spawns an object such as an enemy. Useful for objects dedicated to doing such things
Requires: this function in update()
Input: local variable maxThings
Output spawns thing at the script's object
 */
void SpawnObject(gameObject thing, Vector3 spawnOffset){
    spawnTimer+= Time.deltaTime;
    if(spawnTime - spawnTimer < 0 && maxThings > 0){
        // add && Vector3.Distance(transform.position,player.transform.position) <= detectDistance to make this only happen if this is a certain distance from somthign
        spawnTimer = 0;
        maxThings--; //do this until maxThings is 0
        Instantiate(thing,transform.position+spawnOffset,new Quaternion(0,0,0,0)); //spawn thing upright
    }    
}