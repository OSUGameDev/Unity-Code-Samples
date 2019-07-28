/*//
Purpose: will make the UI image retract and detract. Use this for a UI object that's at a corner of the screen
Requires: this function in Update(),
Input: bool toggleUI, AudioClip uiIn, AudioClip uiOut
Output: The ui either retracting or detracting
//*/

private void ShowUI(bool toggleUI, AudioSource audioPlayer, AudioClip uiIn, AudioClip uiOut, RawImage image){
    if(toggleUI && image.transform.localScale.x <3){
        audioPlayer.PlayOneShot(uiOut,menuVolume);
        var scale = image.transform.localScale;
        scale.x += Time.deltaTime*showSpeed;
        image.transform.localScale = scale;
    }
        
    else if(!toggleUI && image.transform.localScale.x > 0){
        audioPlayer.PlayOneShot(uiIn,menuVolume);
        var scale = image.transform.localScale;
        scale.x -= Time.deltaTime*showSpeed;
        image.transform.localScale = scale;
    }
}
/*//
Purpose: will make text or any object you modify this function for change colors to alert the player
This is good when a player has low health or ammo
Requires: this function is called in update, local variable timer
Input: bool toggleUI, AudioClip uiIn, AudioClip uiOut
Output: The ui either retracting or detracting
//*/
void AlertColors(Text text){
    timer += Time.deltaTime;
    if(timer < .25){
        text.color = Color.red;
    }
    else if(timer > .5)
        timer = 0;
    else {
        text.color = Color.black;
    }
}
/*
Purpose: Makes an object such as a reticle follow the position of the mouse on the screen
Requires: local variable gameObject player
Input: mouseMovement
Output: object goes to mouse position
 */
void FollowMouse(){
    bool facingRight = player.GetComponent<CharacterMovement>().facingRight = false;
    Vector3 mousePos = Input.mousePosition;
    transform.position = Camera.main.ScreenToWorldPoint(mousePos);
    transform.position = Vector2.Lerp(transform.position, mousePos, lookSpeed);
}