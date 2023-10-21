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
                    CollectibleCounter.instance.IncreaseCount(_val, 'g');
                    break;
                case "SilverCoin":
                    CollectibleCounter.instance.IncreaseCount(_val, 's');
                    break;
                case "Diamond":
                    CollectibleCounter.instance.IncreaseCount(_val, 'd');
                    break;
                case "Key":
                    CollectibleCounter.instance.IncreaseCount(_val, 'k');
                    break;
            }
            
        }
    }
}
