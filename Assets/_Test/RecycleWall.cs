using System.Collections.Generic;
using UnityEngine;

public class RecycleWall : MonoBehaviour
{
    public List<Transform> walls;   // Kéo thả 4 bức tường vào Inspector
    public float wallHeight = 5f;   // Chiều cao mỗi đoạn
    public Transform player;        // Nhân vật hoặc camera

    void Update()
    {
        // Lấy bức tường cao nhất và thấp nhất hiện tại
        Transform highest = walls[0];
        Transform lowest = walls[0];

        foreach (Transform w in walls)
        {
            if (w.position.y > highest.position.y) highest = w;
            if (w.position.y < lowest.position.y) lowest = w;
        }

        // Nếu player sắp chạm tới bức tường cao nhất -> chuyển bức tường thấp nhất lên trên
        if (player.position.y + 10f > highest.position.y) // +10f = khoảng cách buffer
        {
            lowest.position = new Vector3(
                lowest.position.x,
                highest.position.y + wallHeight,
                lowest.position.z
            );
        }
    }
}
