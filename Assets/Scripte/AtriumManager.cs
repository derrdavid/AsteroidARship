using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AtriumManager : MonoBehaviour
{
    public int maxHealth = 100;
    [SerializeField] private BarController barController;
    private void Start()
    {
        barController.setValue(maxHealth);
    }
    public void takeDamage(int damage)
    {
        barController.decrease(damage);
    }

    public void heal(int heal)
    {
        barController.increase(heal);
    }

    public int getCurrentHealth()
    {
        return (int)barController.getCurrentHealth();
    }
}
