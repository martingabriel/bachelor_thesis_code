using System;
using StudentsNotifier.Models;

namespace StudentsNotifier.ViewModels
{
    public class LectionRatingDetailViewModel : BaseViewModel
    {
        public LectionRating rating { get; set; }
        public LectionRatingDetailViewModel( LectionRating rating = null )
        {
            Title = rating?.LectionName;
            this.rating = rating;
        }
    }
}
