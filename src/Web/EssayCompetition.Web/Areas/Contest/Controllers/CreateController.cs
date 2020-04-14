namespace EssayCompetition.Web.Areas.Contest.Controllers
{
    using EssayCompetition.Web.ViewModels.Contest.Create;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class CreateController : ContestController
    {
        private const string ContentName = "_EssayContent";
        private const string Title = "_EssayTitle";
        private const string Description = "_EssayDescription";

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel();
            var title = this.HttpContext.Session.GetString(Title);
            var description = this.HttpContext.Session.GetString(Description);
            var content = this.HttpContext.Session.GetString(ContentName);

            viewModel.Title = title;
            viewModel.Description = description;
            viewModel.Content = content;

            return this.View(viewModel);
        }

        public IActionResult ReviewedEssay(IndexViewModel viewModel, string button)
        {
            if (button == "Save")
            {
                this.PopulateSession(viewModel);
                this.TempData["FormResult"] = "Successfully saved in session.";

                return this.RedirectToAction("Index");
            }

            return this.View();
        }

        private void PopulateSession(IndexViewModel viewModel)
        {
            if (viewModel.Title != null)
            {
                this.HttpContext.Session.SetString(Title, viewModel.Title);
            }

            if (viewModel.Description != null)
            {
                this.HttpContext.Session.SetString(Description, viewModel.Description);
            }

            if (viewModel.Content != null)
            {
                this.HttpContext.Session.SetString(ContentName, viewModel.Content);
            }
        }
    }
}
