/*//
Purpose: Destroy projectile upon hitting the a tagged object, instantiating a new gameobject at that location, usually a self destructing animated sprite to show an impact
Requires: OnCollisionEnter2D to be declared, and the object to have a 2d collider
Input: Collider2D, gameobject effect
Output: Destroy self
//*/
private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "tag"){ //check if acid is fast enough
            var effect = Instantiate(effectObj,other.transform);
            effect.transform.parent = transform;
            Destroy(gameObject);
        }
}