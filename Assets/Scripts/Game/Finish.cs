using UnityEngine;

public class Finish : MonoBehaviour
{
    private Vector3 playerStartPos;
    [SerializeField]
    private bool loop = false;
    private void Start() {
        playerStartPos = FindObjectOfType<Player>().transform.position;
    }
    private void OnTriggerEnter(Collider other) {
        if(other.TryGetComponent<Player>(out Player player)) {
            if (loop)
            {
                player.ResetPos();
                return;
            }
            Debug.Log("Game Finish");
            Base.FinisGame(Enums.GameStat.Win,1f);
        }
    }
}
