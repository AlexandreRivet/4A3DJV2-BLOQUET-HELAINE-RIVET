using UnityEngine;
using System.Collections;

public class TestAdjustResolution : MonoBehaviour {
    public float targetaspect_w;
    public float targetaspect_h;
	// Use this for initialization
	void Start () {
        // set the desired aspect ratio (the values in this example are
        // hard-coded for 16:9, but you could make them into public
        // variables instead so you can set them at design time)
        float targetaspect = targetaspect_w / targetaspect_h;

        // determine the game window's current aspect ratio
        float windowaspect = (float)Screen.width / (float)Screen.height;

        // current viewport height should be scaled by this amount
        float scaleheight = windowaspect / targetaspect;

        // obtain camera component so we can modify its viewport
        Camera camera = GetComponent<Camera>();

        // if scaled height is less than current height, add letterbox
        if (scaleheight < 1.0f)
        {
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;

            camera.rect = rect;
        }
        else // add pillarbox
        {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = camera.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }
	}
	
	// Update is called once per frame
	void Update () {
        
	}
    /*
     
    void Start () 
    {
        sr = GetComponent<SpriteRenderer> ();
        float xmas = Screen.width*Camera.main.orthographicSize*2.0f /(Screen.height*1.0f);
        float yScale = Camera.main.orthographicSize*2.0f / sr.renderer.bounds.size.y;
        float xScale = 0;
        xScale = xmas / sr.renderer.bounds.size.x;
        transform.localScale = new Vector3 (xScale,yScale,1);
    }
     
      
    var resolutions : Resolution[] = Screen.resolutions;
    // Print the resolutions
    for (var res in resolutions) {
        print(res.width + "x" + res.height);
    }
    // Switch to the lowest supported fullscreen resolution
    Screen.SetResolution (resolutions[0].width, resolutions[0].height, true);
        
     */

    /*// set the desired aspect ratio (the values in this example are
    // hard-coded for 16:9, but you could make them into public
    // variables instead so you can set them at design time)
    float targetaspect = 16.0f / 9.0f;

    // determine the game window's current aspect ratio
    float windowaspect = (float)Screen.width / (float)Screen.height;

    // current viewport height should be scaled by this amount
    float scaleheight = windowaspect / targetaspect;

    // obtain camera component so we can modify its viewport
    Camera camera = GetComponent<Camera>();

    // if scaled height is less than current height, add letterbox
    if (scaleheight < 1.0f)
    {  
        Rect rect = camera.rect;

        rect.width = 1.0f;
        rect.height = scaleheight;
        rect.x = 0;
        rect.y = (1.0f - scaleheight) / 2.0f;
        
        camera.rect = rect;
    }
    else // add pillarbox
    {
        float scalewidth = 1.0f / scaleheight;

        Rect rect = camera.rect;

        rect.width = scalewidth;
        rect.height = 1.0f;
        rect.x = (1.0f - scalewidth) / 2.0f;
        rect.y = 0;

        camera.rect = rect;
    }
     * */
}
