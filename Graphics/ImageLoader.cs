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

        static Bitmap GetImage(string url)
        {
            if (!cache.ContainsKey(url))
            {
                string filePath = $"Images/{url}";
                cache.Add(url, new Bitmap(Image.FromFile(filePath)));
            }

            return cache[url];
        }

        static void ClearCache()
        {
            cache.Clear();
        }

        static Bitmap Empty(int x, int y)
        {
            if (!cache.ContainsKey("empty"))
            {
                Bitmap bitmap = new Bitmap(x, y);
                System.Drawing.Graphics g;
                g = System.Drawing.Graphics.FromImage(bitmap);
                g.Clear(System.Drawing.Color.Green);
                bitmap = new Bitmap(x, y, g);
                cache.Add("empty", bitmap);
            }

            Bitmap b = (Bitmap)cache["empty"].Clone();
            return b;
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
