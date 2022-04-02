using UnityEngine;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.TryGetComponent<Player>(out Player player)) {
            Debug.Log("Finish");
            Base.FinisGame(Enums.GameStat.Win,1f);
        }
    }
}
