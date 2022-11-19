using MvcCoreUploadAndDisplayImage_Demo.ViewModels;
using System.Text.RegularExpressions;

namespace MvcCoreUploadAndDisplayImage_Demo.Validators
{
    public static class PredictionViewModelValidator
    {
        public static ValidationResult Validate(PredictionViewModel predictionViewModel)
        {
            var vr = new ValidationResult();

            if (predictionViewModel is null)
            {
                vr.Succeeded = false;
                vr.Errors.Add("Prediction cannot be null");
            }
            if (string.IsNullOrWhiteSpace(predictionViewModel.Name))
            {
                vr.Succeeded = false;
                vr.Errors.Add("Name cannot be empty.");
            }
            if (string.IsNullOrWhiteSpace(predictionViewModel.Champion))
            {
                vr.Succeeded = false;
                vr.Errors.Add("Champion cannot be empty.");
            }
            if (string.IsNullOrWhiteSpace(predictionViewModel.TopGoalScorer))
            {
                vr.Succeeded = false;
                vr.Errors.Add("Top goal scorer cannot be empty.");
            }
            if (predictionViewModel.ExcelPrediction is null)
            {
                vr.Succeeded = false;
                vr.Errors.Add("Excel prediction filed cannot be empty.");
            }

            if (!Regex.IsMatch(predictionViewModel.Name, @"^[a-zA-Z0-9]+$"))
            {
                vr.Succeeded = false;
                vr.Errors.Add("Please use only latin latter and numbers.");
            }
            if (!Regex.IsMatch(predictionViewModel.Champion, @"^[a-zA-Z0-9]+$"))
            {
                vr.Succeeded = false;
                vr.Errors.Add("Please use only latin latter and numbers.");
            }
            if (!Regex.IsMatch(predictionViewModel.TopGoalScorer, @"^[a-zA-Z0-9]+$"))
            {
                vr.Succeeded = false;
                vr.Errors.Add("Please use only latin latter and numbers.");
            }
            return vr;
        }
    }
}
