/*
Purpose:Flips a character to face a given position relative to it's x value
Requires: flip() be called, should be in update()
Input: position target
Output flips the x transform of the object so that it is facing the character
 */
void flip(position target){
    if(target.x - transform.position.x < 0){
        faceRight = false;
    }
    else
        faceRight = true;
    //in my expirence flipping the transform like this can sometimes leave it to be 180.02342342 or 0.0234 instead of a clean 180 or 0,
    //so I give it a little extra allowance here
    if(faceRight && transform.eulerAngles.y < 179){
        transform.Rotate(0,180,0);
    }
    if(!faceRight && transform.eulerAngles.y > 1)
        transform.Rotate(0,180,0);
}
/*
Purpose:Reorients an object to be "upright" this function is really funky and doesn't work on a lotof objects.
It basically makes your object act like one of those toys that bounce back when you punch them
Requires: reOrient() is in update()
Input: float timeToReorient and rigidbody rb
Output:constantly applies force towards a place inbetween
 */
void reOrient(float timeToReorient,Rigidbody2D rb){
    //the || part of this if statement assumes that the object can flip it's x angle, you can remove one of these if you aren't doing this
    if(transform.rotation.eulerAngles.z > 220 && transform.rotation.eulerAngles.z < 359 || transform.rotation.eulerAngles.z > 1 && transform.rotation.eulerAngles.z < 200){
        timer += Time.deltaTime;
        if(timeToReorient - timer < 0){ //This timer is if you want an object to wait before getting up, but functionally works best at 0
            getUp = true;
            timer = 0;
        }
    }
    else{   
        getUp = false;
        timer = 0;
    }
    if(getUp){
        rb.centerOfMass = com;
        rb.AddForce(transform.right*getUpStrength);
        //rb.AddForce(-transform.up*getUpStrength); //add this if you have a flying object
    }
}
/*
Purpose: If you have a flying object, this will regulate it's height if it has a rigidbody.
Requires: isHighEnough is in update()
Input: float minHeight, float maxHeight
Output: force on rigidbody
 */
private void IsHighEnough(float minHeight, float maxHeight){  
    Vector2 position = transform.position;
    Vector2 direction = -transform.up;
    RaycastHit2D lowCheck = Physics2D.Raycast(position, direction, minHeight, groundLayer); //casts a ray straight down the length of 
    Debug.DrawRay(position, direction, Color.green);
    //Debug.Log(hit.collider.gameObject.tag); //uncomment this for when you want to adjust the height
    if (lowCheck.collider != null) {
        rb.AddForce(transform.up*flyStrength);
    }
    //check if the character is too high off the ground
    RaycastHit2D tooHigh = Physics2D.Raycast(position, direction, maxHeight, groundLayer);
    if(highCheck.collider == null)
        rb.AddForce(-transform.up*flyStrength);
}


/*
Purpose: makes the object jump! if you set the cooldown to zero then you can make the object fly infinitely
Requires: This should be called with a conditional such as if the is on the ground, or if they have x amount of jumps left
Input: float cooldown, float jumpstrength
Output: force on rb
 */
void Jump(float jumpCooldown, float jumpStrength, Rigidbody2D rb){
    jumpTimer += Time.deltaTime;
    if(jumpCooldown - jumpTimer < 0){
        rb.AddForce(transform.up*jumpStrength);
        jumpTimer = 0;
    }
}

/*
Purpose: checks if the object is a certain distance from the ground, adjust this to the height of where you are sending the raycast
This is the little brother of isHighEnough()
Requires: this function in update()
Input: (float minHeight, float maxHeight, float jumpOffset, LayerMask groundLayer
Output: bool
 */
bool IsGrounded(float minHeight, float maxHeight, float jumpOffset, LayerMask groundLayer){  
    Vector2 position = transform.position;
    Vector2 direction = -transform.up;
    RaycastHit2D hit = Physics2D.Raycast(position, direction, jumpOffset, groundLayer);
    Debug.DrawRay(position, direction, Color.green);
    //Debug.Log(hit.collider.gameObject.tag); //uncomment this for when you want to adjust the height
    if (hit.collider != null)
        return true;
    return false;
}

/*
Purpose: Returns the angle from one object to the other
Requires: function call
Input: position target
Output: Quaternion, it's like a vector3 but apparently is faster in certain angle computations.
 */
Quaternion FaceObject(position target){
    var dir = target - transform.position;
    var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    angle = Quaternion.AngleAxis(angle, Vector3.forward);
    return angle;
}