using System;
using SkillGraph.SkillSystem.Data;
using SkillGraph.SkillSystem.Utilities;

namespace SkillGraph.SkillSystem.Models
{
    public class Skill : IEquatable<SkillData>, IEquatable<Skill>
    {
        public ObservableProperty<bool> Active { get; set; }

        public Skill[] AdjacentSkills { get; internal set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public int Price { get; private set; }

        public bool Persistent { get; private set; }

        private readonly string _identifier;

        private Skill(string identifier)
        {
            Active = new ObservableProperty<bool>();
            AdjacentSkills = Array.Empty<Skill>();

            _identifier = identifier;
        }

        public override bool Equals(object obj)
        {
            return obj is SkillData skillData && _identifier.Equals(skillData.Identifier);
        }

        public bool Equals(SkillData other)
        {
            return other != null && _identifier.Equals(other.Identifier);
        }

        public bool Equals(Skill other)
        {
            return other != null && _identifier.Equals(other._identifier);
        }

        public override int GetHashCode()
        {
            return _identifier.GetHashCode();
        }

        internal static Skill Create(SkillData skillData)
        {
            return new Skill(skillData.Identifier)
            {
                Name = skillData.Name,
                Description = skillData.Description,
                Price = skillData.Price,
                Persistent = skillData.Persistent,
                Active =
                {
                    Value = skillData.Persistent
                }
            };
        }
    }
}