using UnityEngine;
//Here we include our namespace for motions
using TCM.Motion;

//Since it inherits from MonoBehaviour this class will act as a component
public class Mover : MonoBehaviour
{
    //Making a var public allows us to modify it from Unity's inspector
    public float _speed = 0;
    //We can also expose component types. In Unity we can drag an drop an object in there to set the var
    public Transform _target;
    //Here we expose a custom type. In Order to do that we need to mark EMotion type as [System.Serializable]
    public EMotion _desiredmotion = EMotion.URM;

    void Start()
    {
        //This will look for an object with a particular tag in case we forget to set the target
        if(_target == null)
            _target = GameObject.FindGameObjectWithTag("Target").transform;
    }

    void Update()
    {
        PerformSelectedMovement();
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
        Vector3 l_direction = _target.position - transform.position;
        transform.position = URM.PerformFramerateIndependantMovement(transform.position, l_direction.normalized, _speed);
    }
}
