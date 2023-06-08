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
    public class ReviewsController : Controller
    {
        private readonly GamesCollectionV3Context _context;
        private readonly IRepositoryManager _repositoryManager;

        public ReviewsController(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<IActionResult> Index(List<Review> reviews)
        {
            return PartialView(reviews);
        }

        public IActionResult Create()
        {
            return PartialView();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Username,Timestamp,Description,GameId")] Review review)
        {
            // Check if the model state is valid, i.e., if the provided data passes the validation rules defined in the model.
            if (ModelState.IsValid)
            {
                // Set the Timestamp property of the review to the current date and time.
                review.Timestamp = DateTime.Now;

                // Create the review using the Review repository in the repository manager.
                _repositoryManager.Review.CreateReview(review);

                // Save the changes made to the repository asynchronously.
                await _repositoryManager.SaveAsync();

                // Redirect to the details of the game which the added review belongs to.
                return RedirectToAction("Details", "Games", new { id = review.GameId});
            }

            // If the model state is not valid, return the view with the submitted review object to display validation errors.
            return View(review);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Retrieve the review with the specified 'id' from the Review repository in the repository manager.
            var review = await _repositoryManager.Review.GetReviewByIdAsync(id.Value);

            return review != null ?
                View(review) :
                NotFound();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,Timestamp,Description,GameId")] Review review)
        {
            if (id != review.Id)
            {
                // If the IDs don't match, return a NotFound result, indicating that the requested resource was not found.
                return NotFound();
            }

            // Check if the model state is valid, i.e., if the provided data passes the validation rules defined in the model.
            if (ModelState.IsValid)
            {
                try
                {
                    // Update the review using the Review repository in the repository manager.
                    _repositoryManager.Review.UpdateReview(review);
                    
                    // Save the changes made to the repository asynchronously.
                    await _repositoryManager.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Handle the case where the review being updated no longer exists in the database
                    if (!ReviewExists(review.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                // Redirect to the details of the game which the edited review belongs to.
                return RedirectToAction("Details", "Games", new { id = review.GameId });
            }
            // If the model state is not valid, return the view with the submitted review object to display validation errors.
            return View(review);
        }



        // GET: Reviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            // Retrieve the review with the specified 'id' from the Review repository in the repository manager.
            var review = await _repositoryManager.Review.GetReviewByIdAsync(id.Value);

            // If the review exists, return the Delete view, passing the retrieved review object.
            return review != null ?
                View(review) :
                NotFound();
        }

        

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Retrieve the review with the specified 'id' from the Review repository in the repository manager.
            var review = await _repositoryManager.Review.GetReviewByIdAsync(id);
            if (review != null)
            {
                // Delete the review using the Review repository in the repository manager.
                _repositoryManager.Review.DeleteReview(review);

                // Save the changes made to the repository asynchronously.
                await _repositoryManager.SaveAsync();
            }

            // Redirect to the details of the game which the deleted review belongs to.
            return RedirectToAction("Details", "Games", new { id = review.GameId});
        }

        private bool ReviewExists(int id)
        {
          return (_context.Review?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
