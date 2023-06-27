using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteOrientation : MonoBehaviour
{   
    private void Update()
    {

        float orientation = transform.rotation.eulerAngles.z;

        GetComponent<SpriteRenderer>().flipY = (orientation >= 90 && orientation < 270);

    }

}
