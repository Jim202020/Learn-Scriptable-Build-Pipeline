
```csharp
static class ThreadingManager
{
    public enum ThreadQueues
    {
        SaveQueue,
        UploadQueue,
        PruneQueue, //删除队列
        TotalQueues
    }
    
    static Task[] m_Tasks = new Task[(int)ThreadQueues.TotalQueues];

    static ThreadingManager()
    {
        //当编辑器退出过程无法取消时，会引发EditorApplication.quitting事件
        //类似的还有EditorApplication.wantsToQuit事件，通过返回值判断是否退出
        //当编辑器退出时，会调用WaitForOutstandingTasks方法等待所有未解决的任务完成
        EditorApplication.quitting += WaitForOutstandingTasks;

        //重新加载所有程序集前会调用WaitForOutstandingTasks方法等待所有未解决的任务完成
        //类似的还有afterAssemblyReload事件
        AssemblyReloadEvents.beforeAssemblyReload += WaitForOutstandingTasks;
    }
    
    //等待所有未解决的任务完成
    internal static void WaitForOutstandingTasks()
    {
        //查找所有未解决的任务
        var tasks = m_Tasks.Where(x => x != null).ToArray();
        //清空任务列表
        m_Tasks = new Task[(int)ThreadQueues.TotalQueues];
        //Task.WaitAll方法会等待所有任务完成
        if (tasks.Length > 0)
            Task.WaitAll(tasks);
    }

    internal static void QueueTask(ThreadQueues queue, Action<object> action, object state)
    {
        var task = m_Tasks[(int)queue];
        if (queue == ThreadQueues.PruneQueue)
        {
            // Prune tasks need to run after any existing queued tasks
            var tasks = m_Tasks.Where(x => x != null).ToArray();
            m_Tasks = new Task[(int)ThreadQueues.TotalQueues];
            if (tasks.Length > 0)
                task = Task.WhenAll(tasks).ContinueWith(delegate { action.Invoke(state); });
            else
                task = Task.Factory.StartNew(action, state);
        }
        else if (task == null)
        {
            // New Upload or Save tasks need to be done after any queued prune tasks
            var pruneTask = m_Tasks[(int)ThreadQueues.PruneQueue];
            if (pruneTask != null)
                task = pruneTask.ContinueWith(delegate { action.Invoke(state); });
            else
                task = Task.Factory.StartNew(action, state);
        }
        else
            task = task.ContinueWith(delegate { action.Invoke(state); });

        //不管有没，直接替换掉当前的
        m_Tasks[(int)queue] = task;
    }
}
```
