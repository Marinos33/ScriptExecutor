﻿namespace ScriptExecutor.Interfaces
{
    public interface ILogManager
    {
        string ReadLog();

        void AddLog(string text);
    }
}