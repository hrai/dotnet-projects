using System;

namespace PartA
{
    public class CustomLinkedList<T>
    {
        private Node<T> _node;

        public void Add(T value)
        {
            var node = new Node<T>(null, value);

            if (_node == null)
            {
                _node = node;
            }
            else
            {
                _node.Add(node);
            }
        }

        public int Count()
        {
            return _node.Count();
        }

        public T GetFifthElementFromTailEnd()
        {
            int position = 0;

            if (5 < _node.Count())
            {
                position = _node.Count() - 5;
            }

            return GetElementAt(_node, position);
        }

        private T GetElementAt(Node<T> node, int position)
        {
            if (position == 0)
                return node.Value;

            return GetElementAt(node.LinkedNode, --position);
        }
    }
}