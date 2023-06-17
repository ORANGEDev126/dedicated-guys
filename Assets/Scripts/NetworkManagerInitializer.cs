using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkManagerInitializer : MonoBehaviour
{
    private NetworkManager netManager;

    // Start is called before the first frame update
    void Start()
    {
        netManager = GetComponentInParent<NetworkManager>();

        if(Application.isEditor)
        {
            netManager.StartHost();
        }
        else
        {
            netManager.StartClient();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
