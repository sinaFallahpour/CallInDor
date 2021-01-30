using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Service.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CallInDoor.Hubs
{
    public class NotificationHub : Hub
    {

        #region ctor

        private readonly DataContext _context;
        private readonly UserManager<AppUser> _userManager;
        //private readonly ICommonService _commonService;

        public NotificationHub(UserManager<AppUser> userManager,
            DataContext context
            )
        {
            _context = context;
            _userManager = userManager;
            //_commonService = commonService;
        }

        #endregion ctor


        public async Task UserConnection(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var user = await _userManager.FindByIdAsync(id);
                ////._context..RestaurantRequests.SingleOrDefaultAsync(x => x.Id == id);

                if (user != null)
                {
                    user.ConnectionId = Context.ConnectionId;
                    //_context.Update(request);
                    await _context.SaveChangesAsync();
                }
            }
        }


    }
}
