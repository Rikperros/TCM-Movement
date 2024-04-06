using UnityEngine;
//Here we include our namespace for motions
using TCM.Motion;

//This drawer automatically adds a component of a certain type if it doesn't exist in the gameObject
[RequireComponent(typeof(Rigidbody2D))]
//Since it inherits from MonoBehaviour this class will act as a component
public class Mover : MonoBehaviour
{
    [Header("Input")]
    //We can call directly those axis without exposing the name but that is hardcoding
    //Pretty people don't hardcode things!!
    public string _horizontalInputAxis = "Horizontal";
    public string _verticalInputAxis = "Vertical";
    [Header("Movement Selected")]
    //Here we expose a custom type. In Order to do that we need to mark EMotion type as [System.Serializable]
    public EMotion _desiredmotion = EMotion.URM;

    [Header("URM Config")]
    //Making a var public allows us to modify it from Unity's inspector
    public float _speed = 0;

    //this custom drawers allow us to organise a little bit better the inspector
    [Header("Velocity Based Mov Config")]
    public float _maxSpeed = 10f;
    public float _acceleration = 2f;

    [Header("Lerp Movement")]
    public Transform _target;
    [Range(0.0f, 1.0f)]
    public float _lerpAlpha = 0.2f;
    public string _targetTag = "Target";

    [Header("Physics Based Movement")]
    public Rigidbody2D _rigidbody2D;
    public float _forceMagnitude;
    public ForceMode2D _forceMode = ForceMode2D.Force;

    //Here we will store our inputed direction
    private Vector3 _desiredDirection = Vector3.zero;
    
    private VelocityBasedMovement _VelocityBasedMovement;

    private void Start()
    {
        if(_target == null)
            _target = GameObject.FindGameObjectWithTag(_targetTag).transform;
        //Here we are looking for our own component, so we can access it directly using the gameObject field
        if(_rigidbody2D == null)
            _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        GetDesiredDirectionFromInput();
        PerformSelectedMovement();
    }

    private void FixedUpdate()
    {
        //Any rigidbody related movement is relying on the Physics systems, therefore we need to update it 
        //on the fixed update. Otherwise our movement will fight the physics system and we will have artifacts
        if(_desiredmotion == EMotion.PHYSICS) 
        {
            _rigidbody2D.AddForce(_desiredDirection * _forceMagnitude, _forceMode);
        }
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
            case EMotion.VELOCITY_BASED_MOVEMENT:
                PerformVelocityBasedMovement();
                break;
            case EMotion.LERP:
                PerformLerpMovement();
                break;
            default:
                return;
        }
    }

    void PerformURM()
    {
        transform.position = URM.PerformFramerateIndependantMovement(transform.position, _desiredDirection, _speed);
    }

    void PerformVelocityBasedMovement()
    {
        //Here is an example of a custom regular object usage
        if(_VelocityBasedMovement == null)
        {
            _VelocityBasedMovement = new VelocityBasedMovement(_maxSpeed, _acceleration);
        }

        transform.position = _VelocityBasedMovement.PerformMovement(transform.position, _desiredDirection);
    }

    void PerformLerpMovement()
    {
        //in this case the lerp acts as a logaritmic curve since every time the distance to interpolate is smaller
        //if we used a fixed start point it will act as a linear transition
        transform.position = Vector3.Lerp(transform.position, _target.position, _lerpAlpha * Time.deltaTime);
    }
}
