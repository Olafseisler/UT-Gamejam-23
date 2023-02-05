using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] float parallaxConstant = 5f;
    
    private float length, startpos;

    public GameObject cam;

    public float parallaxeffect;
    
    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallaxeffect));
        float dist = cam.transform.position.x * parallaxeffect;
        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        if (temp + parallaxConstant > startpos + length) startpos += length;
        else if (temp - parallaxConstant < startpos - length) startpos -= length;
        
    }
}
