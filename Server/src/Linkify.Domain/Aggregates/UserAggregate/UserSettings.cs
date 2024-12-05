using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Domain.Aggregates.UserAggregate
{
    public class UserSettings
    {
        public bool IsPrivate { get; private set; }

        public UserSettings()
        {
            IsPrivate = false;
        }

        public void SetPrivacy(bool isPrivate)
        {
            IsPrivate = isPrivate;
        }
    }
}
