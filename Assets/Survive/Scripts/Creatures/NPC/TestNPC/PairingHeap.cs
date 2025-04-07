using System.Collections.Generic;

public class PairingHeapNode<T>
{
    public T Value;
    public int Priority;
    public PairingHeapNode<T> Child, Sibling, Parent;

    public PairingHeapNode(T value, int priority)
    {
        Value = value;
        Priority = priority;
    }
}

public class PairingHeap<T> : ITaskQueue
{
    private PairingHeapNode<T> root;
    private Dictionary<T, PairingHeapNode<T>> nodeMap = new();

    public void Insert(Task task)
    {
        var node = new PairingHeapNode<T>((T)(object)task, task.Priority);
        nodeMap[(T)(object)task] = node;
        root = Merge(root, node);
    }

    public Task ExtractMin()
    {
        if (root == null) return null;
        var min = root.Value;
        nodeMap.Remove(root.Value);
        root = MergeSiblings(root.Child);
        return (Task)(object)min;
    }

    public void DecreaseKey(Task task, int newPriority)
    {
        if (!nodeMap.TryGetValue((T)(object)task, out var node)) return;
        if (newPriority >= node.Priority) return;

        node.Priority = newPriority;
        if (node == root) return;

        if (node.Parent != null)
        {
            if (node.Parent.Child == node)
                node.Parent.Child = node.Sibling;
            else
            {
                var sibling = node.Parent.Child;
                while (sibling != null && sibling.Sibling != node)
                    sibling = sibling.Sibling;
                if (sibling != null)
                    sibling.Sibling = node.Sibling;
            }
        }

        node.Sibling = null;
        node.Parent = null;
        root = Merge(root, node);
    }

    private PairingHeapNode<T> Merge(PairingHeapNode<T> a, PairingHeapNode<T> b)
    {
        if (a == null) return b;
        if (b == null) return a;
        if (a.Priority <= b.Priority)
        {
            b.Sibling = a.Child;
            a.Child = b;
            b.Parent = a;
            return a;
        }
        else
        {
            a.Sibling = b.Child;
            b.Child = a;
            a.Parent = b;
            return b;
        }
    }

    private PairingHeapNode<T> MergeSiblings(PairingHeapNode<T> node)
    {
        if (node == null || node.Sibling == null)
            return node;

        var a = node;
        var b = node.Sibling;
        var rest = b.Sibling;

        a.Sibling = b.Sibling = null;

        return Merge(Merge(a, b), MergeSiblings(rest));
    }

    public bool IsEmpty() => root == null;
}
