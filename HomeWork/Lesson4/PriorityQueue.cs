namespace Lesson4
{
    public sealed class PriorityQueue<E>
    {
        private QueuePart? _queues;
        public void Enqueue(int priority, E value)
        {
            QueuePart priorityQueue = null;
            QueuePart insertAfter = null;
            var nextPart = _queues;
            while (nextPart != null)
            {
                if (nextPart.Priority == priority)
                {
                    priorityQueue = nextPart;
                    break;
                }
                if (nextPart.Priority < priority && (nextPart.NextPart == null || nextPart.NextPart.Priority > priority))
                {
                    insertAfter = nextPart;
                }

                nextPart = nextPart.NextPart;
            }

            var queueElement = new QueueElement() { Value = value };
            if (priorityQueue == null)
            {
                var newPart = new QueuePart() { Head = queueElement, Tail = queueElement, Priority = priority };
                if (insertAfter == null)
                {
                    newPart.NextPart = _queues;
                    _queues = newPart;
                }
                else
                {
                    newPart.NextPart = insertAfter.NextPart;
                    insertAfter.NextPart = newPart;
                }
            }
            else
            {
                priorityQueue.Tail.Next = queueElement;
                priorityQueue.Tail = queueElement;
            }
        }

        public E Dequeue()
        {
            if (_queues == null) return default(E);

            var res = _queues.Head.Value;
            // отобрали все элементы из очереди данного приоритета, переходим к следующей
            if (_queues.Head.Next == null)
            {
                _queues = _queues.NextPart;
            }
            else
            {
                _queues.Head = _queues.Head.Next;
            }
            return res;
        }
        /// <summary>
        /// Очередь определенного приоритета
        /// </summary>
        private class QueuePart
        {
            public QueueElement? Head { get; set; }
            public QueueElement? Tail { get; set; }
            public int Priority { get; set; }
            /// <summary>
            /// очередь следующего по убыванию приоритета
            /// </summary>
            public QueuePart? NextPart { get; set; }
        }

        private class QueueElement
        {
            public E Value { get; set; }
            public QueueElement Next { get; set; }
        }
    }


}
