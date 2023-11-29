using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;
using arbol;

class Program
{
    static void Main()
    {
        Tree tree = null;  // Variable para almacenar el árbol
        Console.WriteLine("Desea Cargar El Archivo Anterior? \n Y Para Si \n N Para No"); string resp = Console.ReadLine();
        
         
        if (resp == "Y" || resp == "y")
        {
            // Verificar si existe un archivo JSON y cargar la estructura del árbol si es así
            if (File.Exists("arbol.json"))
        {
            tree = Tree.LoadTreeFromFile("arbol.json");
            // Realiza cualquier operación adicional en el árbol cargado
        }
        else
        {
                Console.WriteLine("No se encontró un archivo JSON, el árbol se creará en el siguiente bloque"); 
                // No se encontró un archivo JSON, el árbol se creará en el siguiente bloque
        }
    }
        else if (resp == "N" || resp == "n")

        {
            // Si deseas crear el árbol de ejemplo por defecto, puedes hacerlo aquí
            if (tree == null)
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

            tree = new Tree(root);
        }
        }
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
                    Console.WriteLine("Guardando la estructura del árbol y saliendo del programa.");
                    tree.SaveTreeToFile("arbol.json");  // Guardar la estructura del árbol antes de salir
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
