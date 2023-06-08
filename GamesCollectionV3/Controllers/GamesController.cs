using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GamesCollectionV3.Data;
using GamesCollectionV3.Models;
using GamesCollectionV3.Repositories;

namespace GamesCollectionV3.Controllers
{
    public class GamesController : Controller
    {
        private readonly GamesCollectionV3Context _context;
        private readonly IRepositoryManager _repositoryManager;

        public GamesController(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        // GET: Games
        public async Task<IActionResult> Index(int? pageNumber)
        {
            // Define the number of games to be displayed per page.
            int pageSize = 3;

            // Retrieve the games for the specified page using the Game repository in the repository manager.
            var games = await _repositoryManager.Game.GetGamePage(pageSize, pageNumber ?? 1);

            // If the games exist, return the Index view, passing the retrieved games object.
            return games != null ?
                View(games) :
                Problem("Entity set 'GameCollectionV2Context.Game'  is null.");

        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                // If the 'id' is null, return a NotFound result, indicating that the requested resource was not found.
                return NotFound();
            }

            // Retrieve the game with the specified 'id' along with its associated reviews from the Game repository in the repository manager.
            var game = await _repositoryManager.Game.GetGameWithReviewsAsync(id.Value);

            // If the game exists, return the Details view, passing the retrieved game object.
            return game != null ?
                View(game) :
                NotFound();

        }

        // GET: Games/Create
        public IActionResult Create()
        {
            return View();
        }



        // POST: Games/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Developer,Platforms,Genre,Description")] Game game)
        {
            if (ModelState.IsValid)
            {
                // Create the game using the Game repository in the repository manager.
                _repositoryManager.Game.CreateGame(game);

                // Save the changes made to the repository asynchronously.
                await _repositoryManager.SaveAsync();

                // Redirect to the Index action method.
                return RedirectToAction(nameof(Index));
            }
            return View(game);
        }


        // GET: Games/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                // If the 'id' is null, return a NotFound result, indicating that the requested resource was not found.
                return NotFound();
            }

            // Retrieve the game with the specified 'id' from the Game repository in the repository manager.
            var game = await _repositoryManager.Game.GetGameByIdAsync(id.Value);

            // If the game exists, return the Edit view, passing the retrieved game object.
            return game != null ?
                View(game) :
                NotFound();
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Developer,Platforms,Genre,Description")] Game game)
        {
            if (id != game.Id)
            {
                // If the 'id' is null, return a NotFound result, indicating that the requested resource was not found.
                return NotFound();
            }

            // Check if the model state is valid, i.e., if the provided data passes the validation rules defined in the model.
            if (ModelState.IsValid)
            {
                try
                {
                    // Update the game using the Game repository in the repository manager.
                    _repositoryManager.Game.UpdateGame(game);

                    // Save the changes made to the repository asynchronously.
                    await _repositoryManager.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                // If the game was successfully updated, redirect to the Index action method.
                return RedirectToAction(nameof(Index));
            }
            // If the model state is not valid, return the view with the submitted game object to display validation errors.
            return View(game);
        }

        // GET: Games/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                // If the 'id' is null, return a NotFound result, indicating that the requested resource was not found.
                return NotFound();
            }
            // Retrieve the game with the specified 'id' from the Game repository in the repository manager.
            var game = await _repositoryManager.Game.GetGameByIdAsync(id.Value);

            // If the game exists, return the Delete view, passing the retrieved game object.
            return game != null ?
                View(game) :
                NotFound();
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Retrieve the game with the specified 'id' from the Game repository in the repository manager.
            var game = await _repositoryManager.Game.GetGameByIdAsync(id);
            if (game != null)
            {
                // If the game exists, delete the game using the Game repository in the repository manager.
                _repositoryManager.Game.DeleteGame(game);

                // Save the changes made to the repository asynchronously.
                await _repositoryManager.SaveAsync();
            }

            // Redirect to the Index action method.
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(int id)
        {
            return (_context.Game?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
