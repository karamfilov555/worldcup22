﻿namespace MvcCoreUploadAndDisplayImage_Demo.Config
{
    public class AzureStorageConfig
    {
        public string AccountName { get; set; }
        public string AccountKey { get; set; }
        public string ImageContainer { get; set; }
        public string ThumbnailsContainer { get; set; }
    }
}
