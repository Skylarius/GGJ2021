using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FragmentsGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform FragmentsContainer;
    public List<GameObject> Fragments;
    public Transform[] Placeholders;
    public bool[] FilledPlaceholdersOneHot;
    public int fragmentsWindow, placeholdersWindow;

    void Start()
    {
        print(FragmentsContainer.childCount + " fragments");
        Fragments = new List<GameObject>();
        for (int i=0; i<FragmentsContainer.childCount; i++) {
            Fragments.Add(FragmentsContainer.GetChild(i).gameObject);
        }
        FilledPlaceholdersOneHot = new bool[Placeholders.Length];
        for (int i=0; i<Placeholders.Length; i++) {
            FilledPlaceholdersOneHot[i] = false;
        }
        StartCoroutine("FragmentsGeneratorCoroutine");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.completedMemoriesCounter <2) {
            fragmentsWindow = 9;
            placeholdersWindow = 3;
        }
        else if (GameManager.completedMemoriesCounter <5) {
            fragmentsWindow = 12;
            placeholdersWindow = 6;
        }
        else {
            fragmentsWindow = Fragments.Count;
            placeholdersWindow = FilledPlaceholdersOneHot.Length;
        }
    }

    IEnumerator FragmentsGeneratorCoroutine() {
        while (GameManager.GameOver == false) {
            int randomFragmentNumber = Random.Range(0, fragmentsWindow);
            List<int> freePlaceholders = new List<int>();
            for (int i=0; i<placeholdersWindow; i++) {
                if (FilledPlaceholdersOneHot[i] == false) {
                    freePlaceholders.Add(i);
                }
            }
            if (freePlaceholders.Count == 0) {
                yield return new WaitForSeconds(5f);
                continue;
            }
            int randomPlaceholderNumber = freePlaceholders[Random.Range(0, freePlaceholders.Count)];
            GameObject fragment = Fragments[randomFragmentNumber];
            Vector3 placeholderPosition = Placeholders[randomPlaceholderNumber].position;
            fragment.transform.position = placeholderPosition;
            Fragments.RemoveAt(randomFragmentNumber);
            FilledPlaceholdersOneHot[randomPlaceholderNumber] = true;
            print("randomFragmentNumber" + randomFragmentNumber);
            print("randomPlaceholderNumber" + randomPlaceholderNumber);
            print("Fragments.Count" + Fragments.Count);
            yield return new WaitForSeconds(5f);
        }
    }
}
