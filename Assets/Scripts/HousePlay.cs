using UnityEngine;


public class HousePlay : MonoBehaviour, CardPlay
{

    private GameObject housePrefab;
    private GameManager gameManager;

    void Start()
    {
        housePrefab = (GameObject) Resources.Load("House");
        gameManager = FindAnyObjectByType<GameManager>();
        if (gameManager == null)
        {
            throw new System.ArgumentNullException("game manager not found");
        }
    }
    public void Play()
    {
        if(gameManager.Buy(1)) {
            Instantiate(housePrefab, new Vector3(0,0,0), Quaternion.identity);
        }
    }
}
