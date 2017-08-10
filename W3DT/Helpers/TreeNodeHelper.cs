using System.Windows.Forms;

namespace W3DT
{
    class TreeNodeHelper
    {
        public static TreeNode FindOrCreateSubNode(object parent, string text)
        {
            TreeNodeCollection nodes = null;

            if (parent is TreeNode)
                nodes = ((TreeNode)parent).Nodes;
            else if (parent is TreeView)
                nodes = ((TreeView)parent).Nodes;

            if (nodes != null)
            {
                foreach (TreeNode node in nodes)
                    if (node.Text.ToLower().Equals(text.ToLower()))
                        return node;

                TreeNode newNode = new TreeNode(text);
                nodes.Add(newNode);
                return newNode;
            }
            return null;
        }
    }
}
