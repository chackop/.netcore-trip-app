using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TheWorld.Model
{
    public class WorldUser : IdentityUser
    {
        public DateTime FirstTrip { get; set; }
    }
}
