using System.Collections.Generic;

namespace Robotik_Proje.App_Codes
{
    public class BFS_Algorithm
    {
        public Bolge BFSSearch(Gezinti gez)
        {
            Queue<Bolge> Q = new Queue<Bolge>();
            Q.Enqueue(gez.end);

            while (Q.Count > 0)
            {
                Bolge p = Q.Dequeue();
                if (p.Equals(gez.current))
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
