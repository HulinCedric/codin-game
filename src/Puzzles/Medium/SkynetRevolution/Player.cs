namespace CodinGame.Puzzles.Medium.SkynetRevolution
{
    using System;
    using System.Collections.Generic;

    public class Player
    {
        public static void Main(string[] args)
        {
            string[] inputs = Console.ReadLine().Split(' ');
            int N = int.Parse(inputs[0]); // the total number of nodes in the level, including the gateways
            int L = int.Parse(inputs[1]); // the number of links
            int E = int.Parse(inputs[2]); // the number of exit gateways

            Console.Error.WriteLine("N,L,E:{0},{1},{2}", N, L, E);

            Node[] nodes = new Node[N];
            for (int i = 0; i < N; i++)
            {
                nodes[i] = new Node();
            }

            for (int i = 0; i < L; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                int N1 = int.Parse(inputs[0]);                          // N1 and N2 defines a link between these nodes
                int N2 = int.Parse(inputs[1]);
                Console.Error.WriteLine("({0},{1})", N1, N2);
                nodes[N1].linknodes.Add(N2);
                nodes[N2].linknodes.Add(N1);
            }

            int[,] trap = new int[E * 2, 2];
            int[] gateways = new int[E];
            int trapcount = 0;
            for (int i = 0; i < E; i++)
            {
                int ei = int.Parse(Console.ReadLine()); // the index of a gateway node
                gateways[i] = ei;
                if (nodes[ei].isGate == false) nodes[ei].isGate = true;

                for (int j = 0; j < nodes[ei].linknodes.Count; j++)
                {
                    int ln = nodes[ei].linknodes[j];
                    if (nodes[ln].linknodes.Count == 3)
                    {
                        for (int k = 0; k < nodes[ln].linknodes.Count; k++)
                        {
                            int ln2 = nodes[ln].linknodes[k];
                            if (ln2 != ei & nodes[ln2].linknodes.Count > 3)
                            {
                                trap[trapcount, 0] = ln;
                                trap[trapcount, 1] = ln2;
                                trapcount++;
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < E; i++)
            {
                for (int j = i + 1; j < E; j++)
                {
                    if (nodes[gateways[i]].linknodes.Count < nodes[gateways[j]].linknodes.Count)
                    {
                        int tmp = gateways[j];
                        gateways[j] = gateways[i];
                        gateways[i] = tmp;
                        int t0 = trap[j * 2, 0];
                        int t1 = trap[j * 2, 1];
                        trap[j * 2, 0] = trap[i * 2, 0];
                        trap[j * 2, 1] = trap[i * 2, 1];
                        trap[i * 2, 0] = t0;
                        trap[i * 2, 1] = t1;
                        t0 = trap[j * 2 + 1, 0];
                        t1 = trap[j * 2 + 1, 1];
                        trap[j * 2 + 1, 0] = trap[i * 2 + 1, 0];
                        trap[j * 2 + 1, 1] = trap[i * 2 + 1, 1];
                        trap[i * 2 + 1, 0] = t0;
                        trap[i * 2 + 1, 1] = t1;
                    }
                }
            }

            for (int i = 0; i < gateways.Length; i++)
            {
                Console.Error.WriteLine("{0}/", gateways[i]);
            }

            for (int i = 0; i < E * 2; i++)
            {
                Console.Error.WriteLine("trap({0},{1})", trap[i, 0], trap[i, 1]);
            }

            // game loop
            trapcount = 0;
            while (true)
            {
                string result = string.Empty;

                int SI = int.Parse(Console.ReadLine()); // The index of the node on which the Skynet agent is positioned this turn
                Console.Error.WriteLine("SI:{0}", SI);

                for (int i = 0; i < nodes[SI].linknodes.Count; i++)
                {
                    int ln = nodes[SI].linknodes[i];
                    if (nodes[ln].isGate == true)
                    {
                        result = string.Format("{0} {1}", SI, ln);
                        Remove(nodes[SI].linknodes, ln);
                        Remove(nodes[ln].linknodes, SI);
                        break;
                    }
                }

                if (result == string.Empty)
                {
                    if (trap[trapcount, 0] != trap[trapcount, 1])
                    {
                        result = string.Format("{0} {1}", trap[trapcount, 0], trap[trapcount, 1]);
                        Remove(nodes[trap[trapcount, 0]].linknodes, trap[trapcount, 1]);
                        Remove(nodes[trap[trapcount, 1]].linknodes, trap[trapcount, 0]);
                        trapcount++;
                    }
                }

                if (result == string.Empty)
                {
                    for (int i = 0; i < nodes[SI].linknodes.Count; i++)
                    {
                        int ln = nodes[SI].linknodes[i];
                        result = string.Format("{0} {1}", SI, ln);
                        Remove(nodes[SI].linknodes, ln);
                        Remove(nodes[ln].linknodes, SI);
                        break;
                    }
                }

                Console.WriteLine(result);
            }
        }

        private static void Remove(List<int> l, int n)
        {
            for (int i = 0; i < l.Count; i++)
            {
                if (l[i] == n)
                {
                    l.RemoveAt(i);
                }
            }
        }

        public class Node
        {
            public List<int> linknodes = new List<int>();
            public bool isGate = false;
        }
    }
}