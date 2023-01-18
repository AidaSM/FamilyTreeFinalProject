using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using System.Windows.Forms;
using FamilyTree;

namespace FamilyTree
{
    public class PictureNode : IDrawable
    {
        public Image Picture = null;
        public Person Description;
        public bool Selected = false;
        public PictureNode(Person description, Image picture)
        {
            Description = description;
            Picture = picture;
        }

        // The size of the drawn rectangle
        public SizeF NodeSize = new SizeF(100, 100);

        // Return the size needed
        public SizeF GetSize(Graphics gr, Font font)
        {
            return NodeSize;
        }

        // Return the node's location
        private RectangleF Location(PointF center)
        {
            return new RectangleF(
                center.X - NodeSize.Width / 2,
                center.Y - NodeSize.Height / 2,
                NodeSize.Width, NodeSize.Height);
        }

        // Return True if the target is under this node
        public bool IsAtPoint(Graphics gr, Font font, PointF center_pt, PointF target_pt)
        {
            RectangleF rect = Location(center_pt);
            return rect.Contains(target_pt);
        }

        // Draw the person
        public void Draw(float x, float y, Graphics gr, Pen pen, Brush bg_brush, Brush text_brush, Font font)
        {
            // Draw border
            RectangleF rectf = Location(new PointF(x, y));
            Rectangle rect = Rectangle.Round(rectf);
            if (Selected)
            {
                gr.FillRectangle(Brushes.White, rect);
                ControlPaint.DrawBorder3D(gr, rect,
                    Border3DStyle.Sunken);
            }
            else
            {
                gr.FillRectangle(Brushes.LightGray, rect);
                ControlPaint.DrawBorder3D(gr, rect,
                    Border3DStyle.Raised);
            }

            // Draw the picture
            rectf.Inflate(-5, -5);
            rectf = PositionImage(Picture, rectf);
            gr.DrawImage(Picture, rectf);
        }

        // Find a rectangle to draw the image as large as possible without stretching
        private RectangleF PositionImage(Image picture, RectangleF rect)
        {
            float pic_wid = picture.Width;
            float pic_hgt = picture.Height;
            float pic_aspect = pic_wid / pic_hgt;
            float rect_aspect = rect.Width / rect.Height;
            float scale = 1;
            if (pic_aspect > rect_aspect)
            {
                scale = rect.Width / pic_wid;
            }
            else
            {
                scale = rect.Height / pic_hgt;
            }

            pic_wid *= scale;
            pic_hgt *= scale;
            RectangleF drawing_rect = new RectangleF(
                rect.X + (rect.Width - pic_wid) / 2,
                rect.Y + (rect.Height - pic_hgt) / 2,
                pic_wid, pic_hgt);
            return drawing_rect;
        }
    }
}
