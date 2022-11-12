

namespace ProjectEuler_11_LargestProductInGrid.FindLargestProductInGrid
{
    public static class LargestProductInGrid
    {
        private static int _gridDimension;
        private static int _numberOfIntegersToMultiply;

        public static void Main(string[] args)
        {
            SetGlobalVariables();

            int[,] gridArray = CreateGridArray();

            PrintGridArray(gridArray);

            IntegerSeries integerSeries = FindTheLargestProductInGrid(ref gridArray);

            PrintIntegerSeriesProduct(integerSeries);
            
            PrintIntegerSeriesIndices(integerSeries);
        }

        private static int[,] CreateGridArray()
        {
            Random random = new Random();

            int[,] gridArray = new int[_gridDimension, _gridDimension];

            for (int row = 0; row < _gridDimension; row++)
            {
                for (int column = 0; column < _gridDimension; column++)
                {
                    gridArray[row, column] = (int)random.Next(100);
                }
            }

            return gridArray;
        }

        private static void PrintGridArray(int[,] gridArray)
        {

            if (gridArray is not null)
            {
                for (int row = 0; row < _gridDimension; row++)
                {
                    for (int column = 0; column < _gridDimension; column++)
                    {
                        Console.Write($" {gridArray[row, column]:00}");
                    }

                    Console.WriteLine();
                }
            }

            Console.WriteLine();
        }
        
        private static IntegerSeries GetHorizontalProduct(ref int[,] gridArray, int rowIndex, int columnIndex)
        {
            IntegerSeries integerSeries = new IntegerSeries();
            
            if (columnIndex + _numberOfIntegersToMultiply < _gridDimension)
            {
                for (int currentColumn = columnIndex; currentColumn < columnIndex + _numberOfIntegersToMultiply; currentColumn++)
                {
                    integerSeries.indices.Add(new Tuple<int, int>(rowIndex, currentColumn));
                    
                    integerSeries.product *= gridArray[rowIndex, currentColumn];
                }
            }

            return integerSeries;
        }

        private static IntegerSeries GetVerticalProduct(ref int[,] gridArray, int rowIndex, int columnIndex)
        {
            IntegerSeries integerSeries = new IntegerSeries();

            if (rowIndex + _numberOfIntegersToMultiply < _gridDimension)
            {
                for (int currentRowIndex = rowIndex; currentRowIndex < rowIndex + _numberOfIntegersToMultiply; currentRowIndex++)
                {
                    integerSeries.indices.Add(new Tuple<int, int>(currentRowIndex, columnIndex));
                    
                    integerSeries.product *= gridArray[currentRowIndex, columnIndex];
                }
            }

            return integerSeries;
        }

        private static IntegerSeries GetForwardDiagonalProduct(ref int[,] gridArray, int rowIndex, int columnIndex)
        {
            IntegerSeries integerSeries = new IntegerSeries();

            if (rowIndex + _numberOfIntegersToMultiply < _gridDimension && columnIndex + _numberOfIntegersToMultiply < _gridDimension)
            {
                for (int currentIndexOffset = 0; currentIndexOffset < _numberOfIntegersToMultiply; currentIndexOffset++)
                {
                    integerSeries.indices.Add(new Tuple<int, int>(rowIndex + currentIndexOffset, columnIndex + currentIndexOffset));
                    
                    integerSeries.product *= gridArray[rowIndex + currentIndexOffset, columnIndex + currentIndexOffset];
                }
            }

            return integerSeries;
        }

        private static IntegerSeries GetBackwardDiagonalProduct(ref int[,] gridArray, int rowIndex, int columnIndex)
        {
            IntegerSeries integerSeries = new IntegerSeries();

            if (rowIndex + _numberOfIntegersToMultiply < _gridDimension && columnIndex + 1 - _numberOfIntegersToMultiply >= 0)
            {
                for (int currentIndexOffset = 0; currentIndexOffset < _numberOfIntegersToMultiply; currentIndexOffset++)
                {
                    integerSeries.indices.Add(new Tuple<int, int>(rowIndex + currentIndexOffset, columnIndex - currentIndexOffset));
                    
                    integerSeries.product *= gridArray[rowIndex + currentIndexOffset, columnIndex - currentIndexOffset];
                }
            }

            return integerSeries;
        }


