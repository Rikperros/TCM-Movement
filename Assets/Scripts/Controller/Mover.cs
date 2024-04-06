using UnityEngine;
//Here we include our namespace for motions
using TCM.Motion;

//Since it inherits from MonoBehaviour this class will act as a component
public class Mover : MonoBehaviour
{
    //We can call directly those axis without exposing the name but that is hardcoding
    //Pretty people don't hardcode things!!
    public string _horizontalInputAxis = "Horizontal";
    public string _verticalInputAxis = "Vertical";

    //Making a var public allows us to modify it from Unity's inspector
    public float _speed = 0;
    //Here we expose a custom type. In Order to do that we need to mark EMotion type as [System.Serializable]
    public EMotion _desiredmotion = EMotion.URM;

    //Here we will store our inputed direction
    private Vector3 _desiredDirection = Vector3.zero;

    void Update()
    {
        GetDesiredDirectionFromInput();
        PerformSelectedMovement();
    }

    private void GetDesiredDirectionFromInput()
    {
        //Read inputs and store them inside our vector
        _desiredDirection.x = Input.GetAxis(_horizontalInputAxis);
        _desiredDirection.y = Input.GetAxis(_verticalInputAxis);
        //Always remember to normalize it, otherwise diagonals will move faster!
        _desiredDirection.Normalize();
    }

    void PerformSelectedMovement()
    {
        switch(_desiredmotion) 
        {
            case EMotion.URM:
                PerformURM();
                break;
            default:
                return;
        }
    }

    void PerformURM()
    {
        transform.position = URM.PerformFramerateIndependantMovement(transform.position, _desiredDirection, _speed);
    }
}
