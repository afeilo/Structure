using System;

[AttributeUsage(AttributeTargets.Field, Inherited = true)]
public class FastObtainAttribute : Attribute
{
    public string monoName;

    public FastObtainAttribute()
    {

    }

    public FastObtainAttribute(string monoName)
    {
        this.monoName = monoName;
    }
}