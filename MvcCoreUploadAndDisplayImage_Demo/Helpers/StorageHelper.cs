﻿using Azure.Storage;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using MvcCoreUploadAndDisplayImage_Demo.Config;
using MvcCoreUploadAndDisplayImage_Demo.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreUploadAndDisplayImage_Demo.Helpers
{
    public static class StorageHelper
    {
        public static bool IsImage(IFormFile file)
        {
            if (file.ContentType.Contains("image"))
            {
                return true;
            }

            string[] formats = new string[] { ".jpg", ".png", ".gif", ".jpeg" };

            return formats.Any(item => file.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase));
        }

        public static async Task<bool> UploadFileToStorage(Stream fileStream, PostViewModel postViewModel,
                                                            AzureStorageConfig _storageConfig)
        {
            // Create a URI to the blob
            Uri blobUri = new Uri("https://" +
                                  _storageConfig.AccountName +
                                  ".blob.core.windows.net/" +
                                  _storageConfig.ImageContainer +
                                  "/" + postViewModel.PostImage.FileName);

            // Create StorageSharedKeyCredentials object by reading
            // the values from the configuration (appsettings.json)
            StorageSharedKeyCredential storageCredentials =
                new StorageSharedKeyCredential(_storageConfig.AccountName, _storageConfig.AccountKey);

            // Create the blob client.
            BlobClient blobClient = new BlobClient(blobUri, storageCredentials);

            try
            {
                // Upload the file
                await blobClient.UploadAsync(fileStream);
                var date = DateTime.UtcNow.ToString();
                // Set Metadata
                blobClient.SetMetadata(new Dictionary<string, string> {
                { "Title", postViewModel.Title },
                { "Description", postViewModel.Description },
                { "Redirect", postViewModel.UrlForRedirect },
                { "CreatedDate", DateTime.UtcNow.ToString() }});
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return await Task.FromResult(true);
        }

        public static async Task<bool> UploadFileToStorage(Stream fileStream, BannerViewModel bannerViewModel,
                                                            AzureStorageConfig _storageConfig)
        {
            // Create a URI to the blob
            Uri blobUri = new Uri("https://" +
                                  _storageConfig.AccountName +
                                  ".blob.core.windows.net/" +
                                  _storageConfig.ThumbnailsContainer +
                                  "/" + bannerViewModel.PostImage.FileName);

            // Create StorageSharedKeyCredentials object by reading
            // the values from the configuration (appsettings.json)
            StorageSharedKeyCredential storageCredentials =
                new StorageSharedKeyCredential(_storageConfig.AccountName, _storageConfig.AccountKey);

            // Create the blob client.
            BlobClient blobClient = new BlobClient(blobUri, storageCredentials);

            try
            {
                // Upload the file
                await blobClient.UploadAsync(fileStream);
                var date = DateTime.UtcNow.ToString();
                // Set Metadata
                blobClient.SetMetadata(new Dictionary<string, string> {
                { "Redirect", bannerViewModel.UrlForRedirect },
                { "CreatedDate", DateTime.UtcNow.ToString() }});
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return await Task.FromResult(true);
        }
    }
}
