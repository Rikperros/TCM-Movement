using UnityEngine;

//Adding a new movement type into this namespace
namespace TCM.Motion
{
    public class VelocityBasedMovement
    {
        private float _maxSpeed;
        private float _acceleration;

        private Vector3 _velocity = Vector3.zero;

        //We can set up regular constructors
        public VelocityBasedMovement(float MaxSpeed, float Acceleration)
        {
            this._maxSpeed = MaxSpeed;
            this._acceleration = Acceleration;
        }

        public Vector3 PerformMovement(Vector3 CurrentPosition, Vector3 DesiredDirection) 
        {
            //Compute velocity by adding small accelerations. Here we multiply by delta time since we want to make 
            //Acceleration tame dependant, not frame dependant
            _velocity += DesiredDirection * _acceleration * Time.deltaTime;

            //Clamp velocity to not surpass the max speed
            if(_velocity.magnitude > _maxSpeed)
            {
                _velocity = DesiredDirection * _maxSpeed;
            }

            //Add this velocity to the current position, again we want to make that time dependant, not frame dependant
            return CurrentPosition +_velocity * Time.deltaTime;
        }

    }
}