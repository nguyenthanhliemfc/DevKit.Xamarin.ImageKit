﻿using Android.Graphics;
using DevKit.Xamarin.ImageKit.Abstractions;
using DevKit.Xamarin.ImageKit.Droid.Utils;
using System;
using System.Threading.Tasks;

namespace DevKit.Xamarin.ImageKit
{
    public class ImageResizer : IImageResizer
    {
        /// <summary>
        /// Reduces the quality of a Jpg image.
        /// </summary>
        /// <returns>The low quality image.</returns>
        /// <param name="originalImage">Original image.</param>
        /// <param name="newQuality">New quality from 1 to 100.</param>
        public async Task<byte[]> ReduceJPGQuality(byte[] originalImage, int newQuality)
        {
            if (newQuality > 100 || newQuality < 1)
                throw new Exception("Invalid quality value");

            Bitmap bitmap = await originalImage.ToBitmap();
            return await bitmap.ToByteArray(Abstractions.ImageFormat.JPG, newQuality);
        }

        /// <summary>
        /// Resizes an image.
        /// </summary>
        /// <returns>The resized image.</returns>
        /// <param name="originalImage">Original image.</param>
        /// <param name="newHeight">New height.</param>
        /// <param name="newWidth">New width.</param>
        /// <param name="imageFormat">Image format Jpg or Png.</param>
        public async Task<byte[]> ResizeImage(byte[] originalImage, int newHeight, int newWidth, Abstractions.ImageFormat imageFormat)
        {
            byte[] resultScaledImage = null;
            Bitmap bitmap = await originalImage.ToBitmap();
            Bitmap scaledBitmap = Bitmap.CreateScaledBitmap(bitmap, newWidth, newHeight, false);
            resultScaledImage = await scaledBitmap.ToByteArray(imageFormat);
            return resultScaledImage;
        }

        /// <summary>
        /// Scales an image.
        /// </summary>
        /// <returns>The scaled image.</returns>
        /// <param name="originalImage">Original image.</param>
        /// <param name="finalImagePercentage">Final image percentage.</param>
        /// <param name="imageFormat">Image format Jpg or Png.</param>
        public async Task<byte[]> ScaleImage(byte[] originalImage, double finalImagePercentage, Abstractions.ImageFormat imageFormat)
        {
            Bitmap bitmap = await originalImage.ToBitmap();
            int width = bitmap.Width;
            int height = bitmap.Height;
            int scaleWidth = Convert.ToInt16(width * (finalImagePercentage * .01));
            int scaleHeight = Convert.ToInt16(height * (finalImagePercentage * .01));
            return await ResizeImage(originalImage, scaleHeight, scaleWidth, imageFormat);
        }
    }
}