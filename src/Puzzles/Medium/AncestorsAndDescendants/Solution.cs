namespace CodinGame.Puzzles.Medium.AncestorsAndDescendants
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Solution
    {
        private static void Main(string[] args)
        {
            var list = new List<Node>();
            int count = int.Parse(Console.ReadLine());

            for (int i = 0; i < count; i++)
            {
                string line = Console.ReadLine();
                if (!line.StartsWith("."))
                    list.Add(new Node { Id = line });
                else
                    list.Last().AddChild(line.Substring(1));
            }

            foreach (var item in list)
            {
                foreach (var s in item.GetChildrenHierarchy())
                {
                    Console.WriteLine(s);
                }
            }
        }
    }

    public class Node
    {
        public Node()
        {
            Children = new List<Node>();
        }


        public string Id { get; set; }
        public List<Node> Children { get; set; }

        public void AddChild(string name)
        {
            if (name.StartsWith("."))
            {
                name = name.Substring(1);
                Children.Last().AddChild(name);
            }
            else
            {
                Children.Add(new Node { Id = name });
            }
        }

        public IEnumerable<string> GetChildrenHierarchy()
        {
            var result = Id + " > ";
            var list = new List<string>();

            if (Children.Count() == 0)
                yield return Id;
            else
            {
                foreach (var itesm in Children)
                {
                    if (itesm.Children.Count() == 0)
                    {
                        yield return result + itesm.Id;
                    }
                    else
                    {
                        foreach (var item in itesm.GetChildrenHierarchy())
                        {
                            yield return result + item;
                        }
                    }
                }
            }
        }
    }
}