using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Core;

namespace mantis_tests
{
    public class ProjectData : IEquatable<ProjectData>, IComparable<ProjectData>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ProjectData(string name)
        {
            Name = name;
        } 

        // Для сравнения объектов типа ProjectData
        public bool Equals(ProjectData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return Name == other.Name && Description == other.Description;

        }
        public int CompareTo(ProjectData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            int result = Name.CompareTo(other.Name);
            if (result == 0)
            {
                result = Description.CompareTo(other.Description);
            }
            if (result == 0)
            {
                result = Id.CompareTo(other.Id);
            }
            return result;
        }

    }
}
