using System.Threading;

namespace Mbs.Trading
{
    /// <summary>
    /// Compare And Swap (Interlocked Compare and Exchange) queue.
    /// </summary>
    /// <typeparam name="T">A type.</typeparam>
    public class CompareAndSwapQueue<T>
    {
        private Pointer head;
        private Pointer tail;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompareAndSwapQueue{T}"/> class.
        /// </summary>
        public CompareAndSwapQueue()
        {
            var node = new Node();
            head.Node = tail.Node = node;
        }

        /// <summary>
        /// Dequeue an element. Returns a boolean indicating the success of the operation.
        /// </summary>
        /// <returns>A boolean indicating the success of the operation.</returns>
        public T Dequeue()
        {
            T element = default;

            // Keep trying until deque is done.
            bool bDequeNotDone = true;
            while (bDequeNotDone)
            {
                Pointer theHead = head;
                Pointer theTail = tail;
                Pointer next = theHead.Node.Next;

                // Are head, tail, and next consistent?
                if (theHead.Count == head.Count && theHead.Node == head.Node)
                {
                    // Is tail falling behind?
                    if (theHead.Node == theTail.Node)
                    {
                        // Is the queue empty?
                        if (next.Node == null)
                        {
                            // The queue is empty, cannot dequeue.
                            return default;
                        }

                        // Tail is falling behind, try to advance it.
                        Cas(ref tail, theTail, new Pointer(next.Node, theTail.Count + 1));
                    }
                    else
                    {
                        // No need to deal with tail.
                        // Read value before CAS otherwise another deque might try to free the next node.
                        element = next.Node.Value;

                        // Try to swing the head to the next node.
                        if (Cas(ref head, theHead, new Pointer(next.Node, theHead.Count + 1)))
                        {
                            bDequeNotDone = false;
                        }
                    }
                }
            }

            return element;
        }

        /// <summary>
        /// Enqueue an element.
        /// </summary>
        /// <param name="element">The element.</param>
        public void Enqueue(T element)
        {
            var node = new Node { Value = element };
            bool bEnqueueNotDone = true;

            // Keep trying until Enqueue is done.
            while (bEnqueueNotDone)
            {
                // Read Tail.Node and Tail.Count together.
                Pointer theTail = tail;

                // Read next Node and next Count together.
                Pointer next = theTail.Node.Next;

                // Are the tail and next consistent?
                if (theTail.Count == tail.Count && theTail.Node == tail.Node)
                {
                    // Was tail pointing to the last node?
                    if (next.Node == null)
                    {
                        if (Cas(ref theTail.Node.Next, next, new Pointer(node, next.Count + 1)))
                        {
                            bEnqueueNotDone = false;
                        }
                    }
                    else
                    {
                        // Tail was not pointing to last node. Try to swing tail to the next node.
                        Cas(ref tail, theTail, new Pointer(next.Node, theTail.Count + 1));
                    }
                }
            }
        }

        private static bool Cas(ref Pointer destination, Pointer compared, Pointer exchange)
        {
            if (compared.Node == Interlocked.CompareExchange(ref destination.Node, exchange.Node, compared.Node))
            {
                Interlocked.Exchange(ref destination.Count, exchange.Count);
                return true;
            }

            return false;
        }

        private struct Pointer
        {
            /// <summary>
            /// The count.
            /// </summary>
            internal long Count;

            /// <summary>
            /// The node.
            /// </summary>
            internal Node Node;

            /// <summary>
            /// Initializes a new instance of the <see cref="Pointer"/> struct.
            /// </summary>
            /// <param name="node">The node.</param>
            /// <param name="count">The count.</param>
            internal Pointer(Node node, long count)
            {
                Node = node;
                Count = count;
            }
        }

#pragma warning disable SA1401 // Fields should be private
        private class Node
        {
            /// <summary>
            /// The value.
            /// </summary>
            internal T Value;

            /// <summary>
            /// The pointer. The Next member cannot be a property since it participates in CAS operations.
            /// </summary>
            internal Pointer Next;
        }
#pragma warning restore SA1401
    }
}
