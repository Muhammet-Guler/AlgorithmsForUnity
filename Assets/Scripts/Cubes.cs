using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cubes : MonoBehaviour
{
    public int CubeHeightMax = 40;
    public int NumberOfCubes = 100;
    public float speed = 0.01f;
    private int numberOfCubes;
    private bool isSorting = false;
    public GameObject sorting_ui;
    public GameObject unsorting_ui;
    public List<GameObject> Cube;
    GameObject Temp;
    GameObject[] TempList;
    public void Again()
    {
        SceneManager.LoadScene(0);
    }
    public void OnMergeSort()
    {
        if (IsSorted(Cube))
            Init();
        isSorting = true;
        StartCoroutine(MergeSort(Cube, 0, Cube.Count - 1));
    }
    public void OnQuickSort()
    {
        if (IsSorted(Cube))
            Init();
        isSorting = true;
        StartCoroutine(QuickSort(Cube, 0, Cube.Count - 1));
    }
    public void OnInsertionSort()
    {
        if (IsSorted(Cube))
            Init();
        isSorting = true;
        StartCoroutine(InsertionSort(Cube));
    }
    public void OnSelectionSort()
    {
        if (IsSorted(Cube))
            Init();
        isSorting = true;
        StartCoroutine(SelectionSort(Cube));
    }
    // Start is called before the first frame update
    void Start()
    {
        numberOfCubes = 100;
        Cube = new List<GameObject>();
        Init();
        //StartCoroutine(SelectinSort(Cube));
    }
    public IEnumerator SelectionSort(List<GameObject> c)
    {
        for (int i = 0; i < c.Count; i++)
        {
            int min_index = i;
            LeanTween.color(c[i], Color.green, 0.01f);

            for (int j = i + 1; j < c.Count; j++)
            {
                yield return new WaitForSeconds(speed);
                LeanTween.color(c[j], Color.green, 0.01f);

                int min_scale_Y = (int)c[min_index].transform.localScale.y;
                int second_scale_Y = (int)c[j].transform.localScale.y;
                if (second_scale_Y < min_scale_Y)
                {
                    LeanTween.color(c[j], Color.green, 0.01f);
                    LeanTween.color(c[min_index], Color.white, 0.01f);
                    min_index = j;
                }

                yield return new WaitForSeconds(speed);
                if (min_index != j)
                    LeanTween.color(c[j], Color.white, 0.01f);
            }

            yield return new WaitForSeconds(speed);
            // Do Swap
            Temp = c[i];

            LeanTween.moveLocalX(c[i], c[min_index].transform.localPosition.x, speed);
            LeanTween.moveLocalZ(c[i], -1.5f, speed).setLoopPingPong(1);
            c[i] = c[min_index];

            LeanTween.moveLocalX(c[min_index], Temp.transform.localPosition.x, speed);
            LeanTween.moveLocalZ(c[min_index], +1.5f, speed).setLoopPingPong(1);
            c[min_index] = Temp;

            yield return new WaitForSeconds(speed);
            LeanTween.color(c[i], Color.green, 0.01f);
        }

        // Complete
        if (IsSorted(Cube))
        {
            yield return new WaitForSeconds(0.01f);
            StartCoroutine(Complete());
        }
    }
    public IEnumerator InsertionSort(List<GameObject> c)
    {
        for (int i = 1; i < c.Count; i++)
        {
            yield return new WaitForSeconds(speed);

            this.Temp = c[i];
            LeanTween.color(c[i], Color.green, 1f);
            LeanTween.moveLocalY(this.Temp,(c[i].transform.localScale.y / 2), speed);

            int j = i - 1;

            float temp = -100; // for animation

            while (j >= 0 && c[j].transform.localScale.y > this.Temp.transform.localScale.y)
            {
                yield return new WaitForSeconds(speed);

                LeanTween.moveLocalX(c[j], c[j].transform.localPosition.x + 1f, speed);
                c[j + 1] = c[j];

                temp = c[j].transform.localPosition.x;
                j--;
            }

            // for animation
            if (temp >= 0)
            {
                yield return new WaitForSeconds(speed);
                LeanTween.moveLocalX(this.Temp, temp, speed);
            }
            yield return new WaitForSeconds(speed);
            LeanTween.moveLocalY(this.Temp, this.Temp.transform.localScale.y / 2.0f, speed);

            LeanTween.color(c[i], Color.green, 1f);

            c[j + 1] = this.Temp;
        }

        yield return new WaitForSeconds(0.1f);
        if (IsSorted(Cube))
        {
            yield return new WaitForSeconds(0.001f);
            StartCoroutine(Complete());
        }

    }
    public IEnumerator QuickSort(List<GameObject> c, int left, int right)
    {
        if (left < right)
        {
            // Partiction Begin !!!!!
            int pivot = (int)c[right].transform.localScale.y;
            LeanTween.color(c[right], Color.white, 0.01f);

            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                yield return new WaitForSeconds(speed);
                LeanTween.color(c[j], Color.green, 0.01f);

                if (c[j].transform.localScale.y < pivot)
                {
                    yield return new WaitForSeconds(speed);
                    i++;
                    LeanTween.color(c[i], Color.white, 0.01f);

                    yield return new WaitForSeconds(speed * 1.5f);
                    // Swap
                    Temp = c[i];
                    Vector3 tempPosition = c[i].transform.localPosition;

                    LeanTween.moveLocalX(c[i], c[j].transform.localPosition.x, speed);
                    LeanTween.moveZ(c[i], -1.5f, speed).setLoopPingPong(1);
                    c[i] = c[j];

                    LeanTween.moveLocalX(c[j], tempPosition.x, speed);
                    LeanTween.moveZ(c[j], 1.5f, speed).setLoopPingPong(1);
                    c[j] = Temp;

                    yield return new WaitForSeconds(speed * 1.5f);
                    LeanTween.color(c[i], Color.white, 0.01f);
                }
                yield return new WaitForSeconds(speed);
                LeanTween.color(c[j], Color.white, 0.01f);
            }

            yield return new WaitForSeconds(speed * 1.5f);
            // Swap Again
            Temp = c[i + 1];
            Vector3 tP = c[i + 1].transform.localPosition;

            LeanTween.moveLocalX(c[i + 1], c[right].transform.localPosition.x, speed);
            LeanTween.moveZ(c[i + 1], -1.5f, speed).setLoopPingPong(1);
            c[i + 1] = c[right];

            LeanTween.moveLocalX(c[right], tP.x, speed);
            LeanTween.moveZ(c[right], 1.5f, speed).setLoopPingPong(1);
            c[right] = Temp;

            LeanTween.color(c[i + 1], Color.white, 0.01f);
            yield return new WaitForSeconds(speed * 1.5f);

            // Partiction End !!!

            int p = i + 1;
            yield return new WaitForSeconds(speed * 1.5f);
            StartCoroutine(QuickSort(c, p + 1, right));
            yield return new WaitForSeconds(speed * 1.5f);
            StartCoroutine(QuickSort(c, left, p - 1));
        }

        // Complete
        if (IsSorted(Cube))
        {
            yield return new WaitForSeconds(speed);
            StartCoroutine(Complete());
        }
    }
    public IEnumerator MergeSort(List<GameObject> c, int low, int high)
    {
        if (low < high)
        {
            yield return new WaitForSeconds(speed);
            int mid = (low + high) / 2;

            LeanTween.color(c[mid], Color.white, 0.01f);
            yield return new WaitForSeconds(speed);
            LeanTween.color(c[mid], Color.white, 0.01f);


            yield return MergeSort(c, low, mid);
            yield return MergeSort(c, mid + 1, high);

            yield return Merge(c, low, high, mid);
        }

        if (IsSorted(Cube))
        {
            yield return new WaitForSeconds(0.001f);
            StartCoroutine(Complete());
        }
    }

    public IEnumerator Merge(List<GameObject> c, int low, int high, int mid)
    {
        yield return new WaitForSeconds(speed);

        int leftIndex = low;
        int rightIndex = mid + 1;
        int mergeIndex = low;

        // Animation Part
        for (int i = low; i <= high; i++)
        {
            LeanTween.moveLocalZ(c[i], c[i].transform.localPosition.z + 1.5f, speed);
            LeanTween.color(c[i], Color.green, 0.01f);
        }
        //

        TempList = new GameObject[numberOfCubes];

        while (leftIndex <= mid && rightIndex <= high)
        {
            yield return new WaitForSeconds(speed);

            if (c[leftIndex].transform.localScale.y < c[rightIndex].transform.localScale.y)
            {
                LeanTween.color(c[leftIndex], Color.white, 0.01f);

                TempList[mergeIndex] = c[leftIndex];
                leftIndex++;
                yield return new WaitForSeconds(speed);
                LeanTween.color(c[leftIndex - 1], Color.white, 0.01f);
            }
            else
            {
                LeanTween.color(c[rightIndex], Color.white, 0.01f);

                TempList[mergeIndex] = c[rightIndex];
                rightIndex++;
                yield return new WaitForSeconds(speed);
                LeanTween.color(c[rightIndex - 1], Color.white, 0.01f);
            }
            mergeIndex++;
        }

        while (leftIndex <= mid)
        {
            LeanTween.color(c[leftIndex], Color.white, 0.01f);
            yield return new WaitForSeconds(speed);
            LeanTween.color(c[leftIndex], Color.white, 0.01f);

            TempList[mergeIndex] = c[leftIndex];
            mergeIndex++;
            leftIndex++;
        }

        while (rightIndex <= high)
        {
            LeanTween.color(c[rightIndex], Color.white, 0.01f);
            yield return new WaitForSeconds(speed);
            LeanTween.color(c[rightIndex], Color.white, 0.01f);
            TempList[mergeIndex] = c[rightIndex];
            mergeIndex++;
            rightIndex++;
        }

        for (int i = low; i < mergeIndex; i++)
        {
            yield return new WaitForSeconds(speed);
            LeanTween.moveLocalX(TempList[i], i, speed);

            c[i] = TempList[i];

            LeanTween.moveLocalZ(c[i], 0f, speed);
            LeanTween.color(c[i], Color.white, speed);
        }
    }
    void Init()
    {
        StopAllCoroutines();
        // Clear All Cubes
        if (Cube != null)
        {
            for (int i = 0; i < Cube.Count; i++)
                Destroy(Cube[i].gameObject);
            Cube.Clear();
        }

        Destroy(Temp);

        Cube = new List<GameObject>();

        for (int i = 0; i < numberOfCubes; i++)
        {
            int randomNumber = Random.Range(1,CubeHeightMax );

            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.localScale = new Vector3(0.9f, randomNumber, 1);
            cube.transform.position = new Vector3(i, randomNumber / 2.0f, 0);

            cube.transform.SetParent(this.transform);

            Cube.Add(cube);
        }
        transform.position=new Vector3(-numberOfCubes/2f,-CubeHeightMax/2.0f,0);
        Camera.main.transform.position = new Vector3(0,0,-45);
    }

    // Update is called once per frame
    void Update()
    {
        if (isSorting)
        {
            sorting_ui.SetActive(true);
            unsorting_ui.SetActive(false);
        }
        else
        {
            sorting_ui.SetActive(false);
            unsorting_ui.SetActive(true);
        }
    }
    
    private bool IsSorted(List<GameObject> o)
    {
        for (int i = 1; i < o.Count; i++)
        {
            int front = (int)o[i - 1].transform.localScale.y;
            int back = (int)o[i].transform.localScale.y;

            if (front > back)
                return false;
        }
        return true;
    }
    IEnumerator Complete()
    {
        for (int i = 0; i < Cube.Count; i++)
        {
            yield return new WaitForSeconds(0.03f);
            LeanTween.color(Cube[i], Color.green, 0.01f);
            LeanTween.moveLocalZ(Cube[i], 0, speed);
        }
        yield return new WaitForSeconds(0.05f);
        isSorting = false;
        StopAllCoroutines();
    }
}
