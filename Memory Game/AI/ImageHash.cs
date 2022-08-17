using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory_Game.AI
{
    class ImageHash
    {
        public static int HashSize = 16;
        public List<bool> Hash { get; set; }
        public string HashString
        {
            get
            {
                string s = "";
                foreach (var item in Hash)
                {
                    s += item ? 1 : 0;
                }
                return s;
            }
        }

        /// <summary>
        /// Létrehoz egy képből egy hash-t.
        /// </summary>
        /// <param name="image"></param>
        public ImageHash(Image image)
        {
            Hash = new List<bool>();
            using (Bitmap bmpMin = new Bitmap(image, new Size(HashSize, HashSize)))
            {
                for (int j = 0; j < bmpMin.Height; j++)
                {
                    for (int i = 0; i < bmpMin.Width; i++)
                    {
                        Hash.Add(bmpMin.GetPixel(i, j).GetBrightness() < 0.5f);
                    }
                }
            }
        }
        /// <summary>
        /// Létrehoz egy bitmapból egy hash-t.
        /// </summary>
        /// <param name="bitmap"></param>
        public ImageHash(Bitmap bitmap)
        {
            Hash = new List<bool>();
            using (Bitmap bmpMin = new Bitmap(bitmap, new Size(HashSize, HashSize)))
            {
                for (int j = 0; j < bmpMin.Height; j++)
                {
                    for (int i = 0; i < bmpMin.Width; i++)
                    {
                        Hash.Add(bmpMin.GetPixel(i, j).GetBrightness() < 0.5f);
                    }
                }
            }
        }
        /// <summary>
        /// Megvizsgálja, hogy ugyan az-e a két hash.
        /// </summary>
        /// <param name="another">Összehasonlítandó hash.</param>
        /// <returns></returns>
        public bool IsSameHash(ImageHash another)
        {
            if (another == null)
            {
                return false;
            }
            for (int i = 0; i < this.Hash.Count; i++)
            {
                if (this.Hash[i] != another.Hash[i])
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Megvizsgálja, hogy ugyan az-e a két hash.
        /// </summary>
        /// <param name="egyik"></param>
        /// <param name="masik"></param>
        /// <returns></returns>
        public static bool IsSameHash(List<bool> egyik, List<bool> masik)
        {
            if (egyik.Count != masik.Count)
            {
                return false;
            }
            for (int i = 0; i < egyik.Count; i++)
            {
                if (egyik[i] != masik[i])
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Megvizsgálja, hogy ugyan az-e a két kép hashje.
        /// </summary>
        /// <param name="egyik"></param>
        /// <param name="masik"></param>
        /// <returns></returns>
        public static bool IsSameImage(Bitmap egyik, Bitmap masik)
        {
            return IsSameHash(ImageHash.ConvertToHash(egyik), ImageHash.ConvertToHash(masik));
        }
        /// <summary>
        /// Egy Bitmap objektum hash kódját adja vissza.
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static List<bool> ConvertToHash(Bitmap bitmap)
        {
            List<bool> h = new List<bool>();
            using (Bitmap bmpMin = new Bitmap(bitmap, new Size(HashSize, HashSize)))
            {
                for (int j = 0; j < bmpMin.Height; j++)
                {
                    for (int i = 0; i < bmpMin.Width; i++)
                    {
                        h.Add(bmpMin.GetPixel(i, j).GetBrightness() < 0.5f);
                    }
                }
            }
            return h;
        }

        public override string ToString()
        {
            string s = "";
            foreach (var item in Hash)
                s += item ? 1 : 0;
            return s;
        }
    }
}
