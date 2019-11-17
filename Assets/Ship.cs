using DefaultNamespace;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Ship : MonoBehaviour
{
    public string PlayerId = "1";

    private int _health = 3;
    private AudioSource _explosion;
    private Rigidbody _body;
    private Sail _sail;
    private CannonManager _cannonManager;
    private bool _controllingSail = true;
    private bool _sailGone = false;
    private LowPolyWater.LowPolyWater _water;
    private bool _sinking = false;

    private const float SailSpeed = 5f;
    private const float ShipDragForce = .25f;
    private const float ShipRotationSpeed = 800;
    private const float SailRotationSpeed = 100;
    private const float PaddleRotationSpeed = 25;

    private float _sinkTime = 0;
    private float _sinkDownTime = 0;
    private RowManager _rowManager;

    void Start()
    {
        _water = GameObject.FindGameObjectWithTag("water").GetComponent<LowPolyWater.LowPolyWater>();
        _explosion = GetComponent<AudioSource>();
        _body = GetComponent<Rigidbody>();
        _sail = transform.Find("Body/Sail").GetComponent<Sail>();
        _cannonManager = GetComponent<CannonManager>();
    }

    void Update()
    {
        if (_sinking)
        {
            var newRotation = Quaternion.AngleAxis(90, transform.right);

            var similar = Mathf.Abs(Quaternion.Dot(newRotation, transform.rotation)) > .995;
            if (similar)
            {
                _sinkDownTime = Mathf.Min(1, _sinkDownTime + .025f * Time.deltaTime);
                var position = transform.position;
                var endPosition = new Vector3(position.x, position.y - 5, position.z);
                transform.localPosition = Vector3.Slerp(transform.localPosition, endPosition, _sinkDownTime);
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, _sinkTime);
                _sinkTime = Mathf.Min(1, _sinkTime + .01f * Time.deltaTime);
            }

            return;
        }

        if (Input.GetButtonDown(PlayerId + "_player_switch"))
        {
            _controllingSail = !_controllingSail;
            if (_sailGone)
            {
                _rowManager.StopRowing();
            }
        }

        if (_body.velocity.magnitude < GetMaxVelocity())
        {
            if (_sailGone)
            {
                var rowVector = _controllingSail
                    ? _rowManager.GetRowingVector(transform) * 10
                    : _rowManager.GetRowingVector(transform) * -10;
                var speedAhead = GetSailVector() * .1f + rowVector;
                _body.AddForce(speedAhead, ForceMode.Acceleration);
            }
            else
            {
                var speedAhead = GetSailVector() + transform.forward * ShipDragForce;
                _body.AddForce(speedAhead, ForceMode.Acceleration);
            }
        }

        var axis = Input.GetAxis(PlayerId + "_player_horizontal");
        if (axis > .5f || axis < -.5f)
        {
            if (_sailGone)
            {
                _rowManager.StopRowing();

                if (axis > .5f)
                {
                    _rowManager.RowRight();
                }
                else
                {
                    _rowManager.RowLeft();
                }

                var heading = axis < -.5f
                    ? -_sail.transform.up
                    : _sail.transform.up;
                heading = heading * Time.deltaTime * PaddleRotationSpeed;
                _body.transform.Rotate(heading);
            }
            else
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
        else if(_sailGone)
        {
            _rowManager.StopRowing();
        }
    }

    private void FixedUpdate()
    {
        if (_sinking) return;

        var nearestVertecies = _water.SomeVertex();
        var position = _body.position;
        _body.position = new Vector3(position.x, nearestVertecies[0].y + .5f, position.z);
    }

    private Vector3 GetSailVector()
    {
        return _sail.transform.forward * SailSpeed * _sail.GetNormalizedEffeciencyAgainstWind();
    }

    public void DoDamage()
    {
        _explosion.Play();

        _health -= 1;
        if (_health == 1)
        {
            ReplaceSailWithPaddles();
        }
        else if (_health == 2)
        {
            _cannonManager.RemoveOneCannon();
        }
        else if (_health == 0)
        {
            Sink();
        }
    }

    private float GetMaxVelocity()
    {
        if (_sailGone)
        {
            return 5;
        }

        return 10;
    }

    private void ReplaceSailWithPaddles()
    {
        _sailGone = true;

        transform.Find("Body/Sail").gameObject.SetActive(false);

        var paddleParent = transform.Find("Body/Paddles");
        paddleParent.gameObject.SetActive(true);
        _rowManager = paddleParent.GetComponent<RowManager>();
    }

    private void Sink()
    {
        _sinking = true;

        Destroy(_body);

        var currentRotation = transform.rotation;
        transform.rotation = Quaternion.Euler(new Vector3(0, currentRotation.y, 0));

        GameObject.FindGameObjectWithTag("music").GetComponent<AudioSource>().Stop();
    }
}
