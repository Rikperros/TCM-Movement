using UnityEngine;

//Define a name space that will be shared among all movement classes
namespace TCM.Motion
{
    //URM stands for Uniform Rectilinear Movement This class is static so we can call directly those methods from any script
    //That includes this namespace and calls the class
    public static class URM
    {
        //This function doesn't behave good since it is frame dependant meaning that speed isn't in m/s but in m/frame
        //That makes movement inconsistent in different machines or game time
        public static Vector3 PerformFramerateDependantMovement(Vector3 currentPosition, Vector3 direction, float speed)
        {
            return currentPosition + direction * speed;
        }

        //This one is actually in m/s since we multiply it by deltaTime
        public static Vector3 PerformFramerateIndependantMovement(Vector3 currentPosition, Vector3 direction, float speed)
        {
            return currentPosition + (direction * speed * Time.deltaTime);
        }
    }
}