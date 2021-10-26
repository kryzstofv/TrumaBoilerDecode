using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrumaBoilerDecode
{
    class CircularBuffer<T>
    {
        T[] _buffer;
        int _head;
        int _tail;
        int _length;
        int _bufferSize;
        object _lock = new object();
        bool _overflow;

        public CircularBuffer(int bufferSize)
        {
            _buffer = new T[bufferSize];
            _bufferSize = bufferSize;
            _head = bufferSize - 1;
            _overflow = false;
        }

        public bool IsEmpty
        {
            get { return _length == 0; }
        }

        public bool IsFull
        {
            get { return _length == _bufferSize; }
        }

        public int Count
        {
            get 
            {
                return _length;
            }
        }

        public int Space
        {
            get
            {
                return _bufferSize - _length;
            }
        }

        public T Get()
        {
            lock (_lock)
            {
                if (IsEmpty) throw new InvalidOperationException("Queue exhausted");

                T dequeued = _buffer[_tail];
                _tail = NextPosition(_tail);
                _length--;
                return dequeued;
            }
        }

        private int NextPosition(int position)
        {
            return (position + 1) % _bufferSize;
        }

        public void Put(T toAdd)
        {
            lock (_lock)
            {
                _head = NextPosition(_head);
                _buffer[_head] = toAdd;
                if (IsFull)
                {
                    _tail = NextPosition(_tail);
                    _overflow = true;
                }
                else
                    _length++;
            }
        }

        public void Put(T[] toAdd)
        {
            for(int i=0;i<toAdd.Length;i++)
            {
                Put(toAdd[i]);
            }
        }
    }
}
