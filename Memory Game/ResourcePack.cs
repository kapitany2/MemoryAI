using System.Collections.Generic;
using System.Drawing;

namespace Memory_Game
{
    class ResourcePack
    {
        public static ResourcePack AktualisTema;
        internal static List<ResourcePack> resourcePacks;
        public string Name { get; set; }
        public string EleresiUt { get; set; }
        public int KepekSzama { get; set; }
        public List<Image> images { get; set; } = new List<Image>();
        
        /// <summary>
        /// A hátlap képe.
        /// </summary>
        public Image Hatlap;
        /// <summary>
        /// A Levett lap után maradt háttér.
        /// </summary>
        public Image LevettLap;
        public override string ToString()
        {
            return Name + "\n" + EleresiUt + "\n" + KepekSzama;
        }
    }
}
