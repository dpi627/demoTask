using System.Diagnostics;

internal class Program
{
    static readonly int taskCount = 5;
    static readonly int exeMax = 10000;
    static readonly Random r = new();
    static readonly List<Task> tasks = new();

    private static void Main(string[] args)
    {
        Stopwatch watch = new ();
        watch.Start();

        #region code snnipt about Task
        // add all tasks
        for (int i = 0; i < taskCount; i++)
            AddTask(i);

        // loop start tasks
        foreach (Task t in tasks)
        {
            t.Start();
            //t.Wait();
        }

        // wait for all finish
        Task.WaitAll(tasks.ToArray());
        Console.WriteLine($"{Environment.NewLine}All tasks are finished !");
        #endregion

        watch.Stop();

        Console.WriteLine($"Total Time Cost：{GetTime(watch.ElapsedMilliseconds)}");
        Console.Read();
    }

    private static void AddTask(int id)
    {
        // 隨機秒數，模擬執行緒執行時間
        int ms = r.Next(exeMax);
        Task? t = new(() =>
        {
            Console.WriteLine($"Task {id} begin");
            Thread.Sleep(ms);
            Console.WriteLine($"Task {id} finished in {GetTime(ms)}");
        });
        // 加入執行緒陣列
        tasks.Add(t);
    }

    private static string GetTime(long ms)
    {
        TimeSpan t = TimeSpan.FromMilliseconds(ms);
        return $"{t.Hours:D2}h:{t.Minutes:D2}m:{t.Seconds:D2}s:{t.Milliseconds:D3}ms";
    }
}