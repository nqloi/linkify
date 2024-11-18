using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Domain.Aggregates.UserAggregate
{
    public class Friendship
    {
        public Guid FriendId { get; private set; }
        public DateTime FriendshipDate { get; private set; }

        public Friendship()
        {
        }

        public Friendship(Guid friendId)
        {
            FriendId = friendId;
            FriendshipDate = DateTime.UtcNow;
        }
    }
}
