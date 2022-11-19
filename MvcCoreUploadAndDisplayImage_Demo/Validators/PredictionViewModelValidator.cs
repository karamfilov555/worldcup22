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
            else
            {
                if (predictionViewModel.Name.Length > 49)
                {
                    vr.Succeeded = false;
                    vr.Errors.Add("Name max lenght 50 symbols");
                }
                else if(!Regex.IsMatch(predictionViewModel.Name, @"^[0-9a-zA-Z\s]+$"))
                {
                    vr.Succeeded = false;
                    vr.Errors.Add("Please use only latin latter and numbers.");
                }
            }
            if (string.IsNullOrWhiteSpace(predictionViewModel.Champion))
            {
                vr.Succeeded = false;
                vr.Errors.Add("Champion cannot be empty.");
            }
            else
            {
                if (predictionViewModel.Champion.Length > 49)
                {
                    vr.Succeeded = false;
                    vr.Errors.Add("Champion's name max lenght 50 symbols");
                }
                else if (!Regex.IsMatch(predictionViewModel.Champion, @"^[0-9a-zA-Z\s]+$"))
                {
                    vr.Succeeded = false;
                    vr.Errors.Add("Please use only latin latter and numbers.");
                }
            }
            if (string.IsNullOrWhiteSpace(predictionViewModel.TopGoalScorer))
            {
                vr.Succeeded = false;
                vr.Errors.Add("Top goal scorer cannot be empty.");
            }
            else
            {
                if (predictionViewModel.TopGoalScorer.Length > 49)
                {
                    vr.Succeeded = false;
                    vr.Errors.Add("TopGoalScorer's name max lenght 50 symbols");
                }
                else if (!Regex.IsMatch(predictionViewModel.TopGoalScorer, @"^[0-9a-zA-Z\s]+$"))
                {
                    vr.Succeeded = false;
                    vr.Errors.Add("Please use only latin latter and numbers.");
                }
            }
            if (predictionViewModel.ExcelPrediction is null)
            {
                vr.Succeeded = false;
                vr.Errors.Add("Excel prediction filed cannot be empty.");
            }

            return vr;
        }
    }
}
