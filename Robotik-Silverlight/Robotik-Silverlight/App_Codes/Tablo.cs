
namespace Robotik_Proje.App_Codes
{
    public class Tablo
    {
        public Bolge[,] bolgeler;
        public int row, column;
        public Bolge Start, End;

        public Tablo()
        {
        }
        public Tablo(int x, int y)
        {
            row = x;
            column = y;
        }
        public void initBolgeler()
        {
            bolgeler = new Bolge[row, column];
            for (int i = 0; i < row; ++i)
            {
                for (int j = 0; j < column; ++j)
                {
                    bolgeler[i, j] = new Bolge(i,j);
                }
            }
        }
        public void initKomsular()
        {
            for (int i = 0; i < row; ++i)
            {
                for (int j = 0; j < column; ++j)
                {
                    // yan
                    if (j != column - 1) bolgeler[i, j].komsular.Add(bolgeler[i, j + 1]);
                    if (i != row - 1) bolgeler[i, j].komsular.Add(bolgeler[i + 1, j]);
                    if (i != 0) bolgeler[i, j].komsular.Add(bolgeler[i - 1, j]);
                    if (j != 0) bolgeler[i, j].komsular.Add(bolgeler[i, j - 1]);

                    // çarpraz
                    if (j != column - 1 && i != row - 1) bolgeler[i, j].komsular.Add(bolgeler[i + 1, j + 1]);
                    if (j != 0 && i != 0) bolgeler[i, j].komsular.Add(bolgeler[i - 1, j - 1]);
                    if (j != 0 && i != row - 1) bolgeler[i, j].komsular.Add(bolgeler[i + 1, j - 1]);
                    if (j != column - 1 && i != 0) bolgeler[i, j].komsular.Add(bolgeler[i - 1, j + 1]);
                }
            }
        }


    }
}
