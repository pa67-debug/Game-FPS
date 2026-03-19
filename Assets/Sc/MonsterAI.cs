using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public Transform player;
    public float speed = 3f;

    void Update()
    {
        if (player == null) return;

        // เดินไปหาผู้เล่น
        transform.position = Vector3.MoveTowards(
            transform.position,
            player.position,
            speed * Time.deltaTime
        );

        // หันหน้าหาผู้เล่น
        transform.LookAt(player);
    }
}