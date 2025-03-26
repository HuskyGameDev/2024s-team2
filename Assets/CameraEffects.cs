using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class CameraEffects : MonoBehaviour
{
    // Camera shake and fov effects
    
    [Header("Debug")]
    public bool debug = true;

    [Header("Objects")]
    private Camera cam;

    [Header("Original values")]
    private Vector3 originalLocalPos;
    private float originalFOV;

    [Header("FOV effects")]
    private float targetFOV;
    private float fovTime;
    private float fovSpeed;

    [Header("Camera Shake")]
    public float shakeLerpValue;
    private Stack<Shake> activeShakes;
    private class Shake {
        public float shakeStrength;
        public float shakeLifetime;

        public Shake(float shakeStrength, float shakeLifetime) {
            this.shakeLifetime = shakeLifetime;
            this.shakeStrength = shakeStrength;
        }
    }

    // Add a new active shake. These effects stack.
    public void ActivateShake(float shakeStrength, float shakeLifetime) {
        if (debug) Debug.LogFormat("CAMERA FX: New Shake effect:", shakeStrength, shakeLifetime);
        activeShakes.Push(new Shake(shakeStrength, shakeLifetime));
    }

    // Add a new active FOV. Overwrites old FOV lerp.
    public void ActivateFOV(float fovMultiplier, float fovTime, float fovSpeed) {
        if (debug) Debug.LogFormat("CAMERA FX: New FOV effect:", fovMultiplier, fovTime, fovSpeed);
        this.fovTime = fovTime;
        this.targetFOV = cam.fieldOfView * fovMultiplier;
        this.fovSpeed = fovSpeed;
    }

    // Handles the shake effects, if they are active.
    private void HandleShakes() {
        // Return to center if there are no shakes
        if (activeShakes.Count <= 0) {
            gameObject.transform.localPosition = Vector3.Lerp(gameObject.transform.localPosition, originalLocalPos, shakeLerpValue*Time.deltaTime);
        }
        Vector3 currentPos = originalLocalPos; // Necessary to stack effects
        for (int i = 0; i < activeShakes.Count; i++) {
            Shake shake = activeShakes.Pop();
            shake.shakeLifetime -= Time.deltaTime;
            if (shake.shakeLifetime <= 0) {
                continue;
            }
            Vector3 shakeVec = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1));
            shakeVec *= shake.shakeStrength;
            currentPos += Vector3.Lerp(originalLocalPos, shakeVec, shakeLerpValue*Time.deltaTime);
            activeShakes.Push(shake);
        }
        gameObject.transform.localPosition = currentPos;
    }

    private void HandleFOV() {
        if (fovTime <= 0) {
            fovTime = 0;
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, originalFOV, fovSpeed*Time.deltaTime);
            return;
        }
        fovTime -= Time.deltaTime;
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, fovSpeed*Time.deltaTime);
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
        originalLocalPos = gameObject.transform.localPosition;
        originalFOV = cam.fieldOfView;
        activeShakes = new Stack<Shake>();
        // ActivateFOV(2f, 5, 1);
    }

    // Update is called once per frame
    void Update()
    {
        HandleShakes();
        HandleFOV();
    }
}
