using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcApplication1.Models
{
    public class HomeModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Email: (*)")]
        public string EmailUser { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Places of interest in Ho Chi Minh city (Địa điểm nổi tiếng ở TP.HCM): (*)")]
        public string PlaceId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Weather (Thời tiết): (*)")]
        public string WeatherId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Budget (Kinh phí): (*)")]
        public string BudgetId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Companion (Bạn đồng hành): (*)")]
        public string CompanionId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Time (Thời gian đi): (*)")]
        public string TravelTime { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Rating (Điểm đánh giá): (*)")]
        public string Rating { get; set; }
    }
}
