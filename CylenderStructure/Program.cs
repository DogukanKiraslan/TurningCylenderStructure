using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurningCylinders
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int queueNum = 0;
            int nodeInt;
            string startPoint = "0";

            List<PairedData> Paired = new List<PairedData>();
            List<CylinderNode> nodes = new List<CylinderNode>();

            for (int i = 0; i < 4; i++)
            {
                CylinderNode node = new CylinderNode();
                nodeInt = 1;
                int counter = random.Next(2, 5);
                for (int j = 0; j < counter;j++)
                {
                    Data data = new Data();
                    data.number = nodeInt.ToString();
                    data.mark = false;
                    if (j == counter - 1)
                    {
                        data.mark = true;
                    }
                    node.datas.Add(data);
                    nodeInt++;
                }
                queueNum++;
                node.queueNumber = queueNum;
                
                nodes.Add(node);

                
            }

            foreach (CylinderNode item in nodes)
            {
                Console.WriteLine("qc #" + item.queueNumber);

                foreach (Data data in item.datas)
                {
                    if (!data.mark)
                    {
                        Console.Write(data.number + " ");
                    }
                    else
                    {
                        Console.Write(data.number + "*");
                    }
                }
                Console.WriteLine("\n");
            }

            Turn(nodes, queueNum, startPoint, Paired);

            foreach (PairedData pd in Paired)
            {
                foreach (string item in pd.Pairs)
                {
                    Console.Write(item);
                }
                Console.WriteLine(" ");
            } 
        }
        public static List<PairedData> Turn(List<CylinderNode> cNode_List, int queNum,string startPoint, List<PairedData> Paired)
        {
            if(queNum != 0)
            {
                int queCount = queNum - 1;
                bool loop = true;
                if(queNum == 1)
                {
                    
                    foreach (Data data in cNode_List[0].datas)
                    {
                        PairedData tempPair = new PairedData();
                        tempPair.Pairs.Add(startPoint);
                        tempPair.Pairs.Add("-");
                        tempPair.Pairs.Add(data.number);
                        Paired.Add(tempPair);
                    }
                    
                }

                else if (queCount > 1)
                {
                    while(loop)
                    {
                        CylinderNode cNode = cNode_List.Find(x => x.queueNumber == queCount);
                        Forge(cNode_List, startPoint, queNum, Paired);
                        if (cNode.datas.First().mark)
                        {
                            while (cNode.datas.First().mark)
                            {
                                if (queCount != 1)
                                {
                                queCount--;
                                Turn_by_One(cNode); 
                                cNode = cNode_List.Find(x => x.queueNumber == queCount);
                                }
                                else
                                {
                                    loop = false;
                                    break;
                                }
                            }
                        }
                        if (!cNode.datas.First().mark)
                        {
                            Turn_by_One(cNode);
                        }
                        queCount = queNum - 1;
                    }
                }
            }
            return null;
        }
        public static void Turn_by_One(CylinderNode cNode)
        {
            if (cNode.datas.Count > 1)
            {
                Data firstItem = cNode.datas[0];

                for(int i = 1; i < cNode.datas.Count; i++)
                {
                    cNode.datas[i-1] = cNode.datas[i];
                }
                cNode.datas[cNode.datas.Count - 1] = firstItem;
            }
        }
        public static void Forge(List<CylinderNode> cNode_List, string startPoint, int queNum, List<PairedData> Paired)
        {     
            foreach(Data data in cNode_List.Last().datas)
            {
                PairedData tempP = new PairedData();
                tempP.Pairs.Add(startPoint);
                for (int i = 0; i < queNum-1; i++)
                {
                    tempP.Pairs.Add("-" + cNode_List[i].datas.First().number);
                }
                tempP.Pairs.Add("-" + data.number);
                Paired.Add(tempP);
            }
        }
    }

    public class CylinderNode
    {
        public List<Data> datas = new List<Data>();
        public int queueNumber;
    }
   
    public class Data
    {
        public string number;
        public bool mark;
    }

    public class PairedData 
    {
        public List<string> Pairs = new List<string>();
        public int pairNum;
    }
}
