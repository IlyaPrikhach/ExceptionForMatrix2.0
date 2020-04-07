using System;

namespace ExceptionsForMatrix2_0
{
    class Program
    {
       static Matrix matrix1;
       static Matrix matrix2;
        static void Main(string[] args)
        {
            int fchois = chois();
            if (fchois == -1)
            {
                return;
            }
            getSizeMatrix(out int[] sizeMatrix);
            m(sizeMatrix, out string stroka);
            arguments(sizeMatrix, stroka, out double[,] mass);
            matrix1 = new Matrix(mass);
            getSizeMatrix(out sizeMatrix);
            m(sizeMatrix, out stroka);
            arguments(sizeMatrix, stroka,out mass);
            matrix2 = new Matrix(mass);
            output();
            Console.ReadLine();
        }

        static int chois()
        {

            try
            {
                Console.WriteLine("Нужно проводить расчеты, или вывести нулевую матрицу?(c/n)");
                string ch = Console.ReadLine();
                if (ch == "n")
                {
                    getSizeNullMatrix(out int[] sizeNullMatrix);
                    getEmpty(sizeNullMatrix);
                    return -1;
                }
                else if (ch == "c")
                {

                }
                else
                {
                    Console.WriteLine("Возможно вы выбрали неверно, повторите снова");
                    chois();
                }

            }
            catch
            {
                Console.WriteLine("Возможно вы выбрали неверно, повторите снова");
                chois();
            }
            return 0;
        }

            static void getSizeNullMatrix(out int[] sizeNullMatrix)
            {
                try
                {
                    Console.WriteLine("Пожалуйста введите размер нулевой матрицы");
                    string StrokaNull = Console.ReadLine();
                    while(StrokaNull.Contains("  "))
                    {
                        StrokaNull.Replace(" ", " ");
                    }
                    string[] sizeNull = StrokaNull.Split(' ');
                    sizeNullMatrix = new int[2];
                    sizeNullMatrix[0] = Convert.ToInt32(sizeNull[0]);
                    sizeNullMatrix[1] = Convert.ToInt32(sizeNull[1]);

                }
                catch
                {
                    Console.WriteLine("Возможно вы ввели неверно, повторите снова");
                    getSizeNullMatrix(out sizeNullMatrix);
                }
            }

            static void getEmpty(int[] sizeNullMatrix)
            {
                
                int[] ams = sizeNullMatrix;
                double[,] emptyMass = new double[ams[1], ams[0]];

                for (int y = 0; y < ams[1]; y++)
                {
                    for (int x = 0; x < ams[0]; x++)
                    {
                        emptyMass[y, x] = 0;

                    }

                }
                for (int y = 0; y < ams[1]; y++)
                {
                    for (int x = 0; x < ams[0]; x++)
                    {
                        Console.Write(emptyMass[y, x] + "\t");

                    }
                    Console.Write("\n");

                }
                Console.Write("\n");
            }
        
        static void getSizeMatrix(out int[] sizeMatrix)
        {
            try
            {
                Console.WriteLine("Пожалуйста введите размер матрицы");
                string Stroka = Console.ReadLine();
                while (Stroka.Contains("  "))
                {
                    Stroka.Replace(" ", " ");
                }
                string[] size = Stroka.Split(' ');
                sizeMatrix = new int[2];
                sizeMatrix[0] = Convert.ToInt32(size[0]);
                sizeMatrix[1] = Convert.ToInt32(size[1]);

            }
            catch
            {
                Console.WriteLine("Возможно вы ввели неверно, повторите снова");
                getSizeMatrix(out sizeMatrix);
            }
        }

