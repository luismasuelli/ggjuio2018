using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveScript : MonoBehaviour {

    /*
     * Based on http://www.psilocybegames.com/captainslog/2017/02/15/heat-wave-shader-effect-tutorial-in-unity-5/
     */
    public float speed = 0.01f;
    public float shockwaveScaleLimit = 2;
    public bool loop = true;

	void Update ()
    {
        transform.localScale += new Vector3( speed, speed, speed );
        if( transform.localScale.x > shockwaveScaleLimit )
        {
            if( loop ) {
                transform.localScale = new Vector3( 0.01f, 0.01f, 0.01f );
            }
            else {
                //Destroy( this );
                gameObject.SetActive( false );
            }
        }
		
	}
}
    