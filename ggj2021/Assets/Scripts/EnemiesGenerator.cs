using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] Plans;
    public GameObject[] Enemies;

    public static int activePlansCounter = 1;

    public static int enemiesCounter = 0;

    public Transform player, library;

    void Start()
    {
        StartCoroutine("EnemiesGeneratorCoroutine");
    }

    IEnumerator EnemiesGeneratorCoroutine() {
        while (GameManager.GameOver == false) {
            for (int planIndex=0; planIndex < activePlansCounter; planIndex++) {
                if (Random.Range(0 + enemiesCounter / 2, 10 + GameManager.completedMemoriesCounter) <= GameManager.completedMemoriesCounter) {
                    GameObject enemy = Instantiate(Enemies[Random.Range(0, Enemies.Length)]);
                    enemy.transform.position = Plans[planIndex].position;
                    enemy.GetComponent<EnemyBehaviour>().library = library;
                    enemy.GetComponent<EnemyBehaviour>().player = player;
                    enemiesCounter += 1;
                }
            }
            if (GameManager.completedMemoriesCounter > 1 && GameManager.completedMemoriesCounter < 5) {
                activePlansCounter = 2;
            }
            if (GameManager.completedMemoriesCounter >= 5) {
                activePlansCounter = 2;
            }
            yield return new WaitForSeconds(5f);
        }
    }
}
