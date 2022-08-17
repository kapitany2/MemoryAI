using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Memory_Game.AI
{
    class CPanel : Panel
    {
        public PictureBox PBox;
        public ImageHash ImageHash;
        public byte hanyadikValtozas;
        public bool tiltott;
        public short Index { get; set; }

        public CPanel()
        {
            PBox = new PictureBox()
            {
                Margin = new Padding(0, 0, 0, 0),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Dock = DockStyle.Fill
            };
            this.Controls.Add(PBox);
            hanyadikValtozas = 0;
            tiltott = false;
        }
    }
}
