using UnityEngine;

namespace DefaultNamespace
{
    public class Sail : MonoBehaviour
    {
        private GameObject _wind;

        public void Start()
        {
            _wind = GameObject.FindGameObjectWithTag("wind");
        }

        private float GetRotation()
        {
            var angle = transform.rotation.eulerAngles.y;
            return angle;
        }

        private float GetRotationAgainstWind(float windAngle)
        {
            var angle = windAngle;
            if (angle > 180)
            {
                return -270 + angle;
            }
            else
            {
                return 90 - angle;
            }
        }

        public float GetNormalizedEffeciencyAgainstWind()
        {
            var wind = _wind.transform.rotation.eulerAngles.y;
            var sail = GetRotation();
            var windRelative = (360 - wind + sail) % 360;

            var rotation = GetRotationAgainstWind(windRelative);
            var normalized = ((rotation - -90) / (90 - -90));
            return normalized;
        }
    }
}