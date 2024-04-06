using UnityEngine;

public class DebugMovement : MonoBehaviour
{
    private int _accumulatedFrames = 0;
    private float _currentDisplacement = 0;
    private float _currentLifeTime = 0;
    private float _accumulatedDisplacement = 0;
    private Vector3 _previousPosition;

    private void Start()
    {
        _previousPosition = transform.position;
    }
    void Update()
    {
        _accumulatedFrames += 1;
        _currentDisplacement = (transform.position - _previousPosition).magnitude;
        _accumulatedDisplacement += _currentDisplacement;
        _currentLifeTime += Time.deltaTime;

        Debug.Log("Frame " +_accumulatedFrames
            + ", adds " +_currentDisplacement
            + "m to " + _accumulatedDisplacement 
            + ". Time " + _currentLifeTime
            + " deltaTime: " + Time.deltaTime);

        _previousPosition = transform.position;
    }
}
