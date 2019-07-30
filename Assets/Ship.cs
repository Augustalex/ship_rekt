using DefaultNamespace;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Ship : MonoBehaviour
{
    public string PlayerId = "1";

    private Rigidbody _body;
    private Sail _sail;
    private bool _controllingSail = true;
    private LowPolyWater.LowPolyWater _water;

    private const float SailSpeed = 5f;
    private const float ShipDragForce = .25f;
    private const float ShipRotationSpeed = 800;
    private const float SailRotationSpeed = 100;
    private const int MaxVelocity = 10;

    void Start()
    {
        _water = GameObject.FindGameObjectWithTag("water").GetComponent<LowPolyWater.LowPolyWater>();
        _body = GetComponent<Rigidbody>();
        _sail = transform.Find("Body/Sail").GetComponent<Sail>();
    }

    void Update()
    {
        if (Input.GetButtonDown(PlayerId + "_player_switch"))
        {
            _controllingSail = !_controllingSail;
        }


        if (_body.velocity.magnitude < MaxVelocity)
        {
            var speedAhead = GetSailVector() + transform.forward * ShipDragForce;
            _body.AddForce(speedAhead, ForceMode.Acceleration);
        }

        var axis = Input.GetAxis(PlayerId + "_player_horizontal");
        if (axis > .5f || axis < -.5f)
        {
            if (_controllingSail)
            {
                var heading = axis < -.5f
                    ? -_sail.transform.up
                    : _sail.transform.up;
                heading = heading * Time.deltaTime * SailRotationSpeed;
                _sail.transform.Rotate(heading);
            }
            else
            {
                var heading = axis < -.5f
                    ? -transform.up
                    : transform.up;
                _body.AddTorque(heading * ShipRotationSpeed * Time.deltaTime);
            }
        }
    }

    private void FixedUpdate()
    {
        var nearestVertecies = _water.SomeVertex();
        var position = _body.position;
//        Debug.Log(nearestVertecies[0].y + ", " + nearestVertecies[1].y + ", " + nearestVertecies[2].y);
         _body.position = new Vector3(position.x, nearestVertecies[0].y + .5f, position.z);
    }

    private Vector3 GetSailVector()
    {
        return _sail.transform.forward * SailSpeed * _sail.GetNormalizedEffeciencyAgainstWind();
    }
}