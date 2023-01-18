using FamilyTree;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;


namespace FamilyTree
{
    public partial class Form1 : Form
    {
        List<Panel> listPanel = new List<Panel>();
        int index;
        List<Person> family = new List<Person>();

        public Form1()
        {
            InitializeComponent();
        }
        TreeNode<PictureNode> root =
                   new TreeNode<PictureNode>(
                       new PictureNode(new Person("Unknown", new DateTime(12 / 12 / 2012), DotNetOpenAuth.OpenId.Extensions.SimpleRegistration.Gender.Female, "dead queen", 4),
                           Properties.Resources.Unknown));
        private void Form1_Load(object sender, EventArgs e)
        {

            panelTree.Controls.Add(picTree);
            listPanel.Add(panelTable);
            listPanel.Add(panelTree);
            listPanel[index].BringToFront();

            panelTree.AutoScroll = true;

            ArrangeTree();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (index > 0)
                listPanel[--index].BringToFront();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (index < listPanel.Count - 1)
                listPanel[++index].BringToFront();
            ArrangeTree();
        }
        private void picTree_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            root.DrawTree(e.Graphics);
        }

        // Center the tree on the form.
        private void picTree_Resize(object sender, EventArgs e)
        {
            ArrangeTree();
        }
        private void ArrangeTree()
        {
            using (Graphics gr = picTree.CreateGraphics())
            {
                // Arrange the tree once to see how big it is.
                float xmin = 0, ymin = 0;
                root.Arrange(gr, ref xmin, ref ymin);

                // Arrange the tree again to center it horizontally.
                xmin = (this.ClientSize.Width - xmin) / 2;
                ymin = 10;
                root.Arrange(gr, ref xmin, ref ymin);
            }

            picTree.Refresh();
        }

        // The currently selected node.
        private TreeNode<PictureNode> SelectedNode;

        private void picTree_MouseClick(object sender, MouseEventArgs e)
        {
            FindNodeUnderMouse(e.Location);
        }


        // Set SelectedNode to the node under the mouse.
        private void FindNodeUnderMouse(PointF pt)
        {
            // Deselect the previously selected node.
            if (SelectedNode != null)
            {
                SelectedNode.Data.Selected = false;
                nameLabel.Text = "";
            }

            // Find the node at this position (if any).
            using (Graphics gr = picTree.CreateGraphics())
            {
                SelectedNode = root.NodeAtPoint(gr, pt);
            }

            // Select the node.
            if (SelectedNode != null)
            {
                SelectedNode.Data.Selected = true;
                nameLabel.Text = SelectedNode.Data.Description.Name;
            }

            // Redraw.
            picTree.Refresh();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            picTree.Image = null;
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            var dialogResult = openFileDialog1.ShowDialog();
            if (dialogResult != DialogResult.OK)
            {
                return;
            }

            var fileName = openFileDialog1.FileName;

            var lines = FileReader.ReadFromFile(fileName);

            family = lines.Select(x => FileReader.Parse(x))
                .Where(x => x != null)
                .ToList();

            List<TreeNode<PictureNode>> nodes = new List<TreeNode<PictureNode>>();

            TreeNode<PictureNode> currentNode;

            int current = -1;

            foreach (var member in family)
            {
                dataGridView1.Rows.Add(member.Name, member.DateOfBirth, member.Gender, member.Occupation, member.NumberOfChildren);
                if (current < 0)
                {
                    if (GetImageByName(member.Name) != null)
                        currentNode = new TreeNode<PictureNode>(new PictureNode(member, GetImageByName(member.Name)));
                    else
                        currentNode = new TreeNode<PictureNode>(new PictureNode(member, Properties.Resources.Unknown));

                    root = currentNode;
                    nodes.Add(root);
                    current++;
                }
                else
                    if (nodes[current].Children.Count < nodes[current].Data.Description.NumberOfChildren)
                {

                    if (GetImageByName(member.Name) != null)
                        currentNode = new TreeNode<PictureNode>(new PictureNode(member, GetImageByName(member.Name)));
                    else
                        currentNode = new TreeNode<PictureNode>(new PictureNode(member, Properties.Resources.Unknown));


                    nodes[current].AddChild(currentNode);
                    nodes.Add(currentNode);
                }
                else
                {

                    current++;
                    while (nodes[current].Data.Description.NumberOfChildren == 0)
                        current++;

                    if (GetImageByName(member.Name) != null)
                        currentNode = new TreeNode<PictureNode>(new PictureNode(member, GetImageByName(member.Name)));
                    else
                        currentNode = new TreeNode<PictureNode>(new PictureNode(member, Properties.Resources.Unknown));
                    nodes[current].AddChild(currentNode);
                    nodes.Add(currentNode);

                }

            }

            ArrangeTree();
            picTree.Refresh();
        }
        public static Bitmap GetImageByName(string imageName)
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            string resourceName = asm.GetName().Name + ".Properties.Resources";
            var rm = new System.Resources.ResourceManager(resourceName, asm);
            return (Bitmap)rm.GetObject(imageName);

        }

        private void picTree_Click(object sender, EventArgs e)
        {

        }
    }
}
