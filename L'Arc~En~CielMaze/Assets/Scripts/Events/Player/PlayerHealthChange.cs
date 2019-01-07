public class PlayerHealthChange : CodeControl.Message
{
    public int totalHealth, currentHealth;

    public PlayerHealthChange(int totalHealth, int currentHealth)
    {
        this.totalHealth = totalHealth;
        this.currentHealth = currentHealth;
    }
}
