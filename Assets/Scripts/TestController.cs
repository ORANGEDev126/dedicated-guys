using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class TestController : NetworkBehaviour
{
    public NetworkVariable<Vector3> position;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if(IsOwner)
        {
            Move();
        }
    }

    private static Vector3 MakeRandomVector()
    {
        return new Vector3(Random.Range(-3f, 3f), 1f, Random.Range(-3f, 3f));
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            Move();
        }
    }

    private void Move()
    {
        if(IsServer && IsOwner)
        {
            position.Value = MakeRandomVector();
            transform.position = position.Value;
        }
        else
        {
            RequestMoveServerRpc(MakeRandomVector());
        }
    }

    [ServerRpc]
    void RequestMoveServerRpc(Vector3 positionToRequest)
    {
        // check if position corrects
        position.Value = positionToRequest;
        SyncPositionClientRpc();
    }

    [ClientRpc]
    void SyncPositionClientRpc()
    {
        transform.position = position.Value;
    }
}
