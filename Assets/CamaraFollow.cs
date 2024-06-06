using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraFollow : MonoBehaviour
{
    public string playerName = "Olivia";
    public Vector3 offset = new Vector3(0f, 3f, -10f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        GameObject player = GameObject.Find(playerName);
        if (player != null )
        {
            transform.position = player.transform.position + offset;
        }
    }
}
