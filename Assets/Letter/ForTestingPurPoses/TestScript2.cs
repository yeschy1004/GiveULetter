using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript2 : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float width;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(1.5f, target.localScale.y + Mathf.Sqrt(2f), 1);
    }
}
