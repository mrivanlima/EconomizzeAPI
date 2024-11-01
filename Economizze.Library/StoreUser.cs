using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Economizze.Library
{
    public class StoreUser
    {
        public int StoreId { get; set; }
        public int StoreUserId { get; set; }
        public string UserName { get; set; } = string.Empty;

        public StoreUser(int storeId, int storeUserId, string userName)
        {
            StoreId = storeId;
            StoreUserId = storeUserId;
            UserName = userName;
        }
    }
}
