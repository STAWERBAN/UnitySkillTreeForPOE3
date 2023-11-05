using System;

namespace SkillGraph.SkillSystem.Exceptions
{
    public class SkillResetException : Exception
    {
        private const string Message = "Skill reset exception";

        public SkillResetException() : base(Message)
        {
        }
    }
}