using UnityEngine;


public class CollectCollectible : MonoBehaviour
{
    [SerializeField] private int _val = 1;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            Debug.Log(gameObject.tag);
            switch (gameObject.tag)
            {
                case "GoldCoin":
                    CollectibleCounter.instance.IncreaseCoinCount(_val, 'g');
                    break;
                case "SilverCoin":
                    CollectibleCounter.instance.IncreaseCoinCount(_val, 's');
                    break;
                case "Diamond":
                    CollectibleCounter.instance.IncreaseCoinCount(_val, 'd');
                    break;
            }
            
        }
    }
}
