using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FamilyTree
{
    // Represents what the TreeNode can draw
    public interface IDrawable
    {
        // Return the object's needed size
        SizeF GetSize(Graphics gr, Font font);

        // Return true if the node is above this point
        bool IsAtPoint(Graphics gr, Font font, PointF center_pt, PointF target_pt);

        // Draw the object with the center at (x, y)
        void Draw(float x, float y, Graphics gr, Pen pen,
            Brush bg_brush, Brush text_brush, Font font);
    }
}
