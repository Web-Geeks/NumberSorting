namespace NumberSorting.Services.Interfaces;

public interface IMergeSorter
{
    Task<List<int>> DoMergeSort(int[] numbers);
    Task Save(List<string> numbers);
    Task<string[]> Load();
}