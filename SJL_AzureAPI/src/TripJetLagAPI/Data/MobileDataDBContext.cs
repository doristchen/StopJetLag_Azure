using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TripJetLagAPI.Data
{
    public class MobileDataDBContext : DbContext
    {
        public MobileDataDBContext(DbContextOptions<MobileDataDBContext> options) :
            base(options)
        {
        }
    }
}
