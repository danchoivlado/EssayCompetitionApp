using EssayCompetition.Data.Models;
using EssayCompetition.Services.Mapping;
using Microsoft.AspNetCore.Mvc;

namespace EssayCompetition.Web.ViewModels.Contest.Create
{
    public class IndexViewModel : IMapTo<Essay>
    { 
        public string Title { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }
    }
}
