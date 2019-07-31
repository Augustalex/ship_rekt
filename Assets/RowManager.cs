using UnityEngine;

public class RowManager : MonoBehaviour
{
    private Paddle _leftPaddle;
    private Paddle _rightPaddle;
    private string _playerId;

    // Start is called before the first frame update
    void Awake()
    {
        _leftPaddle = transform.Find("PaddleHolderL/Paddle").GetComponent<Paddle>();
        _rightPaddle = transform.Find("PaddleHolderR/Paddle").GetComponent<Paddle>();
        _playerId = GetComponentInParent<Ship>().PlayerId;
    }

    public void RowLeft()
    {
        _leftPaddle.Rowing = true;
    }

    public void RowRight()
    {
        _rightPaddle.Rowing = true;
    }

    public void StopRowing()
    {
        _leftPaddle.Rowing = false;
        _rightPaddle.Rowing = false;
    }

    public Vector3 GetRowingVector(Transform relativePosition)
    {
        if (_leftPaddle.Rowing) return -relativePosition.right + relativePosition.forward;
        if (_rightPaddle.Rowing) return relativePosition.right + relativePosition.forward;
        return Vector3.zero;
    }
}