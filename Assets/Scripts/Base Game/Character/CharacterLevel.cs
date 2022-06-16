using System;

public class CharacterLevel : CharacterSetup
{
    public int Level;
    public int Exp;
    public int MaxExp;
    public float ExpMultiplier;
    
    public Action OnLevelUp;
    public Action WhenGetExp;
    
    public void LevelUp(int exp)
    {
        Exp += exp;
        WhenGetExp?.Invoke();
        if (Exp >= MaxExp)
        {
            Level++;
            MaxExp = (int)(MaxExp * ExpMultiplier);
            Exp = 0;
            OnLevelUp?.Invoke();
        }
    }
    
    protected override void OnStart()
    {
        
    }

    protected override void OnUpdate()
    {
        
    }
}