using UnityEngine;

//This component makes the gameObject follow the cursor
public class FollowCursor : MonoBehaviour
{
    void Update()
    {
        //Read cursor position and convert it from screen coords to world coords
        Vector3 l_desiredPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Override mouse z value since in a 2D environment we don't wnt it to change
        l_desiredPosition.z = transform.position.z;
        //Set new pos as cursor pos
        transform.position = l_desiredPosition;
    }
}
