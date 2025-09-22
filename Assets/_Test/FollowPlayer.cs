using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform Player;

    void Update()
    {
        if (Player != null)
        {
            Vector3 newPosition = transform.position;
            newPosition.x = Player.position.x;
            transform.position = newPosition;
        }
    }
}
