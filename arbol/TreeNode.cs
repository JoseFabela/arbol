using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arbol
{
    public class TreeNode
    {

        public string Name { get; set; }
        public List<TreeNode> Children { get; } = new List<TreeNode>();

        public TreeNode(string name)
        {
            Name = name;
        }
    }
}
