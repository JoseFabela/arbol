using System;
using System.Collections.Generic;
using System.Linq;

public class TreeNode
{
    public string Name { get; set; }
    public List<TreeNode> Children { get; } = new List<TreeNode>();

    public TreeNode(string name)
    {
        Name = name;
    }
}

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
        var parentNode = FindParentNode(nodeName);
        if (parentNode != null)
        {
            var nodeToDelete = parentNode.Children.FirstOrDefault(n => n.Name == nodeName);
            if (nodeToDelete != null)
            {
                parentNode.Children.Remove(nodeToDelete);
            }
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
}

class Program
{
    static void Main()
    {
        var root = new TreeNode("CEO");
        var manager1 = new TreeNode("Gerente 1");
        var manager2 = new TreeNode("Gerente 2");
        var employee1 = new TreeNode("Empleado 1");
        var employee2 = new TreeNode("Empleado 2");
        var employee3 = new TreeNode("Empleado 3");

        root.Children.Add(manager1);
        root.Children.Add(manager2);

        manager1.Children.Add(employee1);
        manager1.Children.Add(employee2);

        manager2.Children.Add(employee3);

        var tree = new Tree(root);

        Console.WriteLine("Estructura organizativa (como un árbol):");
        tree.PrintTree(tree.Root);

        while (true)
        {
            Console.WriteLine("\n¿Qué deseas hacer?");
            Console.WriteLine("1. Agregar nodo");
            Console.WriteLine("2. Editar nodo");
            Console.WriteLine("3. Eliminar nodo");
            Console.WriteLine("4. Salir");
            Console.Write("Ingresa tu elección: ");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Opción no válida. Ingresa un número válido.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    Console.Write("Ingresa el nombre del nodo padre: ");
                    string parentNodeName = Console.ReadLine();
                    Console.Write("Ingresa el nombre del nuevo nodo: ");
                    string newNodeName = Console.ReadLine();
                    tree.AddNode(parentNodeName, newNodeName);
                    break;
                case 2:
                    Console.Write("Ingresa el nombre del nodo a editar: ");
                    string nodeNameToEdit = Console.ReadLine();
                    Console.Write("Ingresa el nuevo nombre: ");
                    string newNodeNameToEdit = Console.ReadLine();
                    tree.EditNode(nodeNameToEdit, newNodeNameToEdit);
                    break;
                case 3:
                    Console.Write("Ingresa el nombre del nodo a eliminar: ");
                    string nodeNameToDelete = Console.ReadLine();
                    tree.DeleteNode(nodeNameToDelete);
                    break;
                case 4:
                    Console.WriteLine("Saliendo del programa.");
                    return;
                default:
                    Console.WriteLine("Opción no válida. Ingresa un número válido.");
                    break;
            }

            Console.WriteLine("\nEstructura actual del árbol:");
            tree.PrintTree(tree.Root);
        }
    }
}
