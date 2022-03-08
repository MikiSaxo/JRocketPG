using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Rigidbody player;

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x + 80, player.position.y + 80, player.transform.position.z);
    }
}
