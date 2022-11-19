using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MvcCoreUploadAndDisplayImage_Demo.ViewModels
{
    public class PredictionViewModel
    {
        [Required(ErrorMessage = "Please enter your name")]
        [Display(Name = "Your Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter champion")]
        [Display(Name = "Champion")]
        public string Champion { get; set; }

        [Required(ErrorMessage = "Please enter top goal scorer")]
        [Display(Name = "Top goal scorer")]
        public string TopGoalScorer { get; set; }

        [Required(ErrorMessage = "Please choose excel prediction sheet")]
        [Display(Name = "Excel Prediction Sheet")]
        public IFormFile ExcelPrediction { get; set; }
    }
}