        private static IntegerSeries FindTheLargestProductInGrid(ref int[,] gridArray)
        {
            IntegerSeries _currentProductOfIntegers;
            IntegerSeries _largestProductOfIntegers = new IntegerSeries(); 

            for (int rowIndex = 0; rowIndex < _gridDimension; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < _gridDimension; columnIndex++)
                {                    
                    _currentProductOfIntegers = GetHorizontalProduct(ref gridArray, rowIndex, columnIndex);
        
                    if (_currentProductOfIntegers.product > _largestProductOfIntegers.product)
                    {
                        _largestProductOfIntegers = _currentProductOfIntegers;
                    }

                    _currentProductOfIntegers = GetVerticalProduct(ref gridArray, rowIndex, columnIndex);

                    if (_currentProductOfIntegers.product > _largestProductOfIntegers.product)
                    {
                        _largestProductOfIntegers = _currentProductOfIntegers;
                    }

                    _currentProductOfIntegers = GetForwardDiagonalProduct(ref gridArray, rowIndex, columnIndex);

                    if (_currentProductOfIntegers.product > _largestProductOfIntegers.product)
                    {
                        _largestProductOfIntegers = _currentProductOfIntegers;
                    }

                    _currentProductOfIntegers = GetBackwardDiagonalProduct(ref gridArray, rowIndex, columnIndex);

                    if (_currentProductOfIntegers.product > _largestProductOfIntegers.product)
                    {
                        _largestProductOfIntegers = _currentProductOfIntegers;
                    }
                }
            }

            return _largestProductOfIntegers;
        }

        private static void PrintIntegerSeriesProduct(IntegerSeries integerSeries)
        {
            Console.WriteLine($"The largest product of {_numberOfIntegersToMultiply} integers in a {_gridDimension} x {_gridDimension} array is: {integerSeries.product}.");
            
            Console.WriteLine();
        }

        private static void PrintIntegerSeriesIndices(IntegerSeries integerSeries)
        {
            if (integerSeries.indices is not null)
            {
                Console.Write($"The sequence of integers that produced the product is: "); 
                
                foreach (Tuple<int, int> index in integerSeries.indices)
                {
                    Console.Write($"[{index.Item1.ToString()},{index.Item2.ToString()}] ");
                }

                Console.WriteLine();
            }
            else
            {
                Console.WriteLine($"No sequence of integers produced a product!");
            }
        }

        private static void SetGlobalVariables()
        {
            var configuration = ProjectConfiguration.GetProjectConfiguration();

            string? gridDimension = configuration["GridDimension"];
            string? numberOfIntegersToMultiply = configuration["NumberOfIntegersToMultiply"];

            if (gridDimension is not null && int.TryParse(gridDimension, out int gridDimensionOutput))
            {
                _gridDimension = gridDimensionOutput;
            }

            if (numberOfIntegersToMultiply is not null && int.TryParse(numberOfIntegersToMultiply, out int numberOfIntegersToMultiplyOutput))
            {
                _numberOfIntegersToMultiply = numberOfIntegersToMultiplyOutput;
            }

            if (!(_gridDimension <= 100 && _gridDimension > _numberOfIntegersToMultiply))
            {
                _gridDimension = 20;
            }

            if (!(_numberOfIntegersToMultiply <= 9 && _numberOfIntegersToMultiply < _gridDimension))
            {
                _numberOfIntegersToMultiply = 4;
            }
        }
    }

    public struct IntegerSeries
    {
        public IntegerSeries()
        {
            product = 1;
            indices = new List<Tuple<int, int>>();
        }

        public long product;
        public List<Tuple<int, int>> indices;
    }
}


