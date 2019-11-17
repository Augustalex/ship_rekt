using UnityEngine;

namespace DefaultNamespace
{
    public class ShipSpawn : MonoBehaviour
    {
        public GameObject ShipPrefab;
        public GameObject MusicPrefab;
        public Transform FirstPlayerSpawn;
        public Transform SecondPlayerSpawn;

        public void Awake()
        {
            SpawnAll();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Respawn();
            }
        }

        private void SpawnAll()
        {
            Instantiate(MusicPrefab);

            Spawn(FirstPlayerSpawn.position, "1", "2", new Vector3(0, 90, 0));
            Spawn(SecondPlayerSpawn.position, "3", "4", new Vector3(0, -90, 0));
        }

        private void Spawn(Vector3 spawnPosition, string navigatorPlayerId, string cannonPlayerId, Vector3 rotation)
        {
            var ship = Instantiate(ShipPrefab, spawnPosition, Quaternion.identity * Quaternion.Euler(rotation));
            ship.GetComponent<Ship>().PlayerId = navigatorPlayerId;
            ship.GetComponent<CannonManager>().PlayerId = cannonPlayerId;
        }

        private void Respawn()
        {
            var ships = GameObject.FindGameObjectsWithTag("ship");
            foreach (var ship in ships)
            {
                Destroy(ship);
            }

            var music = GameObject.FindGameObjectWithTag("music");
            Destroy(music);

            SpawnAll();
        }
    }
}