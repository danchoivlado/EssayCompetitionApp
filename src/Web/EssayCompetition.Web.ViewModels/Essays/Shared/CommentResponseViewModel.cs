using System;
using System.Collections.Generic;
using System.Text;

namespace EssayCompetition.Web.ViewModels.Essays.Shared
{
    public class CommentResponseViewModel
    {
        public int CommentsCount { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}
