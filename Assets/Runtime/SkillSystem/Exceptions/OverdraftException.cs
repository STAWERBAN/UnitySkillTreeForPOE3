using System;

namespace SkillGraph.SkillSystem.Exceptions
{
    public class OverdraftException : Exception
    {
        private const string Message = "Cant to purchase overprice skill";

        public OverdraftException() 
            : base(Message)
        {
        }
    }
}