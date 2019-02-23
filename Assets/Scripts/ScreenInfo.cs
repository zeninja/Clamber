using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenInfo : MonoBehaviour
{
    public static float x, y;
    public static float w, h;

    public static Vector2 pixel_TL;
    public static Vector2 pixel_TR;
    public static Vector2 pixel_BL;
    public static Vector2 pixel_BR;
    public static Vector2 pixel_Coordinates;

    public static Vector2 world_TL;
    public static Vector2 world_TR;
    public static Vector2 world_BL;
    public static Vector2 world_BR;
    public static Vector2 world_Coordinates;

    void Awake()
    {
        x = Screen.width;
        y = Screen.height;

        h = Camera.main.orthographicSize * 2.0f;
        w = h * Screen.width / Screen.height;

        pixel_TL = new Vector2(-x/2,  y/2);
		pixel_TR = new Vector2( x/2,  y/2);
		pixel_BL = new Vector2( x/2, -y/2);
		pixel_BR = new Vector2(-x/2, -y/2);

		world_TL = new Vector2(-w/2,  h/2);
		world_TR = new Vector2( w/2,  h/2);
		world_BL = new Vector2( w/2, -h/2);
		world_BR = new Vector2(-w/2, -h/2);

        
    }
}
