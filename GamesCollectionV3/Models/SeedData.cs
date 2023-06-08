using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using GamesCollectionV3.Data;
using System;
using System.Linq;

namespace GamesCollectionV3.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new GamesCollectionV3Context(
                serviceProvider.GetRequiredService<
                    DbContextOptions<GamesCollectionV3Context>>()))
            {
                // Look for any movies.
                if (context.Game.Any())
                {
                    return;   // DB has been seeded
                }

                context.Game.AddRange(
                    new Game
                    {
                        Title = "Ghost of Tsushima",
                        ReleaseDate = DateTime.Parse("2020-07-20"),
                        Developer = "Sucker Punch Productions",
                        Platforms = "PlayStation 5, Playstation 4",
                        Genre = "Action/Adventure/RPG",
                        Description = "Ghost of Tsushima is a 2020 action-adventure game developed by Sucker Punch Productions and published by Sony Interactive Entertainment"
                    },

                    new Game
                    {
                        Title = "Forza Horizon 5",
                        ReleaseDate = DateTime.Parse("2021-11-01"),
                        Developer = "Playground Games",
                        Platforms = "Xbox Series X, Windows",
                        Genre = "Racing/Adventure",
                        Description = "Forza Horizon 5 is a 2021 racing video game developed by Playground Games and published by Xbox Game Studios."
                    },

                    new Game
                    {
                        Title = "The Witcher 3",
                        ReleaseDate = DateTime.Parse("2015-05-18"),
                        Developer = "CD Projekt RED",
                        Platforms = "PlayStation 4, Xbox Series X, Windows",
                        Genre = "RPG/Adventure",
                        Description = "The Witcher 3: Wild Hunt is a 2015 action role-playing game developed and published by CD Projekt."
                    },

                    new Game
                    {
                        Title = "Elden Ring",
                        ReleaseDate = DateTime.Parse("2022-02-25"),
                        Developer = "FromSoftware",
                        Platforms = "PlayStation 5, Xbox Series X, Windows",
                        Genre = "RPG/Adventure",
                        Description = "Elden Ring is a 2022 action role-playing game developed by FromSoftware."
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
