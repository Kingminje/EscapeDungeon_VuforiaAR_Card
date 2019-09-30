using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shot : MonoBehaviour
{
    private RaycastHit hit;
    private Ray ray;

    private int count;

    // Update is called once per frame
    private void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit) && Input.GetMouseButtonDown(0))
        {
            if (hit.collider.name == "Episode_1")
            {
                count = hit.collider.transform.GetComponent<ChildCount>().childCount;

                for (int i = 0; i < hit.collider.transform.childCount; i++)
                {
                    if (hit.collider.transform.GetChild(i).gameObject.activeSelf)
                    {
                        hit.collider.transform.GetChild(i).gameObject.SetActive(false);

                        if (count == i + 1)
                        {
                            return;
                        }

                        hit.collider.transform.GetChild(i + 1).gameObject.SetActive(true);
                        return;
                    }
                }
            }
        }
    }
}