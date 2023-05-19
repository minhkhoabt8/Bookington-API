using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Hubs
{
    public interface IChatHub
    {
        Task AddToGroup(string groupName);
        Task SendMessageToGroup(string groupName, string sender, string message);
    }
}
