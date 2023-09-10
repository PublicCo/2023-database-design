using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp;

using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Fonts;


namespace test.Common
{
    public class Tools
    {
        public static string CreateValidateString()
        {
            string chars = "abcdefghijklmnopqrstuvwxyz";
            Random r = new(DateTime.Now.Millisecond);
            string validatestring = "";
            int length = 4;
            for (int i = 0;i<length;i++)
            {
                validatestring += chars[r.Next(chars.Length)];
            }
            return validatestring;
        }

        public static byte[] CreateValidateCodeBuffer(string validatecode)
        {
            using (var image = new Image<Rgba32>(200, 60))
            {

                image.Mutate(ctx => ctx
                    .BackgroundColor(Color.White)
                    .DrawText(
                    validatecode, SystemFonts.CreateFont("DejaVu Sans", 48, FontStyle.Bold | FontStyle.Italic), Color.Red, new PointF(10, 10)));

                using (var memoryStream = new MemoryStream())
                {
                    image.SaveAsJpeg(memoryStream);
                    return memoryStream.ToArray();
                }
            }
        }
    }
}