        static void m(int[] sizeMatrix, out string stroka)
        {
            try
            {
                Console.WriteLine("Введите элементы в матрицу");
                stroka = Console.ReadLine();
                double z = Convert.ToDouble(stroka);
                string[] exMass = stroka.Split(' ');
                if (exMass.Length < (sizeMatrix[0] * sizeMatrix[1]))
                {
                    throw new SizeMatrixException($"В  матрицу введено неверное количество элементов. Количество элементов должно быть равным  {sizeMatrix[0] * sizeMatrix[1]}");
                }
            }
            catch (SizeMatrixException ex)
            {
                //Console.WriteLine("Возможно вы ввели неправельные данные в матрицу 1");
                Console.WriteLine($"Ошибка: {ex.Message} ");
                stroka = "";
                m(sizeMatrix, out stroka);
            }
            catch (Exception exc)
            {
                Console.WriteLine($"Ошибка: {exc.Message} ");
                stroka = "";
                m(sizeMatrix, out stroka);
            }
        }

        static int arguments(int[] sizeMatrix, string stroka, out double[,] mass)
        {
            mass = new double[sizeMatrix[1], sizeMatrix[0]];
            int z = Convert.ToInt32(sizeMatrix[0] * sizeMatrix[1]);
            string[] stringMass = new string[z];
            double[] doubleMass = new double[z];
            stringMass = stroka.Replace('.', ',').Split(' ');
            doubleMass = Array.ConvertAll(stringMass, double.Parse);


            int dmc = 0;
            for (int y = 0; y < sizeMatrix[1]; y++)
            {
                for (int x = 0; x < sizeMatrix[0]; x++)
                {
                    mass[y, x] = doubleMass[dmc];
                    dmc++;
                }

            }
            for (int y = 0; y < sizeMatrix[1]; y++)
            {
                for (int x = 0; x < sizeMatrix[0]; x++)
                {
                    Console.Write(mass[y, x] + "\t");

                }
                Console.Write("\n");

            }
            Console.Write("\n");
            return 0;
        }

        static void output()
        {

            try
            {
                Console.WriteLine("Что нужно вывести: сложение, вычитание, умножение(a/s/m)");
                string ch = Console.ReadLine();
                if (ch == "a")
                {
                    Matrix.addition(matrix1, matrix2);
                }
                else if (ch == "s")
                {
                    Matrix.substraction(matrix1, matrix2);
                }
                else if (ch == "m")
                {
                    Matrix.multiplication(matrix1, matrix2);
                }
                else
                {
                    Console.WriteLine("Возможно вы выбрали неверно, повторите снова");
                    output();
                }
            }
            catch
            {
                Console.WriteLine("Возможно вы выбрали неверно, повторите снова");
                output();
            }
        }
    }
    class Matrix
    {
        double[,] massM;

        public Matrix(double[,] massM)
        {
            this.massM = massM;

        }

