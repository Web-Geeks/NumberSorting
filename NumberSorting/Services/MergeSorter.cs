using Newtonsoft.Json;
using NumberSorting.Services.Interfaces;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace NumberSorting.Services
{
    public class MergeSorter : IMergeSorter
    {
        public async Task<List<int>> DoMergeSort(int[] numbers)
        {
            var sortedNumbers = MergeSort(numbers);

            for (var i = 0; i < sortedNumbers.Length; i++)
            {
                numbers[i] = sortedNumbers[i];
            }

            return sortedNumbers.ToList();
        }

        public async Task Save(List<string> numbers)
        {
            var dirPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\TestFiles\\";

            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            var fileName = $"NumbersFile.json";
            string json = JsonSerializer.Serialize(numbers);
            await File.WriteAllTextAsync($"{dirPath}{fileName}", json);


        }

        public async Task<string[]> Load()
        {
            var dirPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\TestFiles\\";

            if (!Directory.Exists(dirPath)) return Array.Empty<string>();
            var fileName = $"NumbersFile.json";

            var reader = new StreamReader($"{dirPath}{fileName}");
            string json = await reader.ReadToEndAsync();
            var deserializeObject = JsonConvert.DeserializeObject<List<string>>(json);

            return deserializeObject.ToArray();


        }

        private int[] MergeSort(int[] numbers)
        {
            //base case if numbers contain less than or equal to one
            //return the array

            if (numbers.Length <= 1) return numbers;

            var left = new List<int>();
            var right = new List<int>();

            for (int i = 0; i < numbers.Length; i++)
            {
                //getting odd indexes to split the array into 2 lists
                //this is checked by dividing the index number by 2 and if a remainder it will be odd.
                if (i % 2 > 0)
                {
                    left.Add(numbers[i]);
                }
                else
                {
                    right.Add(numbers[i]);
                }
            }

            left = MergeSort(left.ToArray()).ToList();
            right = MergeSort(right.ToArray()).ToList();

            return Merge(left, right);

        }

        private int[] Merge(List<int> left, List<int> right)
        {
            var result = new List<int>();

            while (left.Any() && right.Any())
            {
                //continue to sort while the left and right list contain values
                MoveValueFromSourceToResult(left.First() <= right.First() ? left : right, result);
            }

            //run just in case there are some left in the list
            while (left.Any())
            {
                MoveValueFromSourceToResult(left, result);
            }

            //run just in case there are some left in the list
            while (right.Any())
            {
                MoveValueFromSourceToResult(right, result);
            }

            return result.ToArray();

        }

        private void MoveValueFromSourceToResult(List<int> list, List<int> result)
        {
            result.Add(list.First());
            list.RemoveAt(0);
        }
    }
}
