using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Graphics
{
    static class ImageLoader
    {
        static private Dictionary<string, Bitmap> cache = new Dictionary<string, Bitmap>();

        public static Bitmap GetImage(string url)
        {
            if (!cache.ContainsKey(url))
            {
                string filePath = $"{url}";
                cache.Add(url, new Bitmap(Image.FromFile(filePath)));
            }

            return cache[url];
        }

        static void ClearCache()
        {
            cache.Clear();
        }

        public static Bitmap Empty(int x, int y)
        {
            if (!cache.ContainsKey("empty"))
            {
                Bitmap bitmap = new Bitmap(x, y);
                using (System.Drawing.Graphics gfx = System.Drawing.Graphics.FromImage(bitmap))
                using (SolidBrush brush = new SolidBrush(System.Drawing.Color.FromArgb(112, 56, 11)))
                {
                    gfx.FillRectangle(brush, 0, 0, x, y);
                }
                cache.Add("empty", bitmap);
            }
            return (Bitmap)cache["empty"].Clone();
        }

        public static BitmapSource CreateBitmapSourceFromGdiBitmap(Bitmap bitmap)
        {
            if (bitmap == null)
                throw new ArgumentNullException("bitmap");

            var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            var bitmapData = bitmap.LockBits(
                rect,
                ImageLockMode.ReadWrite,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            try
            {
                var size = (rect.Width * rect.Height) * 4;

                return BitmapSource.Create(
                    bitmap.Width,
                    bitmap.Height,
                    bitmap.HorizontalResolution,
                    bitmap.VerticalResolution,
                    PixelFormats.Bgra32,
                    null,
                    bitmapData.Scan0,
                    size,
                    bitmapData.Stride);
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }
        }
    }
}
