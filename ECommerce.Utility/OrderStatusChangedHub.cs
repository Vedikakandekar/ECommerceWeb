using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Utility
{
    public class OrderStatusChangedHub:Hub
    {
        public async Task SendOrderStatusUpdate(string userId, string orderStatus,int OrderItemId,string ProductName)
        {
            await Clients.User(userId).SendAsync("ReceiveStatusUpdate", orderStatus,OrderItemId, ProductName);
        }
        public async Task SendOrderStatusNotification(string userId, string orderStatus, int OrderItemId, string ProductName)
        {
            await Clients.User(userId).SendAsync("ReceiveStatusNotification", orderStatus, OrderItemId, ProductName);
        }
    }
}
