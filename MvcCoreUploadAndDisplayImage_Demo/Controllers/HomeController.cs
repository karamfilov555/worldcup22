using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MvcCoreUploadAndDisplayImage_Demo.Config;
using MvcCoreUploadAndDisplayImage_Demo.Helpers;
using MvcCoreUploadAndDisplayImage_Demo.Validators;
using MvcCoreUploadAndDisplayImage_Demo.ViewModels;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MvcCoreUploadAndDisplayImage_Demo.Controllers
{
    public class HomeController : Controller
    {
        private readonly AzureStorageConfig storageConfig;
        private readonly IToastNotification toast;
        private static bool isLogged = false;


        public HomeController(
            IOptions<AzureStorageConfig> options,
            IToastNotification toast)
        {
            this.toast = toast;
            storageConfig = options.Value;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (loginViewModel is null || 
                string.IsNullOrWhiteSpace(loginViewModel.Username) || 
                string.IsNullOrWhiteSpace(loginViewModel.Password))
            {
                toast.AddErrorToastMessage("Please fill all required fields!");
                return View("Index");
            }
            if (!loginViewModel.Username.Equals("dr.adm1n"))
            {
                toast.AddErrorToastMessage("Invalid credentials!");
                return View("Index");
            }
            if (!loginViewModel.Password.Equals("p4$$word"))
            {
                toast.AddErrorToastMessage("Invalid credentials!");
                return View("Index");
            }

            isLogged = true;
            toast.AddSuccessToastMessage("Welcome back Admin!");
            return View("Upload");
        }

        public async Task<IActionResult> UploadPrediction()
        {
            return View("Upload");
        }

        public async Task<IActionResult> UploadedPrediction()
        {
            return View("Upload");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadPrediction(PredictionViewModel model)
        {
            if (!isLogged)
            {
                toast.AddErrorToastMessage("Please login");
                return View("Index");
            }
            if (DateTime.UtcNow > new DateTime(2022, 11, 20, 16, 00, 00))
            {
                toast.AddErrorToastMessage("It's too late the FIFA World Cup Qatar 2022™ has already started, enjoy it! Don't try to cheat!");
                return View("Upload");
            }


            bool isUploaded = false;

            var vr = PredictionViewModelValidator.Validate(model);

            try
            {
                if (vr.Succeeded)
                {
                    if (storageConfig.AccountKey == string.Empty || storageConfig.AccountName == string.Empty)
                        return BadRequest("sorry, can't retrieve your azure storage details from appsettings.js, make sure that you add azure storage details there");

                    if (storageConfig.BetContainer == string.Empty)
                        return BadRequest("Please provide a name for your container in the azure blob storage");

                    if (StorageHelper.IsImage(model.ExcelPrediction))
                    {
                        if (model.ExcelPrediction.Length > 0)
                        {
                            using Stream stream = model.ExcelPrediction.OpenReadStream();
                            isUploaded = await StorageHelper.UploadFileToStorage(stream, model, storageConfig);
                        }
                        else
                        {
                            return new UnsupportedMediaTypeResult();
                        }
                    }
                    else
                    {
                        toast.AddErrorToastMessage($"Unsupported file format! Please provide one of the following file formats: \".xlsx\"");
                        return Redirect("UploadedPrediction");
                    }
                }
                else
                {
                    toast.AddErrorToastMessage("Please fill all required fields!");
                    return View("Upload");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("The specified blob already exists."))
                {
                    toast.AddErrorToastMessage("Invalid Excel Prediction name! Please rename your file in the following format \"YourName.xlsx\". Example: IvanIvanov.xlsx");
                    return View("Upload");
                }
                toast.AddErrorToastMessage(ex.Message);
                return View("Upload");
            }
            if (isUploaded)
            {
                toast.AddSuccessToastMessage("Your prediction is uploaded succesfully! Good luck!");
            }
            else
            {
                toast.AddErrorToastMessage("Something went wrong!");
            }
            return Redirect("UploadedPrediction");
        }
    }
}
