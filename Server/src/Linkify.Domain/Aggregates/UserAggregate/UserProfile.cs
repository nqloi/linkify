using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Domain.Aggregates.UserAggregate
{
    public class UserProfile
    {
        public string FullName { get; private set; }
        public string Bio { get; private set; }

        public UserProfile(string fullName, string bio)
        {
            FullName = fullName;
            Bio = bio;
        }

        public void UpdateBio(string newBio)
        {
            Bio = newBio;
        }
    }
}
