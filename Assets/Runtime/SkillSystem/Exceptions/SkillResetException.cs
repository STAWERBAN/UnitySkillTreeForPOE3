using System;

namespace SkillGraph.SkillSystem.Exceptions
{
    public class SkillResetException : Exception
    {
        private const string Message = "Some of child is not connected with head : ";

        public SkillResetException(string childName) 
            : base(Message + childName)
        {
        }
    }
}