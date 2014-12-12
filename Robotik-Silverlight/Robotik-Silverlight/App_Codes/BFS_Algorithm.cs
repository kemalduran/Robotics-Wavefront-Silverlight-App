using Robotik_Silverlight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robotik_Proje.App_Codes
{
    public class BFS_Algorithm
    {
        public Bolge BFSSearch(Bolge root)
        {
            Queue<Bolge> Q = new Queue<Bolge>();
            Q.Enqueue(root);

            while (Q.Count > 0)
            {
                Bolge p = Q.Dequeue();
                if (p.Equals(MainWindow.tablo.Start))
                    return p;
                foreach (Bolge kk in p.komsular)
                {
                    if (kk.sayi == 0)
                    {
                        Q.Enqueue(kk);
                        kk.sayi = p.sayi + 1;
                    }
                }
            }
            return null;
        }
    }
}
