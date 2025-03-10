using UnityEngine;
using UnityEngine.Events;

public class Life : MonoBehaviour
{
    public float amount;
    public UnityEvent onDeath;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void take_Damage(float damage) 
    {
        amount -= damage;
    }

    // Update is called once per frame
    void Update()
    {
        if (amount <= 0)
        {
            onDeath.Invoke();
            Destroy(gameObject);
        }
    }
}
