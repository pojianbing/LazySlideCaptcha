using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Drawing.Processing;
using System;
using System.Collections.Generic;
using System.Text;
using SixLabors.ImageSharp.Formats;
using Lazy.SlideCaptcha.Core.Resources;

namespace Lazy.SlideCaptcha.Core.Generator
{
    public class DefaultCaptchaImageGenerator : ICaptchaImageGenerator
    {
        private IResourceManager _resourceManager;
        private Random _random = new Random();

        public DefaultCaptchaImageGenerator(IResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }

        /// <summary>
        /// 计算凹槽轮廓
        /// 原理： 一行一行扫描，每行不透明小方块连接形成数个小长方形（RectangularPolygon）。
        ///       多个RectangularPolygon形成ComplexPolygon，ComplexPolygon则代表图形的轮廓
        /// </summary>
        /// <param name="holeTemplateImage"></param>
        /// <returns></returns>
        private static ComplexPolygon CalcHoleShape(Image<Rgba32> holeTemplateImage)
        {
            int temp = 0;
            var pathList = new List<IPath>();
            holeTemplateImage.ProcessPixelRows(accessor =>
            {
                for (int y = 0; y < holeTemplateImage.Height; y++)
                {
                    var rowSpan = accessor.GetRowSpan(y);
                    for (int x = 0; x < rowSpan.Length; x++)
                    {
                        ref Rgba32 pixel = ref rowSpan[x];
                        if (pixel.A != 0)
                        {
                            if (temp == 0)
                            {
                                temp = x;
                            }
                        }
                        else
                        {
                            if (temp != 0)
                            {
                                pathList.Add(new RectangularPolygon(temp, y, x - temp, 1));
                                temp = 0;
                            }
                        }
                    }
                }
            });

            return new ComplexPolygon(new PathCollection(pathList));
        }

        public CaptchaImageData Generate()
        {
            var background = _resourceManager.RandomBackground();
            (var silder, var hole) = _resourceManager.RandomTemplate();

            using var backgroundImage = Image.Load<Rgba32>(background);
            using var sliderTemplateImage = Image.Load<Rgba32>(silder);
            using var holeTemplateImage = Image.Load<Rgba32>(hole);
            using var holeMattingImage = new Image<Rgba32>(sliderTemplateImage.Width, sliderTemplateImage.Height);
            using var sliderBarImage = new Image<Rgba32>(sliderTemplateImage.Width, backgroundImage.Height);

            // 凹槽位置
            int randomX = _random.Next(holeTemplateImage.Width + 5, backgroundImage.Width - holeTemplateImage.Width - 10);
            int randomY = _random.Next(5, backgroundImage.Height - holeTemplateImage.Height - 5);

            // 根据透明度计算凹槽图轮廓形状(形状由不透明区域形成)
            var holeShape = CalcHoleShape(holeTemplateImage);
            // 生成凹槽抠图
            holeMattingImage.Mutate(x =>
            {
                x.Clip(holeShape, p => p.DrawImage(backgroundImage, new Point(-randomX, -randomY), 1));
            });
            // 叠加拖块模板
            holeMattingImage.Mutate(x => x.DrawImage(sliderTemplateImage, new Point(0, 0), 1));
            // 绘制拖块条
            sliderBarImage.Mutate(x => x.DrawImage(holeMattingImage, new Point(0, randomY), 1));

            // 生成背景
            backgroundImage.Mutate(x => x.DrawImage(holeTemplateImage, new Point(randomX, randomY), 1));

            return new CaptchaImageData
            {
                X = randomX,
                Y = randomY,
                BackgroundImageWidth = backgroundImage.Width,
                BackgroundImageHeight = backgroundImage.Height,
                BackgroundImageBase64 = backgroundImage.ToBase64String(SixLabors.ImageSharp.Formats.Png.PngFormat.Instance),
                SliderImageWidth = holeMattingImage.Width,
                SliderImageHeight = holeMattingImage.Height,
                SliderImageBase64 = sliderBarImage.ToBase64String(SixLabors.ImageSharp.Formats.Png.PngFormat.Instance)
            };
        }
    }
}
