using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Knowledge_pantry.Models.ManageViewModels
{
    public class LectureSummariesViewModel
    {
        public string UserName { get; set; }
        public List<Summary> UserSummaries { get; set; }

        public LectureSummariesViewModel(string userName, List<Summary> userSummaries)
        {
            UserName = userName;
            UserSummaries = userSummaries;
        }
    }
}
