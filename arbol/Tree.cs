using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arbol
{
    public class Tree
    {
        public TreeNode Root { get; set; }

        public Tree(TreeNode root)
        {
            Root = root;
        }

        public void PrintTree(TreeNode node, string indent = "")
        {
            Console.WriteLine(indent + "└─ " + node.Name);

            for (int i = 0; i < node.Children.Count; i++)
            {
                PrintTree(node.Children[i], indent + "   ");
            }
        }

        public TreeNode FindNode(string nodeName, TreeNode node = null)
        {
            if (node == null)
                node = Root;

            if (node.Name == nodeName)
                return node;

            foreach (var child in node.Children)
            {
                var foundNode = FindNode(nodeName, child);
                if (foundNode != null)
                    return foundNode;
            }

            return null;
        }

        public void AddNode(string parentNodeName, string newNodeName)
        {
            var parentNode = FindNode(parentNodeName);
            if (parentNode != null)
            {
                parentNode.Children.Add(new TreeNode(newNodeName));
            }
            else
            {
                Console.WriteLine($"No se encontró el nodo padre '{parentNodeName}'.");
            }
        }

        public void EditNode(string nodeName, string newName)
        {
            var nodeToEdit = FindNode(nodeName);
            if (nodeToEdit != null)
            {
                nodeToEdit.Name = newName;
            }
            else
            {
                Console.WriteLine($"No se encontró el nodo '{nodeName}'.");
            }
        }

        public void DeleteNode(string nodeName)
        {
            var nodeToDelete = FindNode(nodeName);
            if (nodeToDelete != null)
            {
                if (nodeToDelete == Root)
                {
                    // No se puede eliminar el nodo raíz
                    Console.WriteLine("No se puede eliminar el nodo raíz.");
                }
                else
                {
                    var parent = FindParentNode(nodeName);
                    if (parent != null)
                    {
                        if (nodeToDelete.Children.Count > 0)
                        {
                            // Convertir el primer hijo en el nuevo padre
                            var firstChild = nodeToDelete.Children[0];
                            firstChild.Children.AddRange(nodeToDelete.Children.Skip(1));
                            parent.Children.Insert(parent.Children.IndexOf(nodeToDelete), firstChild);
                        }
                        parent.Children.Remove(nodeToDelete);
                        Console.WriteLine($"El nodo '{nodeName}' se eliminó, y el primer hijo se convirtió en el nuevo padre sin cambiar la posición de la rama.");
                    }
                }
            }
            else
            {
                Console.WriteLine($"No se encontró el nodo '{nodeName}'.");
            }
        }

        private TreeNode FindParentNode(string nodeName, TreeNode node = null)
        {
            if (node == null)
                node = Root;

            foreach (var child in node.Children)
            {
                if (child.Name == nodeName)
                    return node;

                var parent = FindParentNode(nodeName, child);
                if (parent != null)
                    return parent;
            }

            return null;
        }

        // Guardar la estructura del árbol en un archivo JSON
        public void SaveTreeToFile(string filePath)
        {
            var json = JsonConvert.SerializeObject(this);
            File.WriteAllText(filePath, json);
            Console.WriteLine("Estructura del árbol guardada en el archivo.");
        }


        // Cargar la estructura del árbol desde un archivo JSON
        public static Tree LoadTreeFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                var tree = JsonConvert.DeserializeObject<Tree>(json);
                Console.WriteLine("Estructura del árbol cargada desde el archivo.");
                return tree;
            }
            else
            {
                Console.WriteLine("El archivo no existe. Se creará un nuevo árbol.");
                return null;
            }
        }
    }
}
