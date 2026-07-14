using System;

public static class uFSMActionFactory
{
    public static uFSMAction Create(Type t)
    {
        return (uFSMAction)Activator.CreateInstance(t);
    }
}