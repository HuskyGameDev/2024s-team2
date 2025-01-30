using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTest : MonoBehaviour
{

    //reference to existing scene lightmap data.
    LightmapData[] lightmap_data;
    public Texture2D[] torch1Dir, torch1Color;

    // Use this for initialization
    void Start()
    {
        // Save reference to existing scene lightmap data.
        lightmap_data = LightmapSettings.lightmaps;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) {
            disableLightmaps();
        }

        if (Input.GetKeyDown(KeyCode.L)) {
            enableLightmaps();
        }
    }

    public void disableLightmaps()
    {
        // Disable lightmaps in scene by removing the lightmap data references
        LightmapSettings.lightmaps = new LightmapData[] { };
    }

    public void enableLightmaps()
    {
        // Reenable lightmap data in scene.
        LightmapSettings.lightmaps = lightmap_data;
    }
}
