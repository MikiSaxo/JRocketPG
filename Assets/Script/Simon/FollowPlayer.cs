using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Rigidbody player;

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x + 10, player.position.y + 50, player.transform.position.z);
    }
}
