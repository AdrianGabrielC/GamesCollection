using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GamesCollectionV3.Models;

namespace GamesCollectionV3.Data
{
    public class GamesCollectionV3Context : DbContext
    {
        public GamesCollectionV3Context (DbContextOptions<GamesCollectionV3Context> options)
            : base(options)
        {
        }

        public DbSet<GamesCollectionV3.Models.Game> Game { get; set; } = default!;

        public DbSet<GamesCollectionV3.Models.Review>? Review { get; set; }
    }
}