        public static double[,] operator +(Matrix matrix1, Matrix matrix2)
        {
            try
            {
                if (matrix1.massM.Length != matrix2.massM.Length)
                {
                    throw new SizeMatrixException($"Матрицы содержат различное количество строк и столбцов:\n {matrix1.massM.GetLength(1)} , {matrix1.massM.GetLength(0)} в первой\n {matrix2.massM.GetLength(1)} , {matrix2.massM.GetLength(0)} во второй");
                }
                double[,] addMass = new double[matrix1.massM.GetLength(0), matrix1.massM.GetLength(1)];
                for (int y = 0; y < matrix1.massM.GetLength(0); y++)
                {
                    for (int x = 0; x < matrix1.massM.GetLength(1); x++)
                    {
                        addMass[y, x] = matrix1.massM[y, x] + matrix2.massM[y, x];

                    }

                }
                return addMass;

            }
            catch (SizeMatrixException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message} ");
                double[,] res = null;
                return res;
            }
            catch (Exception exc)
            {
                Console.WriteLine($"Ошибка: {exc.Message} ");
                double[,] res = null;
                return res;
            }

        }
        public static double[,] operator -(Matrix matrix1, Matrix matrix2)
        {
            try
            {
                if (matrix1.massM.Length != matrix2.massM.Length)
                {
                    throw new SizeMatrixException($"Матрицы содержат различное количество строк и столбцов:\n {matrix1.massM.GetLength(1)} , {matrix1.massM.GetLength(0)} в первой\n {matrix2.massM.GetLength(1)} , {matrix2.massM.GetLength(0)} во второй");
                }
                double[,] addMass = new double[matrix1.massM.GetLength(0), matrix1.massM.GetLength(1)];
                for (int y = 0; y < matrix1.massM.GetLength(0); y++)
                {
                    for (int x = 0; x < matrix1.massM.GetLength(1); x++)
                    {
                        addMass[y, x] = matrix1.massM[y, x] - matrix2.massM[y, x];

                    }

                }
                return addMass;

            }
            catch (SizeMatrixException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message} ");
                double[,] res = null;
                return res;
            }
            catch (Exception exc)
            {
                Console.WriteLine($"Ошибка: {exc.Message} ");
                double[,] res = null;
                return res;
            }

        }

        public static void addition(Matrix matrix1, Matrix matrix2)
        {
            double[,] addMass = matrix1 + matrix2;

            Console.WriteLine("Сумма матриц\n");
            for (int y = 0; y < matrix1.massM.GetLength(0); y++)
            {
                for (int x = 0; x < matrix1.massM.GetLength(1); x++)
                {
                    Console.Write(addMass[y, x] + "\t");

                }
                Console.Write("\n");

            }
            Console.Write("\n");
        }
        public static void substraction(Matrix matrix1, Matrix matrix2)
        {
            double[,] addMass = matrix1 - matrix2;

            Console.WriteLine("Разность матриц\n");
            for (int y = 0; y < matrix1.massM.GetLength(0); y++)
            {
                for (int x = 0; x < matrix1.massM.GetLength(1); x++)
                {
                    Console.Write(addMass[y, x] + "\t");

                }
                Console.Write("\n");

            }
            Console.Write("\n");
        }

        public static double[,] operator *(Matrix matrix1, Matrix matrix2)
        {
            try
            {
                if (matrix1.massM.GetLength(1) != matrix2.massM.GetLength(0))
                {
                    throw new SizeMatrixException($"Количество столбцов 1-ой матрицы не равно количеству строк 2-ой матрицы:\n {matrix1.massM.GetLength(1)} , {matrix1.massM.GetLength(0)} в первой\n {matrix2.massM.GetLength(1)} , {matrix2.massM.GetLength(0)} во второй");
                }
                double[,] mulMass = new double[matrix1.massM.GetLength(0), matrix2.massM.GetLength(1)];
                int y = 0, x = 0, x1 = 0, y2 = 0, fy = 0, fx = 0;
                for (int y1 = 0; y1 < matrix1.massM.GetLength(0); y1++)
                {
                    fx = 0;
                    for (int x2 = 0; x2 < matrix2.massM.GetLength(1); )
                    {

                        for (int z = 0; z < matrix1.massM.GetLength(1); z++)
                        {

                            mulMass[y + fy, x + fx] = mulMass[y + fy, x + fx] + matrix1.massM[y1, x1 + z] * matrix2.massM[y2 + z, x2];

                        }
                        if (fx < matrix2.massM.GetLength(1)) {
                            fx++;
                            x2++;
                        }  
                    }

                    fy++;
                }
                return mulMass;

            }
            catch (SizeMatrixException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message} ");
                double[,] res = null;
                return res;
            }
            catch (Exception exc)
            {
                Console.WriteLine($"Ошибка: {exc.Message} ");
                double[,] res = null;
                return res;
            }

        }
        public static void multiplication(Matrix matrix1, Matrix matrix2)
        {
            double[,] addMass = matrix1 * matrix2;

            Console.WriteLine("Разность матриц\n");
            for (int y = 0; y < matrix1.massM.GetLength(0); y++)
            {
                for (int x = 0; x < matrix2.massM.GetLength(1); x++)
                {
                    Console.Write(addMass[y, x] + "\t");

                }
                Console.Write("\n");

            }
            Console.Write("\n");
        }

    }
    class SizeMatrixException : Exception
    {
        public SizeMatrixException(string message)
            : base(message)
        { }
    }
}
