using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Knowledge_pantry.Models.SummaryOptionsViewModels
{
    public class ViewAndEditSummaryViewModel
    {
        public Summary Lecture { get; set; }
        public List<Comment> LectureComments { get; set; }

        public ViewAndEditSummaryViewModel(Summary summary, List<Comment> comments)
        {
            Lecture = summary;
            LectureComments = comments;
        }
    }
}
