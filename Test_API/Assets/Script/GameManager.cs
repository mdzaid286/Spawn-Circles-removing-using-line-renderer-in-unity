using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject circlePrefab;
    // public GameObject restartButton;

    private bool isDrawing = false;
    private Vector2 previousPosition;

    void Start()
    {
        SpawnCircles(Random.Range(5, 11));
        // restartButton.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDrawing = true;
            previousPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDrawing = false;
            CheckIntersection();
        }

        if (isDrawing)
        {
            Vector2 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            DrawLine(previousPosition, currentPosition);
            previousPosition = currentPosition;
        }
    }
void DrawLine(Vector2 start, Vector2 end)
{
    LineRenderer lineRenderer = GetComponent<LineRenderer>();
    lineRenderer.positionCount = 2;
    lineRenderer.SetPosition(0, start);
    lineRenderer.SetPosition(1, end);
}


    void SpawnCircles(int count)
{
    for (int i = 0; i < count; i++)
    {
        Vector2 position = new Vector2(Random.Range(-5f, 5f), Random.Range(-3f, 3f));
        Instantiate(circlePrefab, position, Quaternion.identity);
    }
}


    void CheckIntersection()
{
    Collider2D[] intersectedColliders = Physics2D.OverlapAreaAll(previousPosition, Camera.main.ScreenToWorldPoint(Input.mousePosition));
    foreach (Collider2D collider in intersectedColliders)
    {
        if (collider.CompareTag("Circle"))
        {
            DestroyCircle(collider.gameObject);
        }
    }
}


    void DestroyCircle(GameObject circle)
    {
        Destroy(circle);
    }

//     public void Restart()
// {
//       restartButton.SetActive(true);
//     foreach (GameObject circle in GameObject.FindGameObjectsWithTag("Circle"))
//     {
//         Destroy(circle);
//     }
//     SpawnCircles(Random.Range(5, 11));
// }

}
